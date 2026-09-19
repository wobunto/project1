using MyGame.Moves;
using MyGame.Pokemons;
using MyGame.Trainers;
using MyGame.Items;

namespace MyGame.Commands
{
    public static class BattleCommandFactory
    {
        // 일반 공격 커맨드 생성
        public static AttackCommand CreateAttackCommand(IBattlePokemon attacker, IBattleTrainer defender, MoveRuntime move)
            => new AttackCommand(attacker, defender, move);
        
        public static AttackCommand CreateStruggleCommand(IBattlePokemon attacker, IBattleTrainer defender)
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
    }
}