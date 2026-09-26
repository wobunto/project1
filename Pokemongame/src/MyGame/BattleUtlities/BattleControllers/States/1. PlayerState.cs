using MyGame.BattleControllers;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
    public abstract class PlayerState
    {
        public static readonly PlayerState MenuState = new MenuState();
        public static readonly PlayerState AttackState = new AttackState();
        public static readonly PlayerState ItemState = new ItemState();
        public static readonly PlayerState RunState = new RunState();
        public static readonly PlayerState SwitchState = new SwitchState();
     
        public abstract void HandleInput(
            IBattleStateContext context,
            Input input);
        //public virtual void Resume(IBattleStateContext context) { } 현재 Resume과 Enter에 차이가 없으므로 오류는 나지 않음.
        public virtual void Update(IBattleStateContext context) { }
        public virtual void Enter(IBattleStateContext context) { }
    }
}