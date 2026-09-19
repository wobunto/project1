using MyGame.Controllers;
using MyGame.Pokemons;
using MyGame.Inputs;
using MyGame.Commands;

namespace MyGame.ControllerStates
{
    public class MenuState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            context.View.DisplayCommandMenu();
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            IBattlePokemon activePokemon = context.Player.ActivePokemon;

            switch (input.Value)
            {
                case 1:
                    if (!activePokemon.IsAbleMove())
                    {
                        context.PushState(StruggleSte);  
                        break;
                    }
                    context.PushState(AttackSte);
                    break;

                case 2:
                    context.PushState(ItemSte);
                    break;

                case 3:
                    GetSelectPokemonState(context);
                    break;

                case 4:
                    context.PushState(RunSte);
                    break;
            }   
        } 

        private void GetSelectPokemonState(PlayerController context)
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
    }
}