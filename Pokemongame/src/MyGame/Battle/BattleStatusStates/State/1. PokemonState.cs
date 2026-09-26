using MyGame.Pokemons;

namespace MyGame.BattleStatus
{   
    public abstract class PokemonState
    {
        protected readonly IBattlePokemon _pokemon;

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