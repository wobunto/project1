using MyGame.BattleCalculators;
using MyGame.PokemonDatas;
using MyGame.BattleStatus;

namespace MyGame.Pokemons
{
    public static class PokemonFactory
    {
        public static PokemonRuntime Create(int key, int level)
        {
            if (!PokemonDatabase.TryGetPokemon(key, out var data))
                    throw new InvalidOperationException(
                    $"포켓몬 ID {key}가 존재하지 않습니다.");
            
            var pokemon = new PokemonRuntime(data!,level);
            pokemon.NewStatus(); 
            
            return pokemon;             
        }
        
    }
    /*
    public static class ObjectPooling
    {
        PokemonRuntime wildPokemon => 
        public static void PoolPokemon(PokemonRuntime Oripokemon, int key, int level)
        {
            
            pokemon = 
        }
     
    }
    */
}
