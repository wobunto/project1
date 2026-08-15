namespace Pokemongame
{
    public class Actor
    {
        private ActionState _currentState;
        public IActionSelector _selecter;

        public int EffectStateTurn {get; private set;}

        public Actor(ActionState menu, IActionSelector selecter)
        {
            _currentState = menu;
            _selecter = selecter;
        }

        public void Select()
        {
            _currentState = new MenuState();
            _currentState.Execute(this);     //메뉴에서 _state 상태를 지정.
        }

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

        public void ChangeState(ActionState state)
        {
            _currentState = state;
            EffectStateTurnReset();
        }
        
        public void EffectStateTurnPP()
        {
            EffectStateTurn++;
        }
        
        public void EffectStateTurnReset()
        {
            EffectStateTurn = 0;
        }
    }

    public abstract class ActionState
    {
        public abstract void Update(Actor actor);
        public abstract void Execute(Actor actor);

        protected static readonly MenuState Selecting = new();
    }

    public static class EffectProcessor
    {
        public static void Process(Actor actor)
        {
            EffectState effectState = actor._selecter.GetEffectState();

            switch (effectState)
            {
                case EffectState.None:
                    break;

                case EffectState.Sleep:
                    if(IsSleep(actor))
                    {
                        actor.EffectStateTurnPP();
                        actor.ChangeState(new SkipTurnState(actor));
                    }
                    break;

                case EffectState.Paralysis:
                    if(Chance.TryChance(25))  //25% 확률로 true;
                        actor.ChangeState(new SkipTurnState(actor));  
                    break;

                case EffectState.Burn:
                    break;

                case EffectState.Poison:
                    break;

                case EffectState.Toxic:
                    break;

                case EffectState.Freeze:
                    break;
            }
        }
        public static bool IsSleep(Actor actor)
        {
            if(actor.EffectStateTurn <= 0)
                return true;                   //잠듦

            else if(actor.EffectStateTurn > 2)  //2이상 깨어남
                return false;

            else
                if(Chance.TryChance(33))       // 33% 확률로 깨어남
                    return false;

                return true;
        }
    }

    public class MenuState : ActionState
    {
        public override void Execute(Actor actor)
        {
            ActionState actionState = actor._selecter.SelectAction();
            actor.ChangeState(actionState);
        }

        public override void Update(Actor actor)
        {
           
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
}
