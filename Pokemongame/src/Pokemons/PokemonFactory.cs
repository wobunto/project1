using MyGame.Moves;
using MyGame.PokemonDatas;
using MyGame.NameTables;

namespace MyGame.Pokemons
{
    public static class PokemonFactory
    {
        public static PokemonRuntime Create(
            int key,
            int level,
            IEnumerable<int>? moveKeys = null)
        {
            if (!PokemonDatabase.TryGetPokemon(key, out var data))
            {
                throw new InvalidOperationException(
                    $"포켓몬 ID {key}가 존재하지 않습니다.");
            }

            var pokemon = new PokemonRuntime(data!, level);

            if (moveKeys != null)
            {
                foreach (var moveKey in moveKeys)
                {
                    if (!MoveDatabase.TryGet(moveKey, out var move))
                    {
                        throw new InvalidOperationException(
                            $"기술 ID {moveKey}가 존재하지 않습니다.");
                    }

                    if (!pokemon.TryAddMove(move!))
                    {
                        throw new InvalidOperationException(
                            $"포켓몬 {NameTable.GetPokemon(data!.Id)}에게 기술 {move!.Key}을 추가할 수 없습니다.");
                    }
                }
            }

            return pokemon;
        }
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
