namespace Pokemongame
{
    public class PlayerRuntime : TrainerRuntime
    {
        private int CurrentSlotIndex;

       

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

        
        public void AddItem(int itemKey, int amount = 1)
        {
            _inventory.TryGetValue(itemKey, out var current);
            _inventory[itemKey] = current + amount;
        }
    }
}