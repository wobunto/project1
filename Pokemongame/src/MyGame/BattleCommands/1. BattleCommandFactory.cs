using MyGame.Moves;
using MyGame.Pokemons;
using MyGame.Trainers;
using MyGame.Items;

namespace MyGame.Commands
{
    public static class BattleCommandFactory
    {
        // Null Object는 static readonly로 단순하고 안전하게 캐싱
        public static readonly ErrorCommand Error = new();

        public static AttackCommand CreateAttackCommand(IBattlePokemon attacker, IBattleTargetTrainer defender, MoveRuntime move,  int TrainerId)
        {
            if (!move.HasPP)
                throw new InvalidOperationException("현재 PP가 0인 기술을 사용했습니다.");

            return new AttackCommand(attacker, defender, move, TrainerId);
        }

        public static AttackCommand CreateStruggleCommand(IBattlePokemon attacker, IBattleTargetTrainer defender, int TrainerId)
        {
            var struggleMove = MoveFactory.GetStruggle();
            return new AttackCommand(attacker, defender, struggleMove, TrainerId);
        }

        public static SwitchCommand CreateSwitchCommand(IBattleTrainer trainer, int index, int TrainerId)
            => new SwitchCommand(trainer, index, TrainerId);

        public static UseItemCommand CreateUseItemCommand(IBattleTrainer trainer, IItemTarget pokemon, ItemData item, int TrainerId)
            => new UseItemCommand(trainer, pokemon, item, TrainerId);

        public static ExitCommand CreateExitCommand()
            => new ExitCommand();
    }
}