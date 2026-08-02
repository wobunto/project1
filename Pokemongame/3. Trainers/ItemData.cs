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

    public static class ItemCategory
    {
        private static Dictionary<int, ItemData> _items = new();

        public static bool TryGetItem(int key, out ItemData? data)
            => _items.TryGetValue(key, out data);
    }
}