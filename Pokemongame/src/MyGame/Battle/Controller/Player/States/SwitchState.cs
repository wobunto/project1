namespace MyGame.ControllerStates
{
      public class SwitchState : PlayerState
    {
        public override void Enter(PlayerController context)
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
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            //Debug("선택 단계에서 취소"); 디버그를 아직 구현 안했으니 대충 주석
            context.PopState();          
        }
        
        public override void Update(PlayerController context)
        {
            
        }
    }
}