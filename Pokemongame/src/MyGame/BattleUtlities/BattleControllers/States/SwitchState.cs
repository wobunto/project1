using MyGame.BattleControllers;
using MyGame.Commands;
using MyGame.Inputs;
using MyGame.Moves;

namespace MyGame.ControllerStates
{
    public class SwitchState : PlayerState
    {
        public override void Enter(IBattleStateContext context)
        {
            var player = context.Player;
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    var switchCmd = BattleCommandFactory.CreateSwitchCommand(player, index);
                    context.FinishedTurn(switchCmd);
                },
                filter: player.CanSwitch, // 도메인에 위임된 규칙
                canCancel: !context.ForceSwitch
            );
            
            context.PushState(selectState);      
        }

        public override void HandleInput(IBattleStateContext context, Input input)
        {
            
        }
    
    }
}