using MyGame.BattleCalculators;
using MyGame.Pokemons;
using MyGame.BattleSystems;

namespace MyGame.BattleStatus
{
    public class BurnState : PokemonState
    {
        public BurnState(IBattlePokemon pokemon) : base(pokemon) { }
        public override EffectState Kind => EffectState.Burn;

        public override void OnTurnEnd()
        {
            var damage = StatusEffectCalculator.BurnDamage(_pokemon.MaxHp);
            
            _pokemon.TakeDamage(damage);

            BattleLog.LogStatusDamged(_pokemon, Kind, damage);
        }
    }
}
