namespace Pokemongame
{
    public enum ItemEffectType
    {
        Heal,
        Capture,
        // ...
    }
    public record ItemData(
        int Key, 
        string Name, 
        string Description, 
        ItemEffectType Effect, 
        int Value);
}