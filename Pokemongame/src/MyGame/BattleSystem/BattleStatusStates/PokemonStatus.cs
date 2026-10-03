using MyGame.Pokemons;

namespace MyGame.BattleStatus
{
    public class PokemonStatus
    {   
        protected readonly IBattlePokemon _pokemon;

        public PokemonState CurrentState { get; private set; }
        public EffectState Kind => CurrentState.Kind;

        public PokemonStatus(IBattlePokemon pokemon)
        {
            _pokemon = pokemon;
            CurrentState = new NormalState(pokemon);
        }

        public void ChangeState(EffectState status)
        {
            CurrentState = StatusFactory.Create(status, _pokemon);
        }
        public void CureState()
        {
            ChangeState(EffectState.None);
        }

        public virtual BeforeActionResult OnBeforeAction() => CurrentState.OnBeforeAction(this);
        public virtual void OnTurnEnd() => CurrentState!.OnTurnEnd();
        
        public virtual float ModifyAttack(float currentAttack) => CurrentState!.ModifyAttack(currentAttack);
        public virtual float ModifySpeed(float currentSpeed) => CurrentState!.ModifySpeed(currentSpeed);
    }
}