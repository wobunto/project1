namespace Pokemongame
{
    public interface IActionSelector
    {
        public ActionState SelectAction();
        public EffectState GetEffectState();
    }

    public enum EffectState
    {
        None,
        Sleep,
        Paralysis,
        Burn,
        Poison,
        Toxic,
        Freeze
    }
}