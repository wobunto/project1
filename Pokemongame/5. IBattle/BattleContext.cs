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
        }
    }
}