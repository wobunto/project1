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
            //관찰자에게 알림?
        
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
            var attacker = context.Player.ActivePokemon!;  
            var defender = context.Enemy;        
            
            if(context.TryBackState(input)) return;
            
            int index = input.Value - 1;

            MoveUsageResult result = attacker.TryGetUsableMove(index, out var move);

            if (result == MoveUsageResult.Success)
            {
            // 성공: 기술이 있고 PP도 있음
                var attack = BattleCommandFactory.CreateAttackCommand(attacker,defender, move!, context.Player.NameId);
                context.FinishedTurn(attack);
                return;
            }  

            context.View.DisplayMessage(MoveLog.GetErrorMessage(result));  //MoveLog에서 가져오는게 불편
        }

        private void GetStruggleState(IBattleStateContext context)
        {
            //뷰 호출

            var activePokemon = context.Player.ActivePokemon!;
            var struggle = BattleCommandFactory.CreateStruggleCommand(activePokemon, context.Enemy, context.Player.NameId);

            context.FinishedTurn(struggle);
            return;
        }
    }
}