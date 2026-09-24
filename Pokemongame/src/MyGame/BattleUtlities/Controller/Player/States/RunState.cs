using MyGame.Controllers;
using MyGame.Commands;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
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
}