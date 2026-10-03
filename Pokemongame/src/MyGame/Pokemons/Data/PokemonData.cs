using MyGame.Types;

namespace MyGame.PokemonDatas
{
    public class PokemonData 
    {    // 포켓몬 데이터
        public required int Id {get; init;}
        public required int BaseSpeed {get; init; }
        public required int BaseHp {get; init; }
        public required int BaseAttack {get; init; }
        public required int BaseDefense {get; init; }

        public List<PokemonType> Types {get;} = new();
        public List<int> LearnMovesKey {get;} = new();
        public List<LevelUpMove> LevelUpAutoMoves {get;} = new();
    }
    
    public record struct LevelUpMove(int Level, int MoveKey);

    public static class PokemonDataExtensions
    {
        public static PokemonData AddMove(this PokemonData data, int level, int moveKey)
        {
            data.LevelUpAutoMoves.Add(new LevelUpMove(level, moveKey));
            return data; // 자기 자신을 반환하여 체이닝(연속 호출)이 가능하게 합니다.
        }
    }
}