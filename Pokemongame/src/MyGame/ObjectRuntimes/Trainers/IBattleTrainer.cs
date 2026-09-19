using MyGame.Pokemons;

namespace MyGame.Trainers
{
    public interface IBattleTarget
    {
        IBattlePokemon ActivePokemon { get; }   // 호출할 때마다 "현재" 포켓몬을 반환
    }

    public interface IBattleTrainer : IBattleTarget
    {     
        IReadOnlyList<PokemonRuntime> Party { get; }
        IReadOnlyDictionary<int, int> Inventory {get; }

        void SetActivePokemon(int index);

        bool CanBattle();
        bool HasItem(int itemKey);
        bool TryUseItem(int itemKey);    // 성공 시 수량 -1, 0 되면 슬롯 제거
        bool ConsumeItem(int itemKey, int amount);
        bool CanSwitch(IBattlePokemon pokemon);
    }
}