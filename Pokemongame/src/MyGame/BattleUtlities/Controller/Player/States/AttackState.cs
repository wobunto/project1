using MyGame.Controllers;
using MyGame.Commands;
using MyGame.Inputs;
using MyGame.Moves;

namespace MyGame.ControllerStates
{
     public class AttackState : PlayerState
    {
        public override void Enter(PlayerController context)
        {            
            context.View.DisplayAttackMenu(context.Player.ActivePokemon.CurrentMoves);
        }

        public override void HandleInput(PlayerController context, Input input)
        {
            var _attacker = context.Player.ActivePokemon;  
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
    }
}