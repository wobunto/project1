namespace Pokemongame
{
    public class TrainerRuntime : IBattleParticipant, ISwitchable,  IInventoryHolder
    {
        private readonly Dictionary<int, int> _inventory = new();  //itemKey -> count
        private readonly PokemonRuntime[] _party = new PokemonRuntime[MaxPartySize];
    
        public IReadOnlyList<PokemonRuntime> Party => _party;

        public PokemonRuntime ActivePokemon => _party[_activeIndex];

        private const int MaxPartySize = 6;

        private int CurrentSlotIndex;
        private int _activeIndex;

        public SwitchResult CanSwitchTo(int index)
        {
            if (index < 0 || index >= MaxPartySize)
                throw new ArgumentOutOfRangeException(nameof(index)); // 게임 상황 아님, 버그

            if (_party[index] is null)
                return SwitchResult.NoPokemonInSlot;

            if (_party[index].IsFainted)
                return SwitchResult.Fainted;

            return SwitchResult.Success;
        }

        public SwitchResult SwitchActive(int index)
        {
            var result = CanSwitchTo(index);
            if (result == SwitchResult.Success)
                _activeIndex = index;
            return result;
        }

        public void CapturePokemon(PokemonRuntime pokemon)
        {
            if(CurrentSlotIndex == MaxPartySize)
                GameLog.Info("포켓몬 슬롯이 꽉 차있습니다.");
            else
                {
                    _party[CurrentSlotIndex] = pokemon;
                    CurrentSlotIndex++;
                }
        }

        public bool HasItem(int itemKey) 
        => _inventory.TryGetValue(itemKey, out var count) && count > 0;

        public bool TryUseItem(int itemKey)
        {
            if (!HasItem(itemKey)) return false;

            _inventory[itemKey]--;
            if (_inventory[itemKey] == 0)
                _inventory.Remove(itemKey);

            return true;
        }

        public void AddItem(int itemKey, int amount = 1)
        {
            _inventory.TryGetValue(itemKey, out var current);
            _inventory[itemKey] = current + amount;
        }
    }
}