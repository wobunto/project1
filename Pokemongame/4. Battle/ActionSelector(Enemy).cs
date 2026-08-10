using Pokemongame;

namespace Pokemongame
{
    

    public class EnemyActionSelector : IActionSelector
    {
        private EnemyRuntime _enemy;
        private PlayerRuntime _player;

        public EnemyActionSelector(BattleContext context)
        {
            _enemy = context.Enemy;
            _player = context.Player;
        }

        public IBattleAction SelectAction()
        {
            return SelectMove(_enemy.ActivePokemon, _player.ActivePokemon);
            // 아직 미완성, 우선 무작위 공격만 

        }   

        public IBattleAction ForceSwitchAction()
        {
            int index = 0;
            for(int i = 0; i < _enemy.NullSlotIndex(); i++)
            {
                if(!_enemy.Party[i]!.IsFainted)             //알고리즘 만들기 전 우선 앞에 있는 적부터 Swap
                {
                    index = i;
                    break;
                }
            }
            
            return new SwitchAction(_enemy ,index);
        }

        public AttackAction SelectMove(PokemonRuntime enemyPokemon, PokemonRuntime playerPokemon)
        {    
            int firstEmptyindex = enemyPokemon.GetFirstEmptyIndex();   //제일 앞에 있는 null 칸)

            if (firstEmptyindex == 0)
            throw new InvalidOperationException("[적 포켓몬]이 사용할 기술이 없습니다.");

            var validMoves = enemyPokemon.CurrentMoves;

            int index = Random.Shared.Next(firstEmptyindex == -1 ? 4 : firstEmptyindex);    //Enemy는 항상 Moves가 앞에서부터 채워지므로
            
            MoveRuntime move = validMoves[index]!;

            return new AttackAction(enemyPokemon, playerPokemon, move);   
        }
    }
}
