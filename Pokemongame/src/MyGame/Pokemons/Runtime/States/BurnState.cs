using MyGame.BattleCalculators;
using MyGame.Pokemons;
using MyGame.BattleParticipant;

namespace MyGame.States
{
    public class BurnState : PokemonState
    {
        public BurnState(IBattlePokemon pokemon) : base(pokemon) { }

        public override TurnEndResult OnTurnEnd()
        {
            var damage = StatusEffectCalculator.BurnDamage(_pokemon.MaxHp);
            
            _pokemon.TakeDamage(damage);

            return _pokemon.IsFainted ? TurnEndResult.Fainted : TurnEndResult.Damaged;
        }
    }
}
