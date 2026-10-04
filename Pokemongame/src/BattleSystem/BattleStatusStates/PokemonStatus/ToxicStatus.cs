using MyGame.Pokemons;
using MyGame.BattleCalculators;
using MyGame.BattleSystems;

namespace MyGame.BattleStatus
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
        public override EffectState Kind => EffectState.Toxic;

        public override void OnTurnEnd()
        {
            _stateTurn++;

            int damage = StatusEffectCalculator.ToxicDamage(
                _pokemon.MaxHp,
                _stateTurn);

            _pokemon.TakeDamage(damage);
            BattleLog.LogStatusDamged(_pokemon, Kind, damage);
        }
    }
}