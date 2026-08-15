
namespace Pokemongame
{
    public class Actor
    {
        private ActionState _currentState;
        private IActionSelector _selector;
        private readonly ISwitchable self;   

        public EffectState CurrentEffectState => self.ActivePokemon.CurrentEffectState;
        public int MaxHp => self.ActivePokemon.MaxHp;

        public void TakeDamage(int damage) => self.ActivePokemon.TakeDamage(damage);
       
        public int EffectStateTurn {get; private set;}
        
        private const int _start = 1; 

        public Actor(IActionSelector selector, ISwitchable trainer)
        {
            _currentState = new ErrorState(); //Select 가 정상적으로 실행되면 바뀜
            _selector = selector;
            self = trainer;

            EffectTurnStart();   //상태이상 턴 1로 초기화.
        }

        public void Select()
            =>_currentState = _selector.SelectAction();
        
        public void Execute()
        {   
            Update();                   
            _currentState.Execute(this);
        }

        public void Update()
        {
            EffectProcessor.Process(this); //기절 혹은 저림 등의 이유로 행동 실패를 하면 _state = SkipTurn 또는 피해 입음
            _currentState.Update(this);
        }

        public void ChangeSkipTurnState()
            => _currentState = new SkipTurnState(this);
        
        public void ChangeForceSwitchState()
            => _currentState = new ForceSwitchState(self);

        public void EffectStateReset()
        {
            self.ActivePokemon.SetEffectState(EffectState.None);
            EffectTurnStart();
        }

        public void EffectTurnPP()
            => EffectStateTurn++;
        
        private void EffectTurnStart()
            => EffectStateTurn = _start;
    
    }

    public abstract class ActionState
    {
        public abstract void Update(Actor actor);
        public abstract void Execute(Actor actor);
    }

    public static class EffectProcessor
    {
        public static void Process(Actor actor)
        {
            int damage;

            EffectState effectState = actor.CurrentEffectState;

            switch (effectState)
            {
                case EffectState.None:
                    break;

                case EffectState.Sleep:
                    if(IsSleep(actor))
                    {
                        actor.ChangeSkipTurnState();
                        actor.EffectTurnPP();
                    }
                    else
                        actor.EffectStateReset();

                    break;

                case EffectState.Paralysis:
                    if(Chance.TryChance(25))  //25% 확률로 true;
                        actor.ChangeSkipTurnState();
                    break;

                case EffectState.Burn:
                    damage = Math.Max(1, actor.MaxHp / 16);
                    actor.TakeDamage(damage);
                    break;

                case EffectState.Poison:
                    damage = Math.Max(1, actor.MaxHp / 8);
                    actor.TakeDamage(damage);
                    break;

                case EffectState.Toxic:
                    damage = Math.Max(1, actor.MaxHp / 16 * actor.EffectStateTurn);
                    actor.TakeDamage(damage);
                    actor.EffectTurnPP();
                    break;

                case EffectState.Freeze:
                    if(IsFreeze(actor))
                    {
                        actor.ChangeSkipTurnState();
                        actor.EffectTurnPP();
                    }
                    else
                        actor.EffectStateReset();

                    break;
            }
        }

        public static bool IsSleep(Actor actor)
        {
            if(actor.EffectStateTurn >= 3)
                return false;                   //3번째 턴에는 무조건 풀림

            else if(actor.EffectStateTurn == 2)   
                if(Chance.TryChance(33))         // 2번째 턴에선 33% 확률로 깨어남
                    return false;

            return true;                  // // 1번째 턴에서는 잠듬    
        }

        public static bool IsFreeze(Actor actor)
        {
            if(actor.EffectStateTurn > 3)  //3턴이 지나면 자동으로 풀림
                return false;

            else if(Chance.TryChance(25))   //25% 확률로 풀림        
                return false;
            
            return true;
        }
    }

    public class AttackState : ActionState
    {
        private PokemonRuntime _attacker;
        private PokemonRuntime _defender;
        private MoveRuntime _move;

        public AttackState(
            PokemonRuntime attacker,
            PokemonRuntime defender,
            MoveRuntime move)
        {
            _attacker = attacker;
            _defender = defender;
            _move = move;
        }

        public override void Execute(Actor actor)
        {
            _move.ConsumePP();

            float TypeMultiplier =
                _move.Data.Type.CalculateTypeMultipler(_defender.Data.Types);

            int currentAttack = _attacker.CurrentAttack;

            int damage =
                Calculator.CalculateDamage(currentAttack, TypeMultiplier);

            _defender.TakeDamage(damage);

            _attacker.LogBattleResult(
                _defender,
                _move.Data,
                damage,
                TypeMultiplier);
        }

        public override void Update(Actor actor)
        {
            // 공격 상태 업데이트
        }
    }

    public class ForceSwitchState : ActionState    // 미완성! ActionSeletor에서 받아올지 여기서 받아올지 모름!
    {
        private ISwitchable _trainer;
        private int _index;

        public ForceSwitchState(ISwitchable trainer)
        {
            _trainer = trainer;
        }
        public override void Execute(Actor actor)
        {
            _trainer.SwitchActive(_index);         // 현재 0 인 상태  나중에 수정해야 함
        }

        public override void Update(Actor actor)
        {
            // 교체 상태 업데이트
        }
    }

    public class SwitchState : ActionState
    {
        private TrainerRuntime _trainer;
        private int _index;

        public SwitchState(TrainerRuntime trainer, int index)
        {
            _trainer = trainer;
            _index = index;
        }

        public override void Execute(Actor actor)
        {
            _trainer.SwitchActive(_index);
        }

        public override void Update(Actor actor)
        {
            // 교체 상태 업데이트
        }
    }


    public class UseItemState : ActionState
    {
        private TrainerRuntime _trainer;
        private PokemonRuntime _pokemon;
        private ItemData _item;

        public UseItemState(
            TrainerRuntime trainer,
            PokemonRuntime pokemon,
            ItemData item)
        {
            _trainer = trainer;
            _pokemon = pokemon;
            _item = item;
        }

        public override void Execute(Actor actor)
        {
            _trainer.ConsumeItem(_item.Key, 1);

            switch (_item.Effect)
            {
                case ItemEffectType.Heal:
                    _pokemon.Heal(_item.EffectValue);
                    break;

                case ItemEffectType.Capture:
                    GameLog.Info("미구현");
                    break;
            }
        }

        public override void Update(Actor actor)
        {
        }
    }


    public class SkipTurnState : ActionState
    {
        public SkipTurnState(Actor actor)
        {
            //사유를 받아서 저장해놓음
        }

        public override void Execute(Actor actor)
        {
            //스킵하는 사유 출력
        }

        public override void Update(Actor actor)
        {
        }
    }


    public class ExitState : ActionState
    {
        public override void Execute(Actor actor)
        {
        }

        public override void Update(Actor actor)
        {
        }
    }
    
    public class ErrorState : ActionState
    {
         public override void Execute(Actor actor)
        {
            GameLog.Error("액션이 선택되지 않았습니다!");
        }

        public override void Update(Actor actor)
        {
        }
    }
}
