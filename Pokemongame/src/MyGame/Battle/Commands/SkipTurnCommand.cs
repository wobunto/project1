namespace MyGame.Commands
{
   public class SkipTurnCommand : Command
    {
        private string _reason;

        public SkipTurnCommand(string reason)
        {
            _reason = reason;
        }

        public override void Execute()
        {
            // 스킵하는 사유 출력
        }
    }
}