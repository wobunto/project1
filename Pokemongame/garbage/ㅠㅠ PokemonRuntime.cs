/*
using System.Diagnostics.CodeAnalysis;
namespace Pokemongame
{       
    public class PokemonRuntime 
    {     //전투 중에 변하는 포켓몬 스탯
        private const int _maxMoveCount = 4;         //기술 개수는 총 4개

        private readonly MoveRuntime?[] _moves = new MoveRuntime?[_maxMoveCount];     //기술 리스트

        public IReadOnlyList<MoveRuntime?> CurrentMoves => _moves;
        
        public PokemonData Data {get; private set;}
        public int Level {get; private set;}
        public int Exp {get; private set;}
        public int CurrentHp {get; private set;}
        public int AttackStage{get; set;}              //공격 랭크
        public int SpeedStage{get;set;}                //속도 랭크
        public EffectState CurrentEffectState {get; private set;}               //저림, 수면 등의 상태

        public string Name => Data.Name;
        public List<PokemonType> Types => Data.Types;
        
        public int MaxHp => Calculator.CalculateMaxHp(Data.BaseHp, Level);
        public int CurrentSpeed => Calculator.CalculateCurrentSpeed(Data.BaseSpeed, SpeedStage);
        public int CurrentAttack =>  Calculator.CalculateCurrentAttack(Data.BaseAttack, AttackStage);


        private int _nextLevelUpMoveIndex = 0;
        public bool IsFainted => CurrentHp <= 0;

        public PokemonRuntime(PokemonData data, int level)
        {
            Reinitialize(data, level);
        }
        
        [MemberNotNull(nameof(Data))]
        internal void Reinitialize(PokemonData data, int level)
        {
            Data = data;             //초기화  ?? throw new ArgumentNullException(nameof(data))
            Level = Math.Clamp(level,1,100);
            CurrentHp = MaxHp;

            for (int i = 0; i < _moves.Length; i++)
                _moves[i] = null; // 이전 종의 기술 잔재 제거
        }

    
        public void TakeDamage(int damage) //데미지가 음수 일 수 있지만 재미 요소
            => CurrentHp = Math.Clamp(CurrentHp - damage, 0, MaxHp);  
        

        public void Heal(int amount)
            => CurrentHp = Math.Clamp(CurrentHp + amount, 0, MaxHp);
        
        public bool IsMoveSlotsFull()
            => GetFirstEmptyIndex() == -1;

        public int GetFirstEmptyIndex()
        {
            //첫번째 빈슬롯을 반환. 꽉차있다면 -1
            for (int i = 0; i< _maxMoveCount; i++)
            {
                if(_moves[i] == null)
                    return i;
            }
            return -1;
        }

        public bool TryGetMove(int index, out MoveRuntime? move)
        {
            if (_moves[index] != null)
            {
                move = _moves[index];
                return true;
            }
            move = null;

            return false;
        }

        public bool TryGetPendingLevelUpMoveKey(out int key)
        {
            var autoMoves = Data.LevelUpAutoMoves;

            if (_nextLevelUpMoveIndex >= autoMoves.Count || 
                autoMoves[_nextLevelUpMoveIndex].Level != Level)
            {
                key = default;
                return false;
            }

            key = autoMoves[_nextLevelUpMoveIndex].MoveKey;

            return true;
        }
        
        public void InsertMove(MoveData movedata, int changeMoveSlot)
        {
             var Move = new MoveRuntime(movedata); // 무브데이터로 새로운 런타임 초기화
             _moves[changeMoveSlot] = Move;
        }
        
        public void AdvancePendingLevelUpMove() => _nextLevelUpMoveIndex++;

        public void SetEffectState(EffectState effect)
            => CurrentEffectState = effect;
    }
}
*/