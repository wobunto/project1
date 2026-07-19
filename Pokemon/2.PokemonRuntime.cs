namespace Pokemongame
{       
    public class PokemonRuntime
    {
        //전투 중에 변하는 포켓몬 스탯
        private const int _maxMoveCount = 4;
        //기술 개수는 총 4개
        private MoveRuntime[] _moves = new MoveRuntime[_maxMoveCount];
        //기술 리스트
        public IReadOnlyList<MoveRuntime> CurrentMoves => _moves;
        
        public PokemonData Data {get;}
        public int Level {get; private set;}
        public int exp {get; private set;}
        public int CurrentHp {get; private set;}
        public int AttackStage{get; set;}
        //공격 랭크
        public int SpeedStage{get;set;}
        //속도 랭크
        public int MaxHp => Calculator.CalculateMaxHp(Data, Level);
        public int CurrentSpeed => Calculator.CalculateCurrentSpeed(Data, SpeedStage);

        public PokemonRuntime(PokemonData data, int level)
        {
            //초기화
            Data = data;
            
            Level = Math.Clamp(level,1,100);

            CurrentHp = MaxHp;
        }
        public void TakeDamage(int damage)
        {
            CurrentHp = Math.Clamp(CurrentHp - damage, 0, MaxHp);;
        }
        public void Heal(int amount)
        {
            CurrentHp = Math.Clamp(CurrentHp + amount, 0, MaxHp);
        }
        public bool IsFulledMoveSlot()
        {
            if(CurrentMoves.Count(move => move != null) >= _maxMoveCount)
                return true;
            
            return false;
        }   
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
        public void InsertMove(MoveData newMove, int slotToForget)
        {
            //기술 교체
            var _newMove = new MoveRuntime(newMove);
            _moves[slotToForget] = _newMove;
        }

    }
    public class MoveRuntime
    {
        //전투 중 기술의 데이터 pp 등
        public MoveData Data { get; }

        public int CurrentPP { get; private set; }

        public MoveRuntime(MoveData data)
        {
            Data = data;
            CurrentPP = data.BasePP;
        }
    }
}