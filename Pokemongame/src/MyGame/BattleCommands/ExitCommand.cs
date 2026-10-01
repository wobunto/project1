using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public class ExitCommand : IBattleCommand
    {
        public bool IsPlayerCommand {get; }
        public int TrainerId {get;}
        public BattlePriority Priority 
        {
            get => BattlePriority.Behavior;
        }  

        public void Execute()
        {
            // 도망 실행
        }
    }
}