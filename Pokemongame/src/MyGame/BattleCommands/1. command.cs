using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public interface IBattleCommand
    {
        BattlePriority Priority {get;}
        int TrainerId { get; }
        void Execute();
    }
}