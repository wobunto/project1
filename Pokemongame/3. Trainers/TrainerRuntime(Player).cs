namespace Pokemongame
{
    public class PlayerRuntime : TrainerRuntime
    {
        private void PartyArrange()
        {
            int insertIndex = 0;

            for(int i = 0; i < MaxPartySize; i++ )
            {
                if(_party[i] != null)
                {
                    _party[insertIndex] = _party[insertIndex];
                    insertIndex++;
                }
            }
            _nullSlotIndex = insertIndex;

            for(int i = insertIndex; i < MaxPartySize; i++)
            {
                _party[i] = null;
            }
        }

        public void SetFirstActivePokemon()
        {
            for(int i = 0; i < _nullSlotIndex; i++)
            {
                if(!_party[i]!.IsFainted)
                {
                    _activeIndex = i;
                    return;
                }
            }
            throw new InvalidOperationException("배틀 가능한 포켓몬이 없습니다.");
        }
        
        public void RemovePokemon(int index)
        {
            _party[index] = null;
            PartyArrange();
        }

        public void AddItem(int itemKey, int amount = 1)
        {
            _inventory.TryGetValue(itemKey, out var current);
            _inventory[itemKey] = current + amount;
        }
    }
}