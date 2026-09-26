using MyGame.Pokemons;
using MyGame.Utilities;

namespace MyGame.BattleStatus
{
    public class SleepState : PokemonState
    {
        private int _stateTurn;
        private const int _start = 0;

        public SleepState(IBattlePokemon pokemon)
            : base(pokemon)
        {
            _stateTurn = _start;
        }

        public override BeforeActionResult OnBeforeAction()
        {
            _stateTurn++;

            if (IsSleep())
                return BeforeActionResult.ASleep;

            return BeforeActionResult.WokeUp;
        }

        private bool IsSleep()
        {
            if (_stateTurn >= 3)
                return false;

            if (_stateTurn == 2 && Utility.TryChance(33))
                return false;

            return true;
        }
    }
}