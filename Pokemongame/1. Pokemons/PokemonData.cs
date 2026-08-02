namespace Pokemongame
{
    public class PokemonData 
    {    // 포켓몬 데이터
        public required int Id {get; init;}
        public required string Name {get; init; }
        public required int BaseSpeed {get; init; }
        public required int BaseHp {get; init; }
        public required int BaseAttack {get; init; }

        public List<PokemonType> Types {get; init;} = new();
        public List<int> LearnMovesKeys {get; init;} = new();
        public List<LevelUpMove> LevelUpAutoMoves {get; init;} = new();
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