using MyGame.Pokemons;
using MyGame.BattleCalculators;
using MyGame.BattleParticipant;

namespace MyGame.States
{
    public class ToxicState : PokemonState
    {
        private int _stateTurn = 0;

        public ToxicState(IBattlePokemon pokemon) : base(pokemon) { }

        public override TurnEndResult OnTurnEnd()
        {
            _stateTurn++;
            var damage = StatusEffectCalculator.ToxicDamage(_pokemon.MaxHp, _stateTurn);
            _pokemon.TakeDamage(damage);

            return _pokemon.IsFainted ? TurnEndResult.Fainted : TurnEndResult.Damaged;
        }
    }
}