using MyGame.Logs;
using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public class ErrorCommand : IBattleCommand
    {
        public int TrainerId {get;}

        public BattlePriority Priority 
        {
            get => BattlePriority.Behavior;
        }  
        
        public IReadOnlyList<string> Execute()
        {
            return ["액션이 선택되지 않았습니다!"];
        }
    }
}