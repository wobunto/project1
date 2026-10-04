using MyGame.BattleControllers;
using MyGame.Commands;
using MyGame.Inputs;
using MyGame.Moves;

namespace MyGame.ControllerStates
{
    public class AttackState : PlayerState
    {
        public override void Enter(IBattleStateContext context)
        {    
            var activePokemon = context.Player.ActivePokemon!;

            if (!activePokemon.HasAnyUsableMove())
            {
                GetStruggleState(context);
                return;
            }
        }

        public override void HandleInput(IBattleStateContext context, Input input)
        {
            if(context.TryBackState(input)) return;

            var attacker = context.Player.ActivePokemon!;  
            var defender = context.Enemy;        
             
            int select = input.Value - 1;

            MoveUsageResult result = attacker.TryGetUsableMove(select, out var move);

            if (result == MoveUsageResult.Success)
            {
            // 성공: 기술이 있고 PP도 있음
                var attack = BattleCommandFactory.CreateAttackCommand(attacker,defender, move!, context.Player.NameId);
                context.FinishedTurn(attack);
                return;
            }  
        }

        private void GetStruggleState(IBattleStateContext context)
        {

            var activePokemon = context.Player.ActivePokemon!;
            var struggle = BattleCommandFactory.CreateStruggleCommand(activePokemon, context.Enemy, context.Player.NameId);

            context.FinishedTurn(struggle);
            return;
        }
    }
}