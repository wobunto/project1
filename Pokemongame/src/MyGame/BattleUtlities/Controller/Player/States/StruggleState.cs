using MyGame.Controllers;
using MyGame.Commands;
using MyGame.Inputs;
using MyGame.Moves;

namespace MyGame.ControllerStates
{
    public class StruggleState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            context.View.DisplayMessage($"{context.Player.ActivePokemon.Name}은 현재 사용할 수 있는 기술이 없다...");
            
            var attacker = context.Player.ActivePokemon;
            var defender = context.Enemy;
            var struggleMove = MoveFactory.GetStruggle();

            var attack = new AttackCommand(attacker, defender, struggleMove);
            context.FinishedTurn(attack);
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
        }
    }
}