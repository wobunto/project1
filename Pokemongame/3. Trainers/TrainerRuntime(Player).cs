namespace Pokemongame
{
    public class PlayerRuntime : TrainerRuntime
    {
       

        public void AddItem(int itemKey, int amount = 1)
        {
            _inventory.TryGetValue(itemKey, out var current);
            _inventory[itemKey] = current + amount;
        }
    }

    public class EnemyRuntime : TrainerRuntime
    {
        //아직 미구현
    }
}