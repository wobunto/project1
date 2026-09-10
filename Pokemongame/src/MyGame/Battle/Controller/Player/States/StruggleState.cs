namespace MyGame.ControllerStates
{
    public class StruggleState : PlayerState
    {
        public override void Enter(PlayerController context)
        {
            GameLog.Info($"{context.Player.ActivePokemon.Name}은 현재 사용할 수 있는 기술이 없다...");
        }
        
        public override void HandleInput(
            PlayerController context,
            Input input)
        {
            var attacker = context.Player.ActivePokemon;
            var defender = context.Enemy;
            var struggle = context.Player.ActivePokemon.GetStruggle();
            
            var attack = new AttackCommand(attacker, defender, struggle);
            context.FinishedTurn(attack);
        }
    }
}