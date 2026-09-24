
namespace MyGame.NameTables
{
    public static class NameTable
    {
        private static readonly Dictionary<int, string> _pokemonNames = new()
        {
        [0] = "Error",
        [1] = "이상해씨",
        [4] = "파이리",
        [6] = "리자몽",
        [7] = "라프라스"
   
        };

        private static readonly Dictionary<int, string> _trainerNames = new()
        {
            [0] = "나",
            [1] = "라이벌",
            [2] = "웅"
        };

        public static string GetPokemon(int nameId)
            => _pokemonNames.TryGetValue(nameId, out var name) ? name : _pokemonNames[0];

        public static string GetTrainer(int nameId)
            => _trainerNames.TryGetValue(nameId, out var name) ? name : _trainerNames[0];
    }
}