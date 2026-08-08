namespace Pokemongame
{   
    public class TrainerRuntime : 
        ISwitchable,  
        IInventoryHolder   
    {
        protected readonly Dictionary<int, int> _inventory = new();  //itemKey -> count
        protected readonly PokemonRuntime[] _party = new PokemonRuntime[MaxPartySize];
        
        public IReadOnlyDictionary<int, int> Inventory => _inventory;
        public IReadOnlyList<PokemonRuntime> Party => _party;

        public PokemonRuntime ActivePokemon => _party[_activeIndex];

        protected const int MaxPartySize = 6;

        protected int _activeIndex;
    
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

        public bool HasItem(int itemKey) 
        => _inventory.TryGetValue(itemKey, out var count) && count > 0;

        public bool TryUseItem(int itemKey)
        => ConsumeItem(itemKey,1);

        public bool ConsumeItem(int itemKey, int amount)
        {
            if (!_inventory.TryGetValue(itemKey, out var current) || 
                current < amount)
                return false;

            _inventory[itemKey] = current - amount;
            if (_inventory[itemKey] == 0)
            {
                _inventory.Remove(itemKey);
                //아이템을 모두 사용하셨습니다. 라는 메세지 출력
            }
            return true;
        }
    }
}