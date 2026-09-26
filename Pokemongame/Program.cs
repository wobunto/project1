using MyGame;
using MyGame.BattleCommanders;
using MyGame.BattleControllers;
using MyGame.Trainers;
using MyGame.Views;
using MyGame.BattleSystems;
namespace MyGame
{
    class Program{
    const int Player = 0;

        static void Main(string[] args)
        {
            TrainerRuntime player = new(Player);
            TrainerRuntime enemy = new(1);
            ConsolePlayerView view = new();

            BattleController controller = new(player, enemy,view);
            PlayerCommander playerCommander = new(controller);
            AiCommander aiCommander = new(enemy, player);

            BattleSystem battle =  new(view);
            
            battle.StartTrianerBattle(playerCommander ,aiCommander);
        }
    }
}