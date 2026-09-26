using MyGame.Pokemons;

namespace MyGame.Trainers
{
    public interface IBattleTargetTrainer
    {
        IBattlePokemon? ActivePokemon { get; }   // 호출할 때마다 "현재" 포켓몬을 반환
    }

    public interface IBattleTrainer : IBattleTargetTrainer
    {     
        IReadOnlyList<PokemonRuntime> Party { get; }
        IReadOnlyDictionary<int, int> Inventory {get; }

        int NameId {get;}

        bool TrySetActivePokemon(int index);

        bool CanBattle();
        bool CanSwitch(IBattlePokemon pokemon);
        bool HasItem(int itemKey);
        bool TryUseItem(int itemKey);    // 성공 시 수량 -1, 0 되면 슬롯 제거
    }
}