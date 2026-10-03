using System.Text.Json;

namespace MyGame.NameTables
{
    public static class NameTable
    {
        private static readonly Dictionary<int, string> _pokemonNames;

        static NameTable()
        {
            string json = File.ReadAllText("JsonData/PokemonNames.json");

            var data = JsonSerializer.Deserialize<List<PokemonNameData>>(json)
                       ?? new List<PokemonNameData>();

            _pokemonNames = data.ToDictionary(
                x => x.Id,
                x => x.Name);
        }

        public static string GetPokemon(int pokemonId)
        {
            return _pokemonNames.TryGetValue(pokemonId, out var name)
                ? name
                : "Error";
        }
    }
}