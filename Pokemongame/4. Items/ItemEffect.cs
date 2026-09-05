using MyGame.Pokemons;
using MyGame.States;

namespace MyGame.Items
{
    public interface IItemEffect
    {
        bool CanApply(IItemTarget pokemon);
        void Apply(IItemTarget pokemon, int value);
    }
    
    public class HealEffect : IItemEffect
    {
        public bool CanApply(IItemTarget pokemon)
        {
            return !pokemon.IsFainted
                && pokemon.CurrentHp < pokemon.MaxHp;
        }
    
        public void Apply(IItemTarget pokemon, int value)
        {
            pokemon.Heal(value);
        }
    }
    
    public class FullRestoreEffect : IItemEffect
    {
        public bool CanApply(IItemTarget pokemon)
        {
            return !pokemon.IsFainted;
        }
    
        public void Apply(IItemTarget pokemon, int value = 0)
        {
            pokemon.FullHeal();
            pokemon.SetEffectState(EffectState.None);
        }
    }
    
    public class ReviveEffect : IItemEffect
    {
        public bool CanApply(IItemTarget pokemon)
        {
            return pokemon.IsFainted;
        }
    
        public void Apply(IItemTarget pokemon, int value = 0)
        {
            pokemon.Revive();
            pokemon.FullHeal();
        }
    }
    
    public class CaptureEffect : IItemEffect
    {
        public bool CanApply(IItemTarget pokemon)
        {
            // 현재는 미구현
            return false;
        }
    
        public void Apply(IItemTarget pokemon, int value)
        {
            // 포획 처리
        }
    }


    public static class ItemEffectFactory
    {
        public static readonly IItemEffect Heal = new HealEffect();
        public static readonly IItemEffect FullRestore = new FullRestoreEffect();
        public static readonly IItemEffect Revive = new ReviveEffect();
        public static readonly IItemEffect Capture = new CaptureEffect();

        public static IItemEffect Create(ItemEffectType type)
        {
            return type switch
            {
                ItemEffectType.Heal        => Heal,
                ItemEffectType.FullRestore => FullRestore,
                ItemEffectType.Revive      => Revive,
                ItemEffectType.Capture     => Capture,
    
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }
}