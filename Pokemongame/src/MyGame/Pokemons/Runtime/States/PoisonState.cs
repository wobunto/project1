using MyGame.Pokemons;
using MyGame.BattleCalculators;
using MyGame.BattleParticipant;

namespace MyGame.States
{
    public class PoisonState : PokemonState
    {
        public PoisonState(IBattlePokemon pokemon)
            : base(pokemon)
        {
        }

        public override StatusTurnResult TryExecute()
        {
            var damage = StatusEffectCalculator.PoisonDamage(_pokemon.MaxHp);

            _pokemon.TakeDamage(damage);

            if (IsDead())
                return StatusTurnResult.Death;

            return StatusTurnResult.None;
        }
    }
}