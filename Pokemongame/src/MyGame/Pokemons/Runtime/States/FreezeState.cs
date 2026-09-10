using MyGame.Pokemons;
using MyGame.Utilities;
using MyGame.BattleParticipant;

namespace MyGame.States
{
    public class FreezeState : PokemonState
    {
        private int _stateTurn;
        private const int _start = 0;

        public FreezeState(IBattlePokemon pokemon)
            : base(pokemon)
        {
            _stateTurn = _start;
        }

        public override StatusTurnResult TryExecute()
        {
            _stateTurn++;

            if (IsFreeze())
                return StatusTurnResult.Freeze;

            return StatusTurnResult.None;
        }

        private bool IsFreeze()
        {
            if (_stateTurn > 3)
                return false;

            if (Chance.TryChance(25))
                return false;

            return true;
        }
    }
}