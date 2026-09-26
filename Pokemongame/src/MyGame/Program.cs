using MyGame.BattleCommanders;
using MyGame.BattleControllers;
using MyGame.Trainers;
using MyGame.Views;
using MyGame.BattleSystems;
using MyGame.Pokemons;
using MyGame.PokemonDatas;
using MyGame.Moves;
namespace MyGame
{
    class Program{
    const int Player = 0;

        static void Main(string[] args)
        {
            PokemonDatabase.LoadPokemonDatabase();
            MoveDatabase.LoadMoveDatabase();

            PlayerRuntime player = new(Player);
            EnemyRuntime enemy = new(1);
            ConsolePlayerView view = new();

            var rizard = PokemonFactory.Create(7, 50);
            var laflas = PokemonFactory.Create(6, 50);

            if(!player.TryAddPokemon(rizard))
                Console.WriteLine("설마 안되겠어");
            
            var move1 = MoveDatabase.Get(101);
            var move2 = MoveDatabase.Get(102);
            var move3 = MoveDatabase.Get(105);
            var move4 = MoveDatabase.Get(106);

            var move5 = MoveDatabase.Get(103);
            var move6 = MoveDatabase.Get(104);
            var move7 = MoveDatabase.Get(107);

            rizard.TryAddMove(move1);
            rizard.TryAddMove(move2);
            rizard.TryAddMove(move3);
            rizard.TryAddMove(move4);

            laflas.TryAddMove(move1);
            laflas.TryAddMove(move5);
            laflas.TryAddMove(move6);
            laflas.TryAddMove(move7);

            if(!enemy.TryAddPokemon(laflas))
                Console.WriteLine("설마 안되겠어");
        

            BattleController controller = new(player, enemy,view);
            PlayerCommander playerCommander = new(controller);
            AiCommander aiCommander = new(enemy, player);

            BattleSystem battle =  new(view);
            
            battle.StartTrianerBattle(playerCommander ,aiCommander);
        }
    }
}