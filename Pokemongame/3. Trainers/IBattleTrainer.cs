using MyGame.Pokemons;
using MyGame.Moves;

namespace MyGame.Trainers
{
    public interface IBattleTrainer 
    {     
        IReadOnlyList<PokemonRuntime> Party { get; }
        IBattlePokemon ActivePokemon { get; }
        IReadOnlyDictionary<int, int> Inventory {get; }

        void SetActivePokemon(int index);

        bool CanBattle();
        bool HasItem(int itemKey);
        bool TryUseItem(int itemKey);    // 성공 시 수량 -1, 0 되면 슬롯 제거
        bool ConsumeItem(int itemKey, int amount);
        bool CanSwitch(IBattlePokemon pokemon);
    }


}