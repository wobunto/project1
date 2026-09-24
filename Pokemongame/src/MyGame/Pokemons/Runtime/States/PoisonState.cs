using MyGame.Pokemons;
using MyGame.BattleCalculators;
using MyGame.BattleParticipants;

namespace MyGame.States
{
    public class PoisonState : PokemonState
    {
        public PoisonState(IBattlePokemon pokemon) : base(pokemon) { }

        public override TurnEndResult OnTurnEnd()
        {
            var damage = StatusEffectCalculator.PoisonDamage(_pokemon.MaxHp);
            _pokemon.TakeDamage(damage);

            return _pokemon.IsFainted ? TurnEndResult.Fainted : TurnEndResult.Damaged;
        }
    }
}