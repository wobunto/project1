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

        public string Name => Data.Name;
        public List<PokemonType> Types => Data.Types;

        public int MaxHp => Calculator.CalculateMaxHp(Data.BaseHp, Level);
        public int CurrentSpeed => Calculator.CalculateCurrentSpeed(Data.BaseSpeed, SpeedStage);
        public int CurrentAttack =>  Calculator.CalculateCurrentAttack(Data.BaseAttack, AttackStage);

        public bool IsFainted => CurrentHp <= 0;

        private int _nextLevelUpMoveIndex = 0;

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

        public void TakeDamage(int damage)
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

        public MoveRuntime SelectMove()
        {
            if (GetFirstEmptyIndex() == 0)
                throw new InvalidOperationException("[내 포켓몬]은 사용할 수 있는 기술이 없습니다.");

            this.LogChoiceMove();

            while (true)
            {
                int input = InputManager.GetSlotChoice(InputManager.MAX_MOVE_SLOTS);
                // GetSlotChoice가 이미 0~3 범위를 보장 

                if (TryGetMove(input, out MoveRuntime? move))
                    return move!;

                GameLog.Error("그 슬롯에는 기술이 없습니다.");
            }
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

        public bool TryGetPendingLevelUpMove(out MoveData? move)
        {
            var autoMoves = Data.LevelUpAutoMoves;

            if (_nextLevelUpMoveIndex >= autoMoves.Count || autoMoves[_nextLevelUpMoveIndex].Level != Level)
            {
                move = null;
                return false;
            }

            int key = autoMoves[_nextLevelUpMoveIndex].MoveKey;
            if (!MoveCategory.TryGet(key, out move))
                throw new InvalidOperationException($"기술 키 {key}가 존재하지 않습니다."); // 데이터 오류, 게임 상황 아님

            return true;
        }
        
        public void LearnMove(int moveKey, int? forgetSlotIndex = null)
        {
            int slot = forgetSlotIndex ?? GetFirstEmptyIndex();
            
            if (slot == -1)
                throw new InvalidOperationException("빈 슬롯이 없는데 교체할 슬롯이 지정되지 않았습니다.");

            InsertMove(moveKey, slot);
        }
        
        public void AdvancePendingLevelUpMove() => _nextLevelUpMoveIndex++;

        public MoveRuntime InsertMove(int moveKey, int slotIndex)
        {
            if(slotIndex < 0 || slotIndex >= _maxMoveCount)    
                throw new ArgumentOutOfRangeException(nameof(slotIndex));     //기술 교체
            
            if(!MoveCategory.TryGet(moveKey, out MoveData? data))   
                throw new InvalidOperationException($"기술 키 {moveKey}가 존재하지 않습니다.");
        
            var move = new MoveRuntime(data!);
            _moves[slotIndex] = move;
            return move;
        }
    }
}
