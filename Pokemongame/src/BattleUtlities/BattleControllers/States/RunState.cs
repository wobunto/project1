using MyGame.BattleControllers;
using MyGame.Commands;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
    public class RunState : PlayerState
    {
        public override void Enter(IBattleStateContext context)
        {
            // 4번을 누르자마자 도망 커맨드를 생성하고 턴을 종료함
            context.FinishedTurn(BattleCommandFactory.CreateExitCommand());
        }

        public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
           //미완성 
        }
    }
}