using MyGame.Pokemons;

namespace MyGame.BattleStatus
{
    // 아무런 제약이나 데미지가 없는 평상시 상태
    public class NormalState : PokemonState
    {
        public NormalState(IBattlePokemon pokemon) : base(pokemon) { }
        public override EffectState Kind => EffectState.None;
    }
}