using MyGame.BattleControllers;
using MyGame.Commands;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
    public class RunState : PlayerState
    {
        public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
            context.FinishedTurn(BattleCommandFactory.CreateExitCommand());         //미완성 
        }
    }
}