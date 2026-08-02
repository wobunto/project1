namespace Pokemongame
{
    public enum BattleActionType  
    {
        //행동
        Attack,
        Item,
        Switch,
        Run
    }

    public abstract class BattleAction
    {
        public BattleActor Actor { get; }

        protected BattleAction(BattleActor actor)
        {
            Actor = actor;
        }
    }

    public class AttackAction : BattleAction
    {
        public MoveRuntime Move { get; }
        
        public AttackAction(BattleActor actor,MoveRuntime move)
            : base(actor)
        {
            Move = move;
        }
    }

    public class ItemAction : BattleAction
    {
        public ItemAction (BattleActor actor)
            : base(actor)
        {
        // 아이템을 받는 코드
        }
    }

    public class SwitchAction : BattleAction
    {
        public int TargetIndex {get;}
        public SwitchAction(BattleActor actor, int targetIndex)
            : base(actor)
            {
                TargetIndex = targetIndex;
            }
    }

    public class RunAction : BattleAction
    {
        
        public RunAction (BattleActor actor)
            : base(actor)
            {
            // 도망가는 코드 
            }
    }
}