using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Items;
using MyGame.Commands;
using MyGame.Logs;
using MyGame.Views;
using MyGame.Inputs;

namespace MyGame.Controllers
{
    public class PlayerController : IBattleController
    {   
        public IPlayerView View { get; }
        public IBattleTrainer Player { get; }
        public IBattleTrainer Enemy { get; }
        
        public bool ForceSwitch {get; private set;}
        public bool IsTurnFinished { get; private set; }
        public Command SelectedCommand { get; private set; }
        
        private readonly Stack<PlayerState> _stateStack = new Stack<PlayerState>();
        
        public PlayerState CurrentState => _stateStack.Peek();

        public PlayerController(
            IBattleTrainer player,
            IBattleTrainer enemy,
            IPlayerView view)
        {
            Player = player;
            Enemy = enemy;
            View = view;

            SelectedCommand = new ErrorCommand();

            PushState(PlayerState.MenuSte);
        }
        
        public void HandleInput(Input input)
        {
            CurrentState?.HandleInput(this, input);
        }
    
        public void Update()
        {
            CurrentState?.Update(this);
        }

        public void PushState(PlayerState nextState)
        {
            _stateStack.Push(nextState); 
            CurrentState?.Enter(this);
        }
        
        public void PopState()
        {
            if (_stateStack.Count <= 1)
            {
                GameLog.Warn("메뉴에서는 돌아갈 수 없습니다.");
                return;
            }

            _stateStack.Pop();
        }

        public void ResetState()
        {
            while (_stateStack.Count > 0)
            {
                _stateStack.Pop();
            }

            PushState(PlayerState.MenuSte);
            IsTurnFinished = false;
        }

        public bool IsBack(Input input)
        {
            if (!input.IsCancel) return false;
            
            this.PopState();
            return true;
        }

        public void FinishedTurn(Command command)
        {
            SelectedCommand = command;
            IsTurnFinished = true;
        }
    }

    public abstract class PlayerState
    {
        public static readonly PlayerState MenuSte = new MenuState();
        public static readonly PlayerState AttackSte = new AttackState();
        public static readonly PlayerState ItemSte = new ItemState();
        public static readonly PlayerState SwitchSte = new SwitchState();
        public static readonly PlayerState RunSte = new RunState();
        public static readonly PlayerState StruggleSte = new StruggleState();
     
        public abstract void HandleInput(
            PlayerController context,
            Input input);
        
        public virtual void Update(PlayerController context) { }
        public virtual void Enter(PlayerController context) { }
    }


    public class MenuState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            context.View.DisplayCommandMenu();
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            IBattlePokemon activePokemon = context.Player.ActivePokemon;

            switch (input.Value)
            {
                case 1:
                    if (activePokemon.IsAbleMove())
                    {
                        context.PushState(StruggleSte);  
                        break;
                    }
                    context.PushState(AttackSte);
                    break;

                case 2:
                    context.PushState(ItemSte);
                    break;

                case 3:
                    context.PushState(SwitchSte);
                    break;

                case 4:
                    context.PushState(RunSte);
                    break;
            }   
        }  
    }

    public class AttackState : PlayerState
    {
        public override void Enter(PlayerController context)
        {    
            var _attacker = context.Player.ActivePokemon; //상대 포켓몬이 교체하면 포켓몬이 바뀌니 상대방을 넣음.
            var _moves = _attacker.CurrentMoves;
            
            context.View.DisplayAttackMenu(_moves);
        }

        public override void HandleInput(PlayerController context, Input input)
        {
            var _attacker = context.Player.ActivePokemon;  
            var _defender = context.Enemy;        
            var _moves = _attacker.CurrentMoves;
            
            if(context.IsBack(input)) return;

            if (_attacker.TryGetUseableMove(input.Value, out var move))
            {
            // 성공: 기술이 있고 PP도 있음
                Command attack = new AttackCommand(_attacker,
                                                   _defender, 
                                                   move!);
           
                context.FinishedTurn(attack);
                return;
            }
            GameLog.Warn("그 기술은 지금 사용할 수 없습니다.");
        }
    }
    
    
    public class ItemState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            var _inventory = context.Player.Inventory;
            context.View.DisplayItemMenu(_inventory);
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            if(context.IsBack(input)) return; 
            
            var player = context.Player;
            var keys = player.Inventory.Keys.ToList();
            
            if (player.Inventory.Count == 0)
                return;

            int index = input.Value - 1;
            int itemKey = keys[index];

             if (!ItemDatabase.TryGetItem(itemKey, out ItemData? item))
            {
                GameLog.Error("아이템 데이터가 존재하지 않습니다.");
                return;
            }
            IItemEffect effect = ItemEffectFactory.Create(item!.Effect);
            
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    IItemTarget pokemon = player.Party[index];

                    var itemCmd = new UseItemCommand(
                        context.Player, 
                        pokemon,
                        item
                        );
                    context.FinishedTurn(itemCmd);
                },
                filter: effect.CanApply // 도메인에 위임된 규칙
            );
    
            context.PushState(selectState);

           //아이템으로 회복은 물론 상태회복,PP회복, 
            //데미지, 스피드 등의 랭크업도 가능하니 IBattle로 많은 기능
        }
    }


    public class SwitchState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            var player = context.Player;
            // 선택 완료 시 실행될 액션을 람다로 전달  (AI 도움)
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    var switchCmd = new SwitchCommand(player, index);
                    context.FinishedTurn(switchCmd);
                },
                filter: player.CanSwitch, // 도메인에 위임된 규칙
                canCancel: !context.ForceSwitch
            );
    
            context.PushState(selectState);
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            //Debug("선택 단계에서 취소"); 디버그를 아직 구현 안했으니 대충 주석
            context.PopState();          
        }
        
        public override void Update(PlayerController context)
        {
            
        }
    }

    public class RunState : PlayerState
    {
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            Command run = new ExitCommand();
            context.FinishedTurn(run);         //미완성 
        }
        
        public override void Update(PlayerController context)
        {
           
        }
    }

    public class StruggleState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            GameLog.Info($"{context.Player.ActivePokemon.Name}은 현재 사용할 수 있는 기술이 없다...");
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            var attacker = context.Player.ActivePokemon;
            var defender = context.Enemy;
            var struggle = context.Player.ActivePokemon.GetStruggle();
            
            var attack = new AttackCommand(attacker, defender, struggle);
            context.FinishedTurn(attack);
        }
    }
    
    public class SelectPokemonState : PlayerState
    {
        private readonly Action<int> _onSelected;
        private readonly Func<PokemonRuntime, bool> _filter;
        private readonly bool _canCancel;

        public SelectPokemonState(Action<int> onSelected, Func<PokemonRuntime, bool> filter, bool canCancel = true)
        {
            _onSelected = onSelected;
            _filter = filter;
            _canCancel = canCancel;
        }
            
        public override void Enter(PlayerController context)
        {
            context.View.DisplayPartyMenu(context.Player.Party);
        }
        
         public override void HandleInput(
            PlayerController context,
            Input input)
        {
            if(_canCancel && context.IsBack(input)) return;

            int index = input.Value - 1;
            
            if(index < 0  || index >= context.Player.Party.Count)
            {
                GameLog.Warn("선택 가능한 포켓몬 번호를 입력해주세요.");
                return;
            }
    
            var pokemon = context.Player.Party[index];
            
            if (!_filter(pokemon))
            {
                GameLog.Warn("선택할 수 없는 포켓몬입니다.");
                return;
            }
            
            context.PopState();
            _onSelected?.Invoke(index); // 상위 상태에서 등록한 콜백 실행  
        }
    }
}