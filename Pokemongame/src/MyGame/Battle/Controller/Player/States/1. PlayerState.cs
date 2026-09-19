using MyGame.Controllers;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
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
        public virtual void Resume(PlayerController context) { }
        public virtual void Update(PlayerController context) { }
        public virtual void Enter(PlayerController context) { }
    }
}