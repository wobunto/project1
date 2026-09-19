using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Types;
using MyGame.BattleCalculators;
using MyGame.BattleSystem;

namespace MyGame.Commands
{
    public class AttackCommand : Command
    {
        private IBattlePokemon  _attacker;
        private IBattleTrainer _defendTrainer;
        private MoveRuntime _move;

        public AttackCommand(
            IBattlePokemon attacker,
            IBattleTrainer defendTrainer,
            MoveRuntime move)
        {
            _attacker = attacker;
            _defendTrainer = defendTrainer;
            _move = move;
        }

        public override void Execute()
        {
            var _defender = _defendTrainer.ActivePokemon;
            
            _move.TryConsumePP();

            float typeMultiplier = 
                    TypeEffectiveness.CalculateTypeMultiplier(
                    _move.MoveType,
                    _defender.Types
                );

            int currentAttack = _attacker.CurrentAttack;

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