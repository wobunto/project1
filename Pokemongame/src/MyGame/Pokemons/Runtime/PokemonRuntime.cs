using System.Diagnostics.CodeAnalysis;
using MyGame.Moves;
using MyGame.Types;
using MyGame.States;
using MyGame.PokemonDatas;
using static MyGame.Rules.PokemonRules;
using MyGame.BattleCalculators;
using MyGame.Utilities;

namespace MyGame.Pokemons
{  
    public class PokemonRuntime : IBattlePokemon, IItemTarget //전투 중에 변하는 포켓몬 스탯 
    {        // 랭크 및 제약이 있는 스탯들의 백킹 필드
        private int _attackStage;
        private int _speedStage;
        private int _level;
        private int _currentHp;

        private readonly List<MoveRuntime> _moves = new(MaxMoveSlot);    //기술 리스트
        // --------------------------------------------------
        // [2] 기본 정보 Base Data
        // --------------------------------------------------
        public PokemonData Data {get; private set;}
        public string Name => Data.Name;
        public int Exp {get; private set;}
        public IReadOnlyList<PokemonType> Types => Data.Types;
        public IReadOnlyList<MoveRuntime> CurrentMoves => _moves.AsReadOnly();     

        // --------------------------------------------------
        // [3] 현재 전투 상태 
        // --------------------------------------------------
        public EffectState CurrentEffectState {get; private set;}               //저림, 수면 등의 상태
        public bool IsFainted => CurrentHp <= 0;

        public int Level 
        {
            get => _level; 
            private set => _level = Math.Clamp(value, 1, 100);
        }

        public int CurrentHp
        {
            get => _currentHp; 
            private set => _currentHp = Math.Clamp(value, 0, MaxHp);
        }
        //   랭크
        public int AttackStage
        {
            get => _attackStage; 
            private set => _attackStage = Math.Clamp(value,0,6);
        }   

        public int SpeedStage    
        {
            get => _speedStage; 
            private set => _speedStage = Math.Clamp(value,0,6);
        }    
        // --------------------------------------------------
        // [4] 최종 계산 스탯 
        // --------------------------------------------------
        public int MaxHp 
            => BattleCalculator.CalculateMaxHp(Data.BaseHp, Level);

        public int CurrentSpeed 
            => BattleCalculator.CalculateCurrentSpeed(Data.BaseSpeed, SpeedStage);

        public int CurrentAttackDamage 
            =>  BattleCalculator.CalculateCurrentAttack(Data.BaseAttack, AttackStage);

        public PokemonRuntime(PokemonData data, int level)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));
            
            Data = data;            
            _level = level;
            CurrentHp = MaxHp;
        }
        // ==================================================
        // [5] 전투 생명주기 및 HP (Combat LifeCycle & HP)
        // ==================================================
        public void TakeDamage(int damage) //데미지가 음수 일 수 있지만 재미 요소
        {
            if (IsFainted) return;

            CurrentHp -= damage;
                
            if (IsFainted)    // 기절 시 필요한 부가 처리 (랭크 초기화 등)
                OnFainted();
        }
        
        public bool TryHeal(int amount)
        {
            if(IsFainted)
                return false;

            if(CurrentHp >= MaxHp)
                return false;

            CurrentHp += amount;
            return true;
        }

        public bool TryFullHeal()
        {
            if(IsFainted)
                return false;

            if (CurrentHp >= MaxHp && CurrentEffectState == EffectState.None)
                return false;

            CurrentHp = MaxHp;
            SetEffectState(EffectState.None);  // 상태를 정상으로

            return true;
        }

        public bool TryRevive() //나중에 인자로 풀피로 회복할 것인지 반피로 회복할 것인지 Enum으로 받아서 처리.      
        {
            if (!IsFainted)
                return false;

            CurrentHp = MaxHp/2;
            return true;
        }
        // ==================================================
        // [6] 랭크 및 상태 이상 (Stages & Status Effects)
        // ==================================================
        public void ModifyAttackStage(int amount) 
            => AttackStage += amount; // 

        public void ModifySpeedStage(int amount) 
            => SpeedStage += amount; // 
        
        public bool TrySetEffectState(EffectState effect)
        {
            if (!(CurrentEffectState == EffectState.None))
                return false;

            SetEffectState(effect);
            return true;
        }
        // ==================================================
        // [7] 전투 행동 판정 (Battle Actions & Usability)
        // ==================================================
        public bool HasAnyUsableMove()
        {
            for(int i = 0; i < _moves.Count; i++)
            {
                if(_moves[i].HasPP) // 나중에 || move.UseMove 가 true 인지 추가
                    return true;     
            }
            return false;
        }
        
        public MoveUsageResult TryGetUsableMove(int index, out MoveRuntime? move)
        {
            move = null;

            if (!Utility.IsValidIndex(index, MaxMoveSlot))
                return MoveUsageResult.InvalidSlot;

            if(index >= _moves.Count)   
                return MoveUsageResult.EmptySlot;

            if(_moves[index].CurrentPP <= 0)
                 return MoveUsageResult.NoPP;

            move = _moves[index];
            return MoveUsageResult.Success;
        }
        // ==================================================
        // [8] 기술 관리 및 인벤토리 (Move Management)
        // ==================================================
        public bool TryAddMove(MoveData move)
        {
            if(_moves.Count >= MaxMoveSlot)
                return false;
            
            var runtime = new MoveRuntime(move);
            _moves.Add(runtime);

            return true;
        }

        public void ReplaceMove(MoveData movedata, int changeMoveSlot)
        {
            if(!Utility.IsValidIndex(changeMoveSlot, _moves.Count))
                throw new InvalidOperationException("현재 잘못된 기술 슬롯을 선택했습니다..");
            
            var move = new MoveRuntime(movedata); // 무브데이터로 새로운 런타임 초기화

            _moves[changeMoveSlot] = move;
        }
        // ==================================================
        // [9] 내부 보조 로직 (Private Helpers)
        // ==================================================
        private void SetEffectState(EffectState effect)  
            => CurrentEffectState = effect;
        
        private void OnFainted()
        {
            SetEffectState(EffectState.None);
            AttackStage = Reset;
            SpeedStage = Reset;
        }
    }
}

  /*
        [MemberNotNull(nameof(Data))] 
        internal void Reinitialize(PokemonData data, int level)      필요한지 안한지 고민좀 해봐야 할 듯
        {
            Data = data;             //초기화  ?? throw new ArgumentNullException(nameof(data))
            Level = Math.Clamp(level,1,100);
            CurrentHp = MaxHp;

            _moves.Clear(); 
        }
        */
        
