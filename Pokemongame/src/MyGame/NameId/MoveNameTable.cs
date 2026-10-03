using System.Text.Json;

namespace MyGame.NameTables
{
    public static class MoveNameTable
    {
        private static readonly Dictionary<int, string> _moveNames;

        static MoveNameTable()
        {
            string json = File.ReadAllText("JsonData/MoveNames.json");

            var data = JsonSerializer.Deserialize<List<MoveNameData>>(json)
                       ?? new List<MoveNameData>();

            _moveNames = data.ToDictionary(
                x => x.Id,
                x => x.Name);
        }

        public static string Get(int moveId)
        {
            return _moveNames.TryGetValue(moveId, out var name)
                ? name
                : "Error";
        }
    }
}