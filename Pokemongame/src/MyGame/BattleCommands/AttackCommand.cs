using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Types;
using MyGame.BattleCalculators;
using MyGame.BattleSystems;
using MyGame.Utilities;

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

        public void Execute()
        {
            if(_defendTrainer.ActivePokemon == null)
                throw new InvalidOperationException("defnederTrainer의 ActivePokemon이 null입니다.");
             
            var result = _attacker.CheckBeforeAction();

            if(!result.CanAct)
            {
                //result.Event 를 이용해서 관찰자에게 보내기.
                return;
            }  
            
            if(!_move.TryConsumePP())
                throw new InvalidOperationException("attacker의 move pp가 0입니다."); //AttackState에서 pp체크가 안된 상황.
            
            if(!Utility.TryChance(_move.Data.Accuracy))
            {
                //기술 빗나감 관찰자 보내기.
                return;
            }

            var _defender = _defendTrainer.ActivePokemon;
            
            float typeMultiplier = 
                    TypeEffectiveness.CalculateTypeMultiplier(
                    _move.MoveType,
                    _defender.Types
                    );

            int damage = BattleCalculator.CalculateDamage(
                        _attacker.CurrentAttackDamage,
                        _move.Data.Power,
                        _defender.CurrentDefence,
                        typeMultiplier
                        );

            _defender.TakeDamage(damage);

            BattleLog.LogBattleResult(
                _attacker,
                _defender,
                _move.Data,
                damage,
                typeMultiplier
                );
        }
    }
}