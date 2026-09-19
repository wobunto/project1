using MyGame.Controllers;
using MyGame.Commands;
using MyGame.Inputs;

namespace MyGame.ControllerStates
{
    public class SwitchState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            context.View.DisplayPartyMenu(context.Player.Party);
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            var player = context.Player;
            // 선택 완료 시 실행될 액션을 람다로 전달  (AI 도움)
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    var switchCmd = new SwitchCommand(player, index);
                    context.FinishedTurn(switchCmd);
                },
                filter: player.CanSwitch, // 도메인에 위임된 규칙
                canCancel: !context.ForceSwitch
            );
            
            context.PushState(selectState);        
        }

        public override void Resume(PlayerController context)
        {
              context.PopState(); 
        }
    }
}