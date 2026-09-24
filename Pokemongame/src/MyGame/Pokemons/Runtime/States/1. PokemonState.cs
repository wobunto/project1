using MyGame.Pokemons;
using MyGame.BattleParticipants;

namespace MyGame.States
{   
    public abstract class PokemonState
    {
        protected readonly IBattlePokemon _pokemon;

        protected PokemonState(IBattlePokemon pokemon)
        {
            _pokemon = pokemon;
        }

        public virtual BeforeActionResult OnBeforeAction() => BeforeActionResult.CanAct;

           
        public virtual void OnAciton() { }

        public virtual TurnEndResult OnTurnEnd() => TurnEndResult.None;
        
      

        public virtual float ModifyAttack(float currentAttack) => currentAttack;
        
        public virtual float ModifySpeed(float currentSpeed) => currentSpeed;
    }
}