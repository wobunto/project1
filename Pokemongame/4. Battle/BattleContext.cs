using System.Data;

namespace Pokemongame
{
    public class BattleContext 
    {
        public PlayerRuntime Player { get; }
        public EnemyRuntime Enemy { get; }

        public BattleContext(PlayerRuntime player, EnemyRuntime enemy)
        {
            Player = player;
            Enemy = enemy;
            //Trainer Runtime이 가지고 있는 모든 포켓몬 등록
        }
    }
}