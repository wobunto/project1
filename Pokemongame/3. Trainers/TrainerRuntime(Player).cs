using MyGame.Trainers;

namespace Pokemongame
{
    public class PlayerRuntime : TrainerRuntime
    {
        public void SetFirstActivePokemon()
        {
            for(int i = 0; i < Party.Count; i++)
            {
                if(!Party[i]!.IsFainted)
                {
                    SetActivePokemon(i);
                    return;
                }
            }
            throw new InvalidOperationException("배틀 가능한 포켓몬이 없습니다.");
        }
        
        public void AddItem(int itemKey, int amount = 1)
        {
            _inventory.TryGetValue(itemKey, out var current);
            _inventory[itemKey] = current + amount;
        }
    }
}