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

        public virtual BeforeActionResult OnBeforeAction(PokemonStatus status) => BeforeActionResult.Pass; //추상화해서 status를 받을 수 있지만 그정돈가 싶음
        public virtual void OnTurnEnd() {}

        public virtual float ModifyAttack(float currentAttack) => currentAttack;
        public virtual float ModifySpeed(float currentSpeed) => currentSpeed;
    }
}