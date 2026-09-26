using MyGame.Pokemons;
using MyGame.BattleCalculators;
using MyGame.BattleSystems;

namespace MyGame.BattleStatus
{
    public class PoisonState : PokemonState
    {
        public PoisonState(IBattlePokemon pokemon) : base(pokemon) { }
        public override EffectState Kind => EffectState.Poison;

        public override void OnTurnEnd()
        {
            var damage = StatusEffectCalculator.PoisonDamage(_pokemon.MaxHp);
            _pokemon.TakeDamage(damage);
            BattleLog.LogStatusDamged(_pokemon, Kind, damage);
        }
    }
}