using Microsoft.VisualBasic;

namespace Pokemongame
{   
    public class TrainerRuntime : 
        ISwitchable,  
        IInventoryHolder   
    {
        protected const int MaxPartySize = 6;
        public const int MaxMoveSlot = 4;

        protected readonly PokemonRuntime?[] _party = new PokemonRuntime?[MaxPartySize];
        public IReadOnlyList<PokemonRuntime?> Party => _party;

        public PokemonRuntime ActivePokemon => _party[_activeIndex]!;

        protected readonly Dictionary<int, int> _inventory = new();  //itemKey -> count
        public IReadOnlyDictionary<int, int> Inventory => _inventory;

        protected int _activeIndex = 0;
        protected int _nullSlotIndex;
       
        public int NullSlotIndex()
            => _nullSlotIndex;
        
        public bool CanBattle()
        {
            if(GetAlivePokemonCount() <= 0)
                return false;
            
            return true;
        }

        public void CapturePokemon(PokemonRuntime pokemon)
        {
            if(_nullSlotIndex == MaxPartySize)
                GameLog.Info("포켓몬 슬롯이 꽉 차있습니다.");
            else
                {
                    _party[_nullSlotIndex] = pokemon;
                    _nullSlotIndex++;
                }
        }

        public int GetAlivePokemonCount()
        {
            int count = 0;

            for(int i = 0; i < _nullSlotIndex; i++)
            {
                if(_party[i]!.IsFainted)
                {
                    count++;
                }
            }
            return count;
        }

        public SwitchResult CanSwitchTo(int index)
        {
            if (index < 0 || index >= MaxPartySize)
                throw new ArgumentOutOfRangeException(nameof(index)); // 게임 상황 아님, 버그

            if (index >= _nullSlotIndex)
                return SwitchResult.NoPokemonInSlot;

            if (_party[index]!.IsFainted)
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