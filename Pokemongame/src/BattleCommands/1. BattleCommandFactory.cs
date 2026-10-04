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

        public static AttackCommand CreateAttackCommand(IBattlePokemon attacker, IBattleTargetTrainer defender, MoveRuntime move,  int trainerId)
        {
            if (!move.HasPP)
                throw new InvalidOperationException("현재 PP가 0인 기술을 사용했습니다.");

            return new AttackCommand(attacker, defender, move, trainerId);
        }

        public static AttackCommand CreateStruggleCommand(IBattlePokemon attacker, IBattleTargetTrainer defender, int trainerId)
        {
            var struggleMove = MoveFactory.GetStruggle();
            return new AttackCommand(attacker, defender, struggleMove, trainerId);
        }

        public static SwitchCommand CreateSwitchCommand(IBattleTrainer trainer, int index)
            => new SwitchCommand(trainer, index);

        public static UseItemCommand CreateUseItemCommand(IBattleTrainer trainer, IItemTarget pokemon, ItemData item)
            => new UseItemCommand(trainer, pokemon, item);

        public static ExitCommand CreateExitCommand()
            => new ExitCommand();
    }
}