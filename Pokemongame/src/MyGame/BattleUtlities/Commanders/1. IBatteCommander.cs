using MyGame.Commands;
using MyGame.Trainers;

namespace MyGame.BattleCommanders
{
    public interface IBattleCommander 
    {
        int NameId { get; }
        IBattleTrainer Trainer {get;}
      
        IBattleCommand SelectCommand();
        IBattleCommand GetForcedSwitchCommand();
    }
}