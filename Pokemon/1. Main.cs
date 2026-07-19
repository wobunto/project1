namespace Pokemongame
{
    class Program{
        static void Main(string[] args)
        {
            MoveCategory.MoveDatabase();

            PokemonData charizardData = PokemonDatabase.CreateCharizard();
            PokemonData laflasData = PokemonDatabase.CreateLapras();

            GameObject player = PokemonFactory.Create(charizardData,50);
            GameObject enemy = PokemonFactory.Create(laflasData,50);

            BattleSystem battle = new BattleSystem();

            if(player.TryGetComponent<CharacterComponent>(out var p_pokemon)&& enemy.TryGetComponent<CharacterComponent>(out var e_pokemon))
            {
                BattleActor _player = new PlayerActor(p_pokemon);
                BattleActor _enemy = new EnemyActor(e_pokemon);
                
                battle.StartBattle(_player,_enemy);
            }
            else
                Console.WriteLine("포켓몬이 제대로 설정되지 않았습니다.");    
       }
    }
}