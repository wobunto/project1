using MyGame.BattleCalculators;
using MyGame.Pokemons;
using MyGame.BattleParticipant;

namespace MyGame.States
{
    public class BurnState : PokemonState
    {
        public BurnState(IBattlePokemon pokemon)
            : base(pokemon)
        {
        }

        public override StatusTurnResult TryExecute()
        {
            var damage = StatusEffectCalculator.BurnDamage(_pokemon.MaxHp);

            _pokemon.TakeDamage(damage);

            if (IsDead())
                return StatusTurnResult.Death;

            return StatusTurnResult.None;
        }
    }
}