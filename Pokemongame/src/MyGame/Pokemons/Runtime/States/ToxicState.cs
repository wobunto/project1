using MyGame.Pokemons;
using MyGame.BattleParticipants;
using MyGame.BattleCalculators;

namespace MyGame.States
{
    public class ToxicState : PokemonState
    {
        private int _stateTurn;
        private const int _start = 0;

        public ToxicState(IBattlePokemon pokemon)
            : base(pokemon)
        {
            _stateTurn = _start;
        }

        public override StatusTurnResult TryExecute()
        {
            _stateTurn++;

            int damage = StatusEffectCalculator.ToxicDamage(
                _pokemon.MaxHp,
                _stateTurn);

            _pokemon.TakeDamage(damage);

            if (IsDead())
                return StatusTurnResult.Death;

            return StatusTurnResult.None;
        }
    }
}