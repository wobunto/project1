using MyGame.Pokemons;
using MyGame.BattleParticipants;
using MyGame.Utilities;


namespace MyGame.States
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

        public override StatusTurnResult TryExecute()
        {
            _stateTurn++;

            if (IsSleep())
                return StatusTurnResult.Sleep;

            return StatusTurnResult.None;
        }

        private bool IsSleep()
        {
            if (_stateTurn >= 3)
                return false;

            if (_stateTurn == 2 && TryChance(33))
                return false;

            return true;
        }
    }
}