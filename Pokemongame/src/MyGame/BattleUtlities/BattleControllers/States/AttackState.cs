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

            context.View.DisplayAttackMenu(context.Player.ActivePokemon!.CurrentMoves);
        }

        public override void HandleInput(IBattleStateContext context, Input input)
        {
            var _attacker = context.Player.ActivePokemon!;  
            var _defender = context.Enemy;        
            
            if(context.TryBackState(input)) return;
            
            int index = input.Value - 1;

            MoveUsageResult result = _attacker.TryGetUsableMove(index, out var move);

            if (result == MoveUsageResult.Success)
            {
            // 성공: 기술이 있고 PP도 있음
                var attack = BattleCommandFactory.CreateAttackCommand(_attacker,_defender, move!);
                context.FinishedTurn(attack);
                return;
            }

            context.View.DisplayMessage(MoveLog.GetErrorMessage(result));  //MoveLog에서 가져오는게 불편
        }

        private void GetStruggleState(IBattleStateContext context)
        {
            context.View.DisplayMessage($"{context.Player.ActivePokemon!.Name}은 현재 사용할 수 있는 기술이 없다...");

            var activePokemon = context.Player.ActivePokemon;
            var struggle = BattleCommandFactory.CreateStruggleCommand(activePokemon, context.Enemy);

            context.FinishedTurn(struggle);
            return;
        }
    }
}