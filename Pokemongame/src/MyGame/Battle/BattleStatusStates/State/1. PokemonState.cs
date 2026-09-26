using MyGame.Pokemons;

namespace MyGame.BattleStatus
{   
    public abstract class PokemonState
    {
        protected readonly IBattlePokemon _pokemon;
        public abstract EffectState Kind { get; }   // 이 State가 어떤 EffectState인지
        
        public PokemonState(IBattlePokemon pokemon)
        {
            _pokemon = pokemon;
        }

        public virtual BeforeActionResult OnBeforeAction() => BeforeActionResult.None;
        public virtual void OnTurnEnd() {}
        
        public virtual float ModifyAttack(float currentAttack) => currentAttack;
        public virtual float ModifySpeed(float currentSpeed) => currentSpeed;
    }
}