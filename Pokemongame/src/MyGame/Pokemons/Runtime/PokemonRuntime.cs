using System.Diagnostics.CodeAnalysis;
using MyGame.Moves;
using MyGame.Types;
using MyGame.States;
using MyGame.PokemonDatas;
using MyGame.BattleCalculators;
using MyGame.Utilities;

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
        
        public int MaxHp => BattleCalculator.CalculateMaxHp(Data.BaseHp, Level);
        public int CurrentSpeed => BattleCalculator.CalculateCurrentSpeed(Data.BaseSpeed, SpeedStage);
        public int CurrentAttack =>  BattleCalculator.CalculateCurrentAttack(Data.BaseAttack, AttackStage);

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
                if(!CurrentMoves[i].HasPP) // 나중에 || move.UseMove 가 true 인지 추가
                    return true;     
            }
            return false;
        }
        
        public MoveUsageResult TryGetUsableMove(int index, out MoveRuntime? move)
        {
            move = null;

            if (!Utility.IsValidIndex(index, MaxMoveSlot))
                return MoveUsageResult.InvalidSlot;

            if(index >= _moves.Count)   //기술의 개수보다 더 큰 인덱스.
                return MoveUsageResult.EmptySlot;

            if(_moves[index].CurrentPP <= 0)
                 return MoveUsageResult.NoPP;

            move = _moves[index];
            return MoveUsageResult.Success;
        }

        public bool TryGetPendingLevelUpMoveKey(out int key)   //일정 레벨이 되었는지 판단하는 메서드인데, 무브 데이터 쪽에 있어도 될지도
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
            if(!Utility.IsValidIndex(changeMoveSlot, MaxMoveSlot))
                throw new InvalidOperationException("현재 잘못된 기술 슬롯을 선택했습니다..");
            
            var move = new MoveRuntime(movedata); // 무브데이터로 새로운 런타임 초기화

            _moves.Add(move);
        }

        public void AdvancePendingLevelUpMove() => _nextLevelUpMoveIndex++;

        public void SetEffectState(EffectState effect)   //포켓몬은 화상 상태에서 감전으로 바뀌지 않으니 try로 바꿔야 함
            => CurrentEffectState = effect;
    }
}
