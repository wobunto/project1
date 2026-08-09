namespace Pokemongame
{
    public class MoveData  
    {
         //스킬 기본 데이터. 파워. 정확도. pp
        public int key { get; init; }                 //딕셔너리 key
        public required string Name { get; init; }
        public PokemonType Type { get; init; }
        public int Power { get; init; }
        public int Accuracy { get; init; }
        public int BasePP {get; init;}
    }
}