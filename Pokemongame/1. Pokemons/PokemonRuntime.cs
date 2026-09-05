using System.Diagnostics.CodeAnalysis;
using MyGame.Logs;
using MyGame.Moves;
using MyGame.Types;
using MyGame.States;
using MyGame.BattleCalculators;

namespace MyGame.Pokemons
{
    public class PokemonRuntime : IBattlePokemon, IItemTarget
    {     //전투 중에 변하는 포켓몬 스탯
        public const int MaxMoveSlot = 4;         //기술 개수는 총 4개
        
        private readonly List<MoveRuntime> _moves = new(MaxMoveSlot);    //기술 리스트
        public IReadOnlyList<MoveRuntime> CurrentMoves => _moves.AsReadOnly();     //기술 리스트

        public PokemonData Data {get; private set;}
        public int Level {get; private set;}
        public int Exp {get; private set;}
        public int CurrentHp {get; private set;}
        public int AttackStage{get; set;}              //공격 랭크
        public int SpeedStage{get;set;}                //속도 랭크
        public EffectState CurrentEffectState {get; private set;}               //저림, 수면 등의 상태

        public string Name => Data.Name;
        public IReadOnlyList<PokemonType> Types => Data.Types;
        
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

            //_moves.Clear(); 필요한지 안한지 고민좀 해봐야 할 듯
        }

        public void TakeDamage(int damage) //데미지가 음수 일 수 있지만 재미 요소
            => CurrentHp = Math.Clamp(CurrentHp - damage, 0, MaxHp);  
        
        public void Heal(int amount)
            => CurrentHp = Math.Clamp(CurrentHp + amount, 0, MaxHp);

        public void FullHeal()
            => CurrentHp = MaxHp;
        
        public void Revive()           //꼭 필요한가..?
            => CurrentHp = MaxHp/2;
        
        public bool IsMoveSlotsFull()
            => _moves.Count >= MaxMoveSlot;

        public bool IsAbleMove()
        {
            for(int i = 0; i < CurrentMoves.Count; i++)
            {
                if(!(CurrentMoves[i].CurrentPP <= 0)) // 나중에 || move.UseMove 가 true 인지 추가
                    return true;     
            }
            return false;
        }
        
        public bool TryGetUseableMove(int index, out MoveRuntime? move)
        {
            if (index < 0 || index >= _moves.Count)
            {
                GameLog.Error("입력한 기술의 번호가 너무 크거나 작습니다.");

                move = null;
                return false;
            }

            if(_moves[index].CurrentPP <= 0)
            {
                move = null;
                return false;
            }

            move = _moves[index];
            return true;

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
        public bool TryAddMove(MoveData move)
        {
            if(_moves.Count >= MaxMoveSlot)
                return false;
            
            var runtime = new MoveRuntime(move);
            _moves.Add(runtime);

            return true;
        }

        public void InsertMove(MoveData movedata, int changeMoveSlot)
        {
             var move = new MoveRuntime(movedata); // 무브데이터로 새로운 런타임 초기화
             _moves[changeMoveSlot] = move;
        }

        public MoveRuntime GetStruggle()
        {
             MoveData struggle = MoveDatabase.Get(999);
            
             return new MoveRuntime(struggle);
        }
        
        public void AdvancePendingLevelUpMove() => _nextLevelUpMoveIndex++;

        public void SetEffectState(EffectState effect)
            => CurrentEffectState = effect;
    }
}
