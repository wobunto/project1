using MyGame.Pokemons;
using MyGame.Types;
using MyGame.Moves;
using MyGame.Trainers;
using MyGame.Items;
using MyGame.BattleCalculators;
using MyGame.BattleSystem;
using MyGame.Logs;

namespace MyGame.Commands
{    
    public abstract class Command
    {
        public abstract void Execute();
    }

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

            int damage = Calculator.CalculateDamage(
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

    public class UseItemCommand : Command
    {
        private const int _useOne = 1;
        private IBattleTrainer _trainer;
        private IItemTarget _pokemon;
        private ItemData _item;
        // 나중에 야생 포켓몬을 잡을 경우, 야생 포켓몬도 포켓몬런타임으로 받아야 함.(hp가 적을수록 혹은 특수 타입일 경우, 포획률을 조정해야 하기 때문)
        // 아직 미구현
        public UseItemCommand(
            IBattleTrainer trainer,
            IItemTarget pokemon,
            ItemData item
            )
        {
            _trainer = trainer;
            _pokemon = pokemon;
            _item = item;
        }

        public override void Execute()
        {
            _trainer.ConsumeItem(_item.Key, _useOne);
            
            IItemEffect effect = ItemEffectFactory.Create(_item.Effect);
            
            effect.Apply(_pokemon, _item.EffectValue);
              
            }
        }
    

    public class SwitchCommand : Command
    {
        private IBattleTrainer _trainer;
        private int _index;

        public SwitchCommand(
            IBattleTrainer trainer,
            int index)
        {
            _trainer = trainer;
            _index = index;
        }

        public override void Execute()
        {
            _trainer.SetActivePokemon(_index);
        }
    }



    public class SkipTurnCommand : Command
    {
        private string _reason;

        public SkipTurnCommand(string reason)
        {
            _reason = reason;
        }

        public override void Execute()
        {
            // 스킵하는 사유 출력
        }
    }

    public class ExitCommand : Command
    {
        public override void Execute()
        {
            // 도망 실행
        }
    }

    public class ErrorCommand : Command
    {
        public override void Execute()
        {
            GameLog.Error("액션이 선택되지 않았습니다!");
        }
    }
}
