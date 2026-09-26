using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Types;
using MyGame.BattleCalculators;
using MyGame.BattleSystem;

namespace MyGame.Commands
{
    public class AttackCommand : IBattleCommand
    {
        public BattlePriority Priority {get; init;}  


        public int AttackerSpeed { get;}

        private IBattlePokemon  _attacker;
        private IBattleTargetTrainer _defendTrainer;
        private MoveRuntime _move;

        public AttackCommand(
            IBattlePokemon attacker,
            IBattleTargetTrainer defendTrainer,
            MoveRuntime move)
        {
            _attacker = attacker;
            _defendTrainer = defendTrainer;
            _move = move;
            AttackerSpeed = _attacker.CurrentSpeed;

            Priority = move.Data.Priority;
        }

        public void Execute()
        {
            if(_defendTrainer.ActivePokemon == null)
                throw new InvalidOperationException("defnederTrainer의 ActivePokemon이 null입니다.");

            var _defender = _defendTrainer.ActivePokemon;
            
            float typeMultiplier = 
                    TypeEffectiveness.CalculateTypeMultiplier(
                    _move.MoveType,
                    _defender.Types
                );

            int currentAttack = _attacker.CurrentAttackDamage;

            int damage = BattleCalculator.CalculateDamage(
                        currentAttack,
                        typeMultiplier
                        );

            _defender.TakeDamage(damage);

            _attacker.LogBattleResult(
                _defender,
                _move.Data,
                damage,
                typeMultiplier
            );
        }
    }
}