using MyGame.Pokemons;

namespace MyGame.BattleStatus
{
    public class PokemonStatus
    {   
        protected readonly IBattlePokemon _pokemon;
        public PokemonState? CurrentState { get; private set; }

        public PokemonStatus(IBattlePokemon pokemon)
        {
            _pokemon = pokemon;

            ChangeState(_pokemon.CurrentEffectState); // 생성 시 기존 포켓몬의 상태.
        }

        public void ChangeState(EffectState status)
        {
            CurrentState = StatusFactory.Create(status, _pokemon);
        }
        
        public void NormalState()
        {
            CurrentState =  StatusFactory.Create(EffectState.None, _pokemon);
        }

        public virtual BeforeActionResult OnBeforeAction() => CurrentState!.OnBeforeAction();
        public virtual void OnTurnEnd() => CurrentState!.OnTurnEnd();
        
        public virtual float ModifyAttack(float currentAttack) => CurrentState!.ModifyAttack(currentAttack);
        public virtual float ModifySpeed(float currentSpeed) => CurrentState!.ModifySpeed(currentSpeed);
    }
}