namespace Pokemongame
{
    public interface IBattleParticipant
    {
        PokemonRuntime ActivePokemon { get; }
    }

    public interface ISwitchable
    {
        SwitchResult SwitchActive(int index);
        SwitchResult CanSwitchTo(int index);
    }
    public interface IInventoryHolder
    {
        bool HasItem(int itemKey);
        bool TryUseItem(int itemKey); // 성공 시 수량 -1, 0 되면 슬롯 제거
    }
}
