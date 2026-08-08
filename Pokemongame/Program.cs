namespace Pokemongame
{
    class Program{
        static void Main(string[] args)
        {
            PokemonDatabase.LoadPokemonDatabase();
            MoveDatabase.LoadMoveDatabase();
            BattleSystem battle = new BattleSystem();
            
            var PlayerRuntime = new TrainerRuntime();
            var NpcRuntime = new TrainerRuntime();

            

            var rizard = PokemonFactory.Create(6 ,50);
            rizard.TryLearn(102);
            rizard.TryLearn(105);
            rizard.TryLearn(106);
            rizard.TryLearn(107);

            var laflas = PokemonFactory.Create(3,50);
            laflas.TryLearn(101);
            laflas.TryLearn(103);
            
            PlayerRuntime.CapturePokemon(rizard);
            NpcRuntime.CapturePokemon(laflas);


            BattleActor Player = new PlayerActor(PlayerRuntime);
            BattleActor Enemy = new EnemyActor(NpcRuntime);
                
            battle.StartBattle(Player,Enemy);
       }
    }
}