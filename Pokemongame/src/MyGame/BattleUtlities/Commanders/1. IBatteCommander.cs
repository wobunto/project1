using MyGame.Commands;

namespace MyGame.BattleCommanders
{
    public interface IBattleCommander 
    {
        int NameId { get; }
      
        IBattleCommand SelectCommand();
        TurnResult IsActivePokemonFainted();
    }
}