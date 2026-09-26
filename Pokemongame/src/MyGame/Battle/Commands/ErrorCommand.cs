using MyGame.Logs;
using MyGame.BattleSystem;

namespace MyGame.Commands
{
    public class ErrorCommand : IBattleCommand
    {
        public BattlePriority Priority 
        {
            get => BattlePriority.Behavior;
        }  
        
        public void Execute()
        {
            GameLog.Error("액션이 선택되지 않았습니다!");
        }
    }
}