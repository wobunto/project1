using MyGame.Types;

namespace MyGame.Moves
{
    public class MoveData  
    {
         //스킬 기본 데이터. 파워. 정확도. pp
        public int Key { get; init; }                 //딕셔너리 key
        public required string Name { get; init; }
        public PokemonType Type { get; init; }
        public int Power { get; init; }
        public int Accuracy { get; init; }
        public int BasePP {get; init;}
        public int Priority { get; init; }
    }

    public static class SpecialMoves
    {
        public static readonly MoveData Struggle = new()
        {
            Key = 9999,
            Name = "발버둥",
            Type = PokemonType.Normal,
            Power = 50,
            Accuracy = 100,
            BasePP = 999, // PP 무한
            Priority = 0
        };
    }
}
