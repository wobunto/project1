namespace Pokemongame
{
    public interface ISwitchable
    {
        PokemonRuntime ActivePokemon { get; }

        SwitchResult SwitchActive(int index);
        SwitchResult CanSwitchTo(int index);
    }
    public interface IInventoryHolder
    {
        IReadOnlyDictionary<int, int> Inventory { get; }

        bool HasItem(int itemKey);
        bool TryUseItem(int itemKey);    // 성공 시 수량 -1, 0 되면 슬롯 제거
        bool ConsumeItem(int itemKey, int amount);
    }
}
