using MyGame.BattleSystem;
namespace MyGame.Commands
{
   public class SkipTurnCommand : IBattleCommand
    {
        private string _reason;
        
        public BattlePriority Priority 
        {
            get => BattlePriority.Speed;
        }    

        public SkipTurnCommand(string reason)
        {
            _reason = reason;
        }

        public void Execute()
        {
            // 스킵하는 사유 출력
        }
    }
}