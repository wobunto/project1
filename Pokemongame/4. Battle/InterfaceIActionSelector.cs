namespace Pokemongame
{
    public interface IActionSelector
    {
        public ActionState SelectAction();
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