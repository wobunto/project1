using MyGame.BattleCommanders;
using MyGame.BattleControllers;
using MyGame.Trainers;
using MyGame.Views;
using MyGame.BattleSystems;
using MyGame.Pokemons;
using MyGame.Rules;

namespace MyGame.Services
{
    public class BattleGameService
    {
        // Razor 화면에서 읽어갈 핵심 프로퍼티들
        public BattleController Controller { get; private set; } = default!;
        public BattleSystem BattleSystem { get; private set; } = default!;
        public bool IsInitialized { get; private set; } = false;

        // 배틀 생성 및 시작 메서드 (기존 StartBattle() 코드 그대로 이관)
        public async Task StartNewBattleAsync(IPlayerView view)
        {
            // 1. 트레이너 생성
            PlayerRuntime player = new(PokemonRules.PlayerId);
            EnemyRuntime enemy = new(1);

            // 2. 포켓몬 생성
            var machamp = PokemonFactory.Create(PokemonRules.Machop, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.BrickBreak, 
                MoveRules.Earthquake, 
                MoveRules.RockSlide 
                });
            var charizard = PokemonFactory.Create(PokemonRules.Charizard, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.FlameThrower, 
                MoveRules.ThunderPunch, 
                MoveRules.Fly 
                });
            var pikachu = PokemonFactory.Create(PokemonRules.Pikachu, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.ThunderPunch, 
                MoveRules.Thunderbolt, 
                MoveRules.QuickAttack 
                });
            var bulbasaur = PokemonFactory.Create(PokemonRules.Bulbasaur, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.SolarBeam, 
                MoveRules.LeafBlade, 
                MoveRules.MegaDrain 
                });

            var lapras = PokemonFactory.Create(PokemonRules.Lapras, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.Surf, 
                MoveRules.AquaTail, 
                MoveRules.IcePunch 
                });
            var geodude = PokemonFactory.Create(PokemonRules.Geodude, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.RockSlide, 
                MoveRules.Earthquake,
                 MoveRules.StoneEdge 
                 });
            var growlithe = PokemonFactory.Create(PokemonRules.Growlithe, 50, new[] { 
                MoveRules.Tackle, 
                MoveRules.FlameThrower, 
                MoveRules.FirePunch, 
                MoveRules.QuickAttack 
                });
            var gastly = PokemonFactory.Create(PokemonRules.Gastly, 50, new[] { 
                MoveRules.Tackle,
                MoveRules.ShadowBall, 
                MoveRules.PoisonJab, 
                MoveRules.Psychic 
                });

            // 3. 파티 등록
            player.TryAddPokemon(machamp);
            player.TryAddPokemon(charizard);
            player.TryAddPokemon(pikachu);
            player.TryAddPokemon(bulbasaur);

            enemy.TryAddPokemon(lapras);
            enemy.TryAddPokemon(geodude);
            enemy.TryAddPokemon(growlithe);
            enemy.TryAddPokemon(gastly);

            // 4. 아이템 등록
            player.AddItem(3, 10);

            // 5. 컨트롤러 및 커맨더 조립
            Controller = new BattleController(player, enemy);
            var playerCommander = new PlayerCommander(Controller);
            var aiCommander = new AiCommander(enemy, player);

            // 6. 배틀 시스템 조립
            BattleSystem = new BattleSystem(view);
            BattleSystem.SetupBattle(new List<IBattleCommander> { playerCommander, aiCommander });

            IsInitialized = true;

            // 7. 배틀 비동기 루프 시작
            await BattleSystem.StartTrainerBattleAsync();
        }
    }
}