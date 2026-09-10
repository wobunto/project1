using MyGame.Controllers;
using MyGame.Commands;
using MyGame.Inputs;
using MyGame.Logs;

namespace MyGame.ControllerStates
{
     public class AttackState : PlayerState
    {
        public override void Enter(PlayerController context)
        {    
            var _attacker = context.Player.ActivePokemon; //상대 포켓몬이 교체하면 포켓몬이 바뀌니 상대방을 넣음.
            var _moves = _attacker.CurrentMoves;
            
            context.View.DisplayAttackMenu(_moves);
        }

        public override void HandleInput(PlayerController context, Input input)
        {
            var _attacker = context.Player.ActivePokemon;  
            var _defender = context.Enemy;        
            var _moves = _attacker.CurrentMoves;
            
            if(context.IsBack(input)) return;
            
            int index = input.Value -1;
            if (_attacker.TryGetUseableMove(index, out var move))
            {
            // 성공: 기술이 있고 PP도 있음
                Command attack = new AttackCommand(_attacker,
                                                   _defender, 
                                                   move!);
           
                context.FinishedTurn(attack);
                return;
            }
            GameLog.Warn("그 기술은 지금 사용할 수 없습니다.");
        }
    }

}