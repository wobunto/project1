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

        public static AttackCommand CreateAttackCommand(IBattlePokemon attacker, IBattleTargetTrainer defender, MoveRuntime move)
        {
            if (!move.HasPP)
                throw new InvalidOperationException("현재 PP가 0인 기술을 사용했습니다.");

            return new AttackCommand(attacker, defender, move, attacker.IsPlayers);
        }

        public static AttackCommand CreateStruggleCommand(IBattlePokemon attacker, IBattleTargetTrainer defender)
        {
            var struggleMove = MoveFactory.GetStruggle();
            return new AttackCommand(attacker, defender, struggleMove, attacker.IsPlayers);
        }

        public static SwitchCommand CreateSwitchCommand(IBattleTrainer trainer, int index)
            => new SwitchCommand(trainer, index, trainer.IsPlayer);

        public static UseItemCommand CreateUseItemCommand(IBattleTrainer trainer, IItemTarget pokemon, ItemData item)
            => new UseItemCommand(trainer, pokemon, item, trainer.IsPlayer);

        public static SkipTurnCommand CreateSkipTurnCommand(IBattleTrainer trainer, string reason)
            => new SkipTurnCommand(reason, trainer.IsPlayer);

        public static ExitCommand CreateExitCommand()
            => new ExitCommand();
    }
}