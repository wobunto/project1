using MyGame.BattleSystems;

namespace MyGame.Commands
{
    public interface IBattleCommand
    {
        BattlePriority Priority {get;}
        bool IsPlayerCommand { get; }
        void Execute();
    }
}