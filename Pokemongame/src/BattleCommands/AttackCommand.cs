using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Types;
using MyGame.BattleStatus;
using MyGame.BattleCalculators;
using MyGame.BattleSystems;
using MyGame.Utilities;
using System.Reflection.Metadata;

namespace MyGame.Commands
{
    public class AttackCommand : IBattleCommand
    {
        private IBattlePokemon  _attacker;
        private IBattleTargetTrainer _defendTrainer;
        private MoveRuntime _move;

        public BattlePriority Priority {get; init;}  
        public int TrainerId { get; }
        public int AttackerSpeed => _attacker.CurrentSpeed;
        public event Action<string[]>? OnMessage;
        

        public AttackCommand(
            IBattlePokemon attacker,
            IBattleTargetTrainer defendTrainer,
            MoveRuntime move, int trainerId)
        {
            _attacker = attacker;
            _defendTrainer = defendTrainer;
            _move = move;
            TrainerId = trainerId;

            Priority = move.Data.Priority;
        }

        public IReadOnlyList<string> Execute()
        {
            if(_defendTrainer.ActivePokemon == null) 
                return ["상대 포켓몬이 없습니다."];
            
            var result = _attacker.CheckBeforeAction();
            if(!result.CanAct)
                return [$"{_attacker}은 행동할 수 없다."];
            
            if(!_move.TryConsumePP())
                throw new InvalidOperationException("attacker의 move pp가 0입니다."); //AttackState에서 pp체크가 안된 상황.
            
            if(!Utility.TryChance(_move.Data.Accuracy))
                 return ["빗나갔다!"];
            
            //데미지 계산 
            var defender = _defendTrainer.ActivePokemon;

            float typeMultiplier = 
                    TypeEffectiveness.CalculateTypeMultiplier(
                    _move.MoveType,
                    defender.Types
                    );

            int damage = BattleCalculator.CalculateDamage(
                    _attacker.CurrentAttackDamage,
                    _move.Data.Power,
                    defender.CurrentDefence,
                    typeMultiplier
                        );

            defender.TakeDamage(damage);

            //상태 이상 
            var effectMessages = new List<string>();
            var chance =_move.Data.EffectChance;

            foreach(var effect in _move.moveEffect)
            {
                if(Utility.TryChance(chance))
                {
                   effectMessages.Add(HandleEffect(effect));
                }
            }
     
            return BattleLog.LogBattleResult(
                    _attacker,
                    defender,
                    _move.Data,
                    damage,
                    typeMultiplier,
                    effectMessages
                    );

                //기술 부가 효과 실행
        }
        private string HandleEffect(MoveEffect effect)
        {
            var defender = _defendTrainer.ActivePokemon!;

            switch (effect)
            {
                // 1. 주요 상태이상 (Defender에게 적용)
               case MoveEffect.InflictBurn:
                    defender.TrySetEffectState(EffectState.Burn);
                    return BattleLog.LogSetStatus(EffectState.Burn);

                case MoveEffect.InflictPoison:
                    defender.TrySetEffectState(EffectState.Poison);
                    return BattleLog.LogSetStatus(EffectState.Poison);

                case MoveEffect.InflictBadlyPoison:
                    defender.TrySetEffectState(EffectState.Toxic);
                    return BattleLog.LogSetStatus(EffectState.Toxic);

                case MoveEffect.InflictParalysis:
                    defender.TrySetEffectState(EffectState.Paralysis);
                    return BattleLog.LogSetStatus(EffectState.Paralysis);

                case MoveEffect.InflictSleep:
                    defender.TrySetEffectState(EffectState.Sleep);
                    return BattleLog.LogSetStatus(EffectState.Sleep);

                case MoveEffect.InflictFreeze:
                    defender.TrySetEffectState(EffectState.Freeze);
                    return BattleLog.LogSetStatus(EffectState.Freeze);

                // 능력치 랭크 변화
                case MoveEffect.RaiseAttack:
                    _attacker.ModifyAttackStage(1);
                    return BattleLog.LogMyStatge(MoveEffect.RaiseAttack);

                case MoveEffect.LowerAttack:
                    defender.ModifyAttackStage(-1);
                    return BattleLog.LogEnemyStatge(MoveEffect.LowerAttack);

                case MoveEffect.RaiseSpeed:
                    _attacker.ModifySpeedStage(1);
                    return BattleLog.LogMyStatge(MoveEffect.RaiseSpeed);

                case MoveEffect.LowerSpeed:
                    defender.ModifySpeedStage(-1);
                    return BattleLog.LogEnemyStatge(MoveEffect.LowerSpeed);

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(effect),
                        effect,
                        "처리되지 않은 MoveEffect입니다."
                    );
            }
        }
    }
}