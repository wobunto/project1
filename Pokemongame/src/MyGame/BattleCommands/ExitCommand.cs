using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public class ExitCommand : IBattleCommand 
    {     
        public int TrainerId {get;}            //도망은 플레이어만 칠 수 있으니 0임.
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