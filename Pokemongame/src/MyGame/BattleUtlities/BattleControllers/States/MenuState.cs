using MyGame.BattleControllers;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
    public class MenuState : PlayerState
    {
        public override void Enter(IBattleStateContext context)
        {
            context.View.DisplayCommandMenu();
        }
        
        public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
          

            switch (input.Value)
            {
                case 1:
                    context.PushState(AttackState);
                    break;

                case 2:
                    context.PushState(ItemState);
                    break;

                case 3:
                    context.PushState(SwitchState);
                    break;

                case 4:
                    context.PushState(RunState);
                    break;
                    
                default:            
                    context.View.DisplayMessage("잘못된 번호입니다.");
                    break;
            }   
        } 
     
    }
}