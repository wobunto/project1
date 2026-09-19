using MyGame.Moves;
using MyGame.Pokemons;
using MyGame.Trainers;
using MyGame.Items;

namespace MyGame.Commands
{
    public static class BattleCommandFactory
    {
        private static ErrorCommand? _cashedErrorCommand;
        // 일반 공격 커맨드 생성
        public static AttackCommand CreateAttackCommand(IBattlePokemon attacker, IBattleTarget defender, MoveRuntime move)
            => new AttackCommand(attacker, defender, move);
        
        public static AttackCommand CreateStruggleCommand(IBattlePokemon attacker, IBattleTarget defender)
        {
            var struggleMove = MoveFactory.GetStruggle();
            return new AttackCommand(attacker, defender, struggleMove);
        }

        public static SwitchCommand CreateSwitchCommand(IBattleTrainer trainer, int index)
            => new SwitchCommand(trainer, index);

        public static UseItemCommand CreateUseItemCommand(IBattleTrainer trainer, IItemTarget pokemon, ItemData item)
            => new UseItemCommand(trainer, pokemon, item);
        
        public static SkipTurnCommand CreateSkipTurnCommand(string reason)
            => new SkipTurnCommand(reason);

        public static ExitCommand CreateExitCommand()
            => new ExitCommand();    

        public static ErrorCommand CreateErrorCommand()
        {
            return _cashedErrorCommand ??= new ErrorCommand();
        }
    }
}