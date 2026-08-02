using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokemongame
{
    public static class PokemonDatabase
    {
        private static readonly Dictionary<int, PokemonData> _pokemons = new();

        public static IReadOnlyDictionary<int, PokemonData> Pokemon => _pokemons;
        
        public static void LoadPokemonDatabase()
        {
            string filePath = "PokemonData.json";

            if (!File.Exists(filePath))
            {
                GameLog.Error("JSON 파일을 찾을 수 없습니다.");
                return;
            }

            string jsonString = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            List<PokemonData>? pokemonList = JsonSerializer.Deserialize<List<PokemonData>>(jsonString, options);

            if (pokemonList != null)
            {
                foreach (var pokemon in pokemonList)
                {
                    _pokemons[pokemon.Id] = pokemon;
                }
            }
        }

        public static bool TryGetPokemon(int key,out PokemonData? pokemon)
        {
            if (_pokemons.TryGetValue(key, out pokemon)) 
                return true;
            else 
            {
                GameLog.Warn($"ID [{key}] 번에 해당하는 포켓몬 데이터가 없습니다.");
                pokemon = null;
                return false;
            }
        }
    }   
}
