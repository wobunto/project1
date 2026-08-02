namespace Pokemongame
{
    class Program{
        static void Main(string[] args)
        {
            PokemonCategory.LoadPokemonDatabase();
            MoveCategory.LoadMoveDatabase();
            BattleSystem battle = new BattleSystem();
            
            var PlayerRuntime = new TrainerRuntime();
            var NpcRuntime = new TrainerRuntime();


            var rizard = PokemonFactory.Create(6 ,50);
            rizard.InsertMove(102,0);
            rizard.InsertMove(105,1);
            rizard.InsertMove(106,2);
            rizard.InsertMove(107,3);

            var laflas = PokemonFactory.Create(3,50);
            laflas.InsertMove(101,0);
            laflas.InsertMove(103,1);
    
            PlayerRuntime.CapturePokemon(rizard);
            NpcRuntime.CapturePokemon(laflas);

            BattleActor Player = new PlayerActor(PlayerRuntime);
            BattleActor Enemy = new EnemyActor(NpcRuntime);
                
            battle.StartBattle(Player,Enemy);
       }
    }
}