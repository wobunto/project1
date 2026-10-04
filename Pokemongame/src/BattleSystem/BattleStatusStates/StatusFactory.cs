using MyGame.Pokemons;

namespace MyGame.BattleStatus
{
    public static class StatusFactory
    {
        public static PokemonState Create(EffectState state, IBattlePokemon pokemon)
        {
            return state switch
            {
                EffectState.None      => new NormalState(pokemon),
                EffectState.Sleep     => new SleepState(pokemon),
                EffectState.Paralysis => new ParalysisState(pokemon),
                EffectState.Burn      => new BurnState(pokemon),
                EffectState.Poison    => new PoisonState(pokemon),
                EffectState.Toxic     => new ToxicState(pokemon),
                EffectState.Freeze    => new FreezeState(pokemon),
                _ => throw new ArgumentOutOfRangeException(nameof(state), $"정의되지 않은 상태입니다: {state}")
            };
        }
    }
}
