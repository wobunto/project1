using MyGame.BattleCommanders;
using MyGame.BattleControllers;
using MyGame.Trainers;
using MyGame.Views;
using MyGame.BattleSystems;
using MyGame.Pokemons;
using MyGame.PokemonDatas;
using MyGame.Moves;
using MyGame.Rules;
using MyGame.Items;
namespace MyGame
{
    class Program{

        static void Main(string[] args)
        {
            // ==========================================
            // Database
            // ==========================================
            PokemonDatabase.LoadPokemonDatabase();
            MoveDatabase.LoadMoveDatabase();
            ItemDatabase.LoadItemDatabase();
            PokemonDatabase.LoadPokemonDatabase();
            MoveDatabase.LoadMoveDatabase();
            ItemDatabase.LoadItemDatabase();

            // ==========================================
            // Trainer
            // ==========================================
            PlayerRuntime player = new(PokemonRules.PlayerId);
            EnemyRuntime enemy = new(1);

            ConsolePlayerView view = new();
            // ==========================================
            // Player Pokemon
            // ==========================================

            // 리자몽
            var rizard = PokemonFactory.Create(
                6,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.FlameThrower,
                    MoveRules.ThunderPunch,
                    MoveRules.Fly
                });
            // 피카츄
            var pikachu = PokemonFactory.Create(
                25,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.ThunderPunch,
                    MoveRules.Thunderbolt,
                    MoveRules.QuickAttack
                });
            // 이상해씨
            var bulbasaur = PokemonFactory.Create(
                1,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.SolarBeam,
                    MoveRules.LeafBlade,
                    MoveRules.MegaDrain
                });

            // ==========================================
            // Enemy Pokemon
            // ==========================================

            // 라프라스
            var laflas = PokemonFactory.Create(
                131,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.Surf,
                    MoveRules.AquaTail,
                    MoveRules.IcePunch
                });

            // 꼬마돌
            var geodude = PokemonFactory.Create(
                74,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.StoneEdge,
                    MoveRules.RockSlide,
                    MoveRules.Earthquake
                });

            // 가디
            var growlithe = PokemonFactory.Create(
                58,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.FlameThrower,
                    MoveRules.FirePunch,
                    MoveRules.QuickAttack
                });

            // ==========================================
            // Party 구성
            // ==========================================

            if (!player.TryAddPokemon(rizard))
                Console.WriteLine("플레이어 파티에 리자몽을 추가할 수 없습니다.");

            if (!player.TryAddPokemon(pikachu))
                Console.WriteLine("플레이어 파티에 피카츄를 추가할 수 없습니다.");

            if (!player.TryAddPokemon(bulbasaur))
                Console.WriteLine("플레이어 파티에 이상해씨를 추가할 수 없습니다.");


            if (!enemy.TryAddPokemon(laflas))
                Console.WriteLine("AI 파티에 라프라스를 추가할 수 없습니다.");

            if (!enemy.TryAddPokemon(geodude))
                Console.WriteLine("AI 파티에 꼬마돌을 추가할 수 없습니다.");

            if (!enemy.TryAddPokemon(growlithe))
                Console.WriteLine("AI 파티에 가디를 추가할 수 없습니다.");


            // ==========================================
            // Item
            // ==========================================
            player.AddItem(3, 10);

            


             // ==========================================
            // Battle
            // ==========================================
            BattleController controller = new(
                player,
                enemy,
                view);

            PlayerCommander playerCommander = new(controller);
            AiCommander aiCommander = new(enemy, player);

            BattleSystem battle = new(view);

            List<IBattleCommander> participants = new()
            {
                playerCommander,
                aiCommander
            };

            battle.SetupBattle(participants);
            battle.StartTrainerBattle();
        }
    }
}