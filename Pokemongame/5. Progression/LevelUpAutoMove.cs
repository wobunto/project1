namespace Pokemongame
{
    public class LeveUp
    {
        public static void ProcessLevelUp(PokemonRuntime pokemon)
        {
            while (pokemon.TryGetPendingLevelUpMove(out MoveData? move))
            {
                MoveLearning.TryLearn(pokemon, move!);
                pokemon.AdvancePendingLevelUpMove();
            }
        }
    }
}