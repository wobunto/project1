using MyGame.Pokemons;
using MyGame.BattleParticipant;

namespace MyGame.States
{   
    public abstract class PokemonState
    {
        protected readonly IBattlePokemon _pokemon;

        protected PokemonState(IBattlePokemon pokemon)
        {
            _pokemon = pokemon;
        }

        public abstract StatusTurnResult TryExecute();

        protected bool IsDead()
        {
            return _pokemon.IsFainted;
        }
    }
}