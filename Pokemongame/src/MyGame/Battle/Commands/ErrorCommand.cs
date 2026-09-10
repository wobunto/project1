using MyGame.Logs;

namespace MyGame.Commands
{
    public class ErrorCommand : Command
    {
        public override void Execute()
        {
            GameLog.Error("액션이 선택되지 않았습니다!");
        }
    }
}