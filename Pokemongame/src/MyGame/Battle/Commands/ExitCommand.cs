using MyGame.BattleSystem;

namespace MyGame.Commands
{
    public class ExitCommand : IBattleCommand
    {
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