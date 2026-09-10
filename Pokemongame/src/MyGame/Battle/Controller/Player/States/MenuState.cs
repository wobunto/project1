using MyGame.Controllers;
using MyGame.Pokemons;
using MyGame.Inputs;


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
                    context.PushState(SwitchSte);
                    break;

                case 4:
                    context.PushState(RunSte);
                    break;
            }   
        }  
    }
}