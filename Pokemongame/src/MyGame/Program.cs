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

            StartBattle();

        }
        

        private static void StartBattle()
        {
        
            // ==========================================
            // Trainer
            // ==========================================
            PlayerRuntime player = new(PokemonRules.PlayerId);
            EnemyRuntime enemy = new(1);

            ConsolePlayerView view = new();
            // ==========================================
            // Player Pokemon
            // ==========================================

            // ==========================================
            // Player Pokemon
            // ==========================================

            // 괴력몬
            var machamp = PokemonFactory.Create(
                PokemonRules.Machop,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.BrickBreak,
                    MoveRules.Earthquake,
                    MoveRules.RockSlide
                });

            // 리자몽
            var charizard = PokemonFactory.Create(
                PokemonRules.Charizard,
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
                PokemonRules.Pikachu,
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
                PokemonRules.Bulbasaur,
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
            var lapras = PokemonFactory.Create(
                PokemonRules.Lapras,
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
                PokemonRules.Geodude,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.RockSlide,
                    MoveRules.Earthquake,
                    MoveRules.StoneEdge
                });

            // 가디
            var growlithe = PokemonFactory.Create(
                PokemonRules.Growlithe,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.FlameThrower,
                    MoveRules.FirePunch,
                    MoveRules.QuickAttack
                });

            // 팬텀
            var gastly = PokemonFactory.Create(
                PokemonRules.Gastly,
                50,
                new[]
                {
                    MoveRules.Tackle,
                    MoveRules.ShadowBall,
                    MoveRules.PoisonJab,
                    MoveRules.Psychic
                });

            // ==========================================
            // Party 구성
            // ==========================================

            if (!player.TryAddPokemon(machamp))
                Console.WriteLine("내 포켓몬이 추가가 안됨");
            if (!player.TryAddPokemon(charizard))
                Console.WriteLine("내 포켓몬이 추가가 안됨");
            if (!player.TryAddPokemon(pikachu))
                 Console.WriteLine("내 포켓몬이 추가가 안됨");
            if (!player.TryAddPokemon(bulbasaur))
                 Console.WriteLine("내 포켓몬이 추가가 안됨");

            if (!enemy.TryAddPokemon(lapras))
                Console.WriteLine("적이 포켓몬이 추가가 안됨");
            if (!enemy.TryAddPokemon(geodude))
                 Console.WriteLine("적이 포켓몬이 추가가 안됨");
            if (!enemy.TryAddPokemon(growlithe))
                 Console.WriteLine("적이 포켓몬이 추가가 안됨");
            if (!enemy.TryAddPokemon(gastly))
                 Console.WriteLine("적이 포켓몬이 추가가 안됨");
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