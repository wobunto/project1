using System.Dynamic;
using System.Reflection;

namespace Pokemongame
{
    public enum PokemonType 
    { 
        Normal = 0, 
        Fire, 
        Water, 
        Grass,
        Electric,
        Ground,
        Wind,
        Stone,
        Max
    }
    public static class TypeChart
    {
        private static int max => (int)PokemonType.Max;
        
        private static readonly float[,] _chart = new float[max,max];

        static TypeChart()
        {
            for(int i = 0; i<max; i++)
            {
                for(int j = 0; j<max; j++)
                    _chart[i,j] = 1.0f;        
            }
            
           PokemonType.Normal.Set(
                (PokemonType.Stone, 0.5f)
            );

            // Fire
            PokemonType.Fire.Set(
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Grass, 2.0f),
                (PokemonType.Stone, 0.5f)
            );

            // Water
            PokemonType.Water.Set(
                (PokemonType.Fire, 2.0f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Ground, 2.0f),
                (PokemonType.Stone, 2.0f)
            );

            // Grass
            PokemonType.Grass.Set(
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Ground, 2.0f),
                (PokemonType.Wind, 0.5f),
                (PokemonType.Stone, 2.0f)
            );

            // Electric
            PokemonType.Electric.Set(
                (PokemonType.Water, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Electric, 0.5f),
                (PokemonType.Ground, 0.0f),
                (PokemonType.Wind, 2.0f)
            );

            // Ground
            PokemonType.Ground.Set(
                (PokemonType.Fire, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Electric, 2.0f),
                (PokemonType.Wind, 0.0f),
                (PokemonType.Stone, 2.0f)
            );

            // Wind (Flying)
            PokemonType.Wind.Set(
                (PokemonType.Grass, 2.0f),
                (PokemonType.Electric, 0.5f),
                (PokemonType.Stone, 0.5f)
            );

            // Stone (Rock)
            PokemonType.Stone.Set(
                (PokemonType.Fire, 2.0f),
                (PokemonType.Ground, 0.5f),
                (PokemonType.Wind, 2.0f)
            );
        }
        private static void Set(this PokemonType attack, params(PokemonType defend, float multiplier)[] values)
            {
                foreach (var (defend, multiplier) in values)
                {
                    _chart[(int)attack, (int)defend] = multiplier;
                }
            }
        public static float GetTypeMultiplier(this PokemonType attacker, PokemonType deffender)
            =>_chart[(int)attacker, (int)deffender];
    }

    public class PokemonData 
    {
        // 포켓몬 데이터
        public required int Id{get; init;}
        public required string Name{get; init; }
        public required int BaseSpeed{get; init; }
        public required int BaseHp{get; init; }
        public required int BaseAttack{get; init; }
        public List<PokemonType> Types {get;} = new();
        public List<int> LearnMovesKeys = new();
        public List<LevelUpOutoMove> LevelUpOutoMoves {get;} = new();
        
    }    
    public class LevelUpOutoMove
    {
        public int Level {get; init;}
        public int MoveKey {get; init;}

        public LevelUpOutoMove(int level, int key)
        {
            Level = level;
            MoveKey = key;
        }
    }
    public class MoveData  
    {
         //스킬 기본 데이터. 파워. 정확도. pp
        public int key { get; init; } 
        //딕셔너리 key
        public required string Name { get; init; }
        public PokemonType Type { get; init; }
        public int Power { get; init; }
        public int Accuracy { get; init; }
        public int BasePP {get; init;}
        public int Level {get; init;}
    }
    public static class MoveCategory
    {
        private static readonly Dictionary<int, MoveData> _moves = new();
        public static IReadOnlyDictionary<int, MoveData> Move => _moves;
        public static void MoveDatabase()
        {
            Register(new MoveData { key = 1,Name = "몸통박치기", Type = PokemonType.Normal, Power = 40, Accuracy = 100, BasePP = 30 });    
            Register(new MoveData { key = 2,Name = "화염방사", Type = PokemonType.Fire, Power = 90, Accuracy = 100, BasePP = 10 });
            Register(new MoveData { key = 3,Name = "파도타기", Type = PokemonType.Water, Power = 90, Accuracy = 100, BasePP = 15 });
            Register(new MoveData { key = 4,Name = "솔라빔", Type = PokemonType.Grass, Power = 120, Accuracy = 100, BasePP = 10 });
            Register(new MoveData { key = 5,Name = "번개펀치", Type = PokemonType.Electric, Power = 75, Accuracy = 100, BasePP = 15 });
            Register(new MoveData { key = 6,Name = "공중날기", Type = PokemonType.Wind, Power = 90, Accuracy = 95, BasePP = 15 });
            Register(new MoveData { key = 7,Name = "스톤엣지", Type = PokemonType.Stone, Power = 75, Accuracy = 90, BasePP = 10 });
        }
        private static void Register(MoveData move) 
            => _moves[move.key] = move;

        public static MoveData Get(int key)
        {
            if (_moves.TryGetValue(key, out var move)) return move;
            throw new KeyNotFoundException($"[오류] 기술이 존재하지 않습니다.");
        }
       
    }
    public static class PokemonDatabase
    {
        // 리자몽, 라프라스 등의 원본 데이터를 반환하는 메서드
        public static PokemonData CreateCharizard()
        {
            var data = new PokemonData
            {
                Id = 003,
                Name = "리자몽",
                BaseHp = 220,
                BaseAttack = 84,
                BaseSpeed = 10
            };
            data.Types.AddRange(new[] {PokemonType.Fire});
            
            // 위에서 만든 기술 창고에서 기술을 안전하게 가져와서 레벨셋에 추가
            data.LearnMovesKeys.AddRange(new[]{001,002,005,006,007});
         
            data.AddMove(7, 2)
                .AddMove(14,6)
                .AddMove(24,5)
                .AddMove(35,7);

            return data;
        }

        public static PokemonData CreateLapras()
        {
            var data = new PokemonData 
            { 
                Id = 007,
                Name = "라프라스", 
                BaseHp = 355, 
                BaseAttack = 65, 
                BaseSpeed = 6
            };
            data.Types.Add(PokemonType.Water);

            data.LearnMovesKeys.AddRange(new[]{001,003});

            data.AddMove(9,2);

            return data;
        }
    }   
    public static class PokemonDataExtensions
    {
        public static PokemonData AddMove(this PokemonData data, int level, int moveKey)
        {
            data.LevelUpOutoMoves.Add(new LevelUpOutoMove(level, moveKey));
            return data; // 자기 자신을 반환하여 체이닝(연속 호출)이 가능하게 합니다.
        }
    }
}
