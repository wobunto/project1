using MyGame.Trainers;
using MyGame.Commands;
using MyGame.ControllerStates;
using MyGame.Logs;
using MyGame.Views;
using MyGame.Inputs;
using MyGame.Pokemons;
using MyGame.BattleStatus;

namespace MyGame.BattleControllers
{
    public class BattleController : IBattleStateContext
    {   
        private readonly Stack<PlayerState> _stateStack = new Stack<PlayerState>();
        private PlayerState? CurrentState 
        {
                get => _stateStack.Count > 0 ? _stateStack.Peek() : null;
        }

        public IPlayerView View { get; }
        public IBattleTrainer Player { get; }
        public IBattleTargetTrainer Enemy { get; }

        public bool ForceSwitch {get; private set;}
        public bool IsTurnFinished { get; private set; }
        public IBattleCommand SelectedCommand { get; private set; }
        
        public BattleController(
            IBattleTrainer player,
            IBattleTargetTrainer enemy,
            IPlayerView view)
        {
            if(enemy.ActivePokemon == null)           //배틀 컨트롤러는 배틀이 시작한 뒤 만들어지니 ActivePokemon이 존재해야 함
                throw new InvalidOperationException("현재 enemy.ActivePokemon이 null입니다.");
            if(player.ActivePokemon == null)
                throw new InvalidOperationException("현재 player.ActivePokemon이 null입니다.");

            Player = player;
            Enemy = enemy;
            View = view;

            ForceSwitch = false;
            SetPokemonStatus(player.Party);
            SelectedCommand = BattleCommandFactory.CreateErrorCommand();
        }

        public void Enter()
        {
            CurrentState?.Enter(this);
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
            
            CurrentState?.Enter(this);
        }

        public void ResetState()
        {
             _stateStack.Clear();
            
            SelectedCommand = BattleCommandFactory.CreateErrorCommand();
            
            IsTurnFinished = false;
            PushState(PlayerState.MenuState);
        }

        public bool TryBackState(Input input)
        {
            if (!input.IsCancel) 
                return false;
    
            PopState();
            return true;
        }

        public void FinishedTurn(IBattleCommand command)
        {
            SelectedCommand = command;
            ForceSwitch = false;
            IsTurnFinished = true;
        }

        public IBattlePokemon GetActivePokemon()
        {
            if(Player.ActivePokemon == null)
                throw new InvalidOperationException("현재 ActivePokemon이 null입니다.");

            return Player.ActivePokemon;
        }
        
        public void PushForceSwitchState()
        {
            ForceSwitch = true;
            PushState(PlayerState.SwitchState);
        }
        private void SetPokemonStatus(IReadOnlyList<PokemonRuntime> party)
        {
            foreach (IBattlePokemon pokemon in party)
            {
                PokemonStatus state = new(pokemon);
            }
        }
    }
}