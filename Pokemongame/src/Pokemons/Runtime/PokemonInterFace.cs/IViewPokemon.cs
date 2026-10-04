using MyGame.Types;
using MyGame.Moves;
using MyGame.BattleStatus;

namespace MyGame.Pokemons
{
    public interface IViewPokemon
    {  
        int Id {get;}
        int MaxHp { get; }
        IReadOnlyList<PokemonType> Types {get;}
        IReadOnlyList<MoveRuntime> CurrentMoves {get;}

        int Level {get;}
        int CurrentAttackDamage { get; }
        int CurrentSpeed {get;}
        int CurrentHp {get;}
        int CurrentDefence {get;}
        bool IsFainted {get;}
        public EffectState CurrentEffectState {get;}
    }
}