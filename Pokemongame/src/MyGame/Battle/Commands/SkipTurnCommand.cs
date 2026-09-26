using MyGame.BattleSystems;
namespace MyGame.Commands
{
   public class SkipTurnCommand : IBattleCommand
    {
        private string _reason;
        public bool IsPlayerCommand{get;}
       
        public BattlePriority Priority 
        {
            get => BattlePriority.Speed;
        }    

        public SkipTurnCommand(string reason, bool isPlayer)
        {
            _reason = reason;
            IsPlayerCommand = isPlayer;
        }

        public void Execute()
        {
            // 스킵하는 사유 출력
        }
    }
}