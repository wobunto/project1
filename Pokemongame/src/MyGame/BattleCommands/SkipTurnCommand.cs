using MyGame.BattleSystems;
namespace MyGame.Commands
{
   public class SkipTurnCommand : IBattleCommand
    {
        private string _reason;
        
        public int TrainerId {get;}
       
        public BattlePriority Priority 
        {
            get => BattlePriority.Speed;
        }    

        public SkipTurnCommand(string reason, int trainerId)
        {
            _reason = reason;
            TrainerId = trainerId;
        }

        public void Execute()
        {
            // 스킵하는 사유 출력
        }
    }
}