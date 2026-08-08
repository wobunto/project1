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
        public MovePriority Priority {get; init;}
    }

    public enum MovePriority
    {
        ForcedLast = -1,   //무조건 후공
        SpeedBased = 0,   //속도 비교해서 선공
        ForcedFirst = 1,     //무조건 선공
        
    }
}