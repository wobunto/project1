using System.Threading.Tasks;
using MyGame.Commands;
using MyGame.Trainers;

namespace MyGame.BattleCommanders
{
    public interface IBattleCommander 
    {
        int NameId { get; }
        IBattleTrainer Trainer {get;}
      
        Task<IBattleCommand> SelectCommandAsync();
        Task<IBattleCommand> GetForcedSwitchCommandAsync();
    }
}