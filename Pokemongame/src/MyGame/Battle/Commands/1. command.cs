using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public interface IBattleCommand
    {
        BattlePriority Priority {get;}
        void Execute();
    }
}