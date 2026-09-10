using MyGame.Pokemons;
using MyGame.Utilities;
using MyGame.BattleParticipant;

namespace MyGame.States
{

    public class ParalysisState : PokemonState
    {
        public ParalysisState(IBattlePokemon pokemon)
            : base(pokemon)
        {
        }

        public override StatusTurnResult TryExecute()
        {
            if (IsParalysis())
                return StatusTurnResult.Paralysis;

            return StatusTurnResult.None;
        }

        private bool IsParalysis()
            => Chance.TryChance(25);
    }
}