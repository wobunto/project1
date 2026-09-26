using MyGame.Types;
using MyGame.BattleSystem;

namespace MyGame.Moves
{
    public class MoveData  
    {
         //스킬 기본 데이터. 파워. 정확도. pp
        public int Key { get; init; }
        public required string Name { get; init; }
        public PokemonType Type { get; init; }
        public int Power { get; init; }
        public int Accuracy { get; init; }
        public int BasePP { get; init; }

        public BattlePriority Priority { get; init; }
        public required List<MoveEffect> Effects { get; init; }
        public int EffectChance { get; init; } = 100;                //기본 값 100
    }
}
