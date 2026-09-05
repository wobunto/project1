using MyGame;
namespace MyGame
{
    class Program{
        static void Main(string[] args)
        {
            PokemonDatabase.LoadPokemonDatabase();
            MoveDatabase.LoadMoveDatabase();
            
            BattleSystem battle = new BattleSystem();
            
            var playerRuntime = new PlayerRuntime();
            var enemyRuntime = new EnemyRuntime();

            

            var rizard = PokemonFactory.Create(6 ,50);
            rizard.TryLearn(102);
            rizard.TryLearn(105);
            rizard.TryLearn(106);
            rizard.TryLearn(107);

            var laflas = PokemonFactory.Create(3,50);
            laflas.TryLearn(101);
            laflas.TryLearn(103);
            
            playerRuntime.CapturePokemon(rizard);
            enemyRuntime.CapturePokemon(laflas);
                
            battle.StartBattle(playerRuntime,enemyRuntime);
       }
    }
}