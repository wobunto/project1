using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokemongame
{
    public static class MoveCategory
    {
        private static readonly Dictionary<int, MoveData> _moves = new();

        public static IReadOnlyDictionary<int, MoveData> Move => _moves;

        public static void LoadMoveDatabase()
        {
           string filePath = "MoveData.json";

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

            List<MoveData>? moveList = JsonSerializer.Deserialize<List<MoveData>>(jsonString, options);
            
            if(moveList != null)
            {
                foreach (var move in moveList)
                {
                     _moves[move.key] = move;
                }
            }
        }

        public static bool TryGet(int key, out MoveData? move)
        {
            if (_moves.TryGetValue(key, out move)) 
                return true;
            else
            {    
                GameLog.Warn($"key [{key}]번에 해당하는 기술이 존재하지 않습니다.");
                move = null;
                return false;
            }
        }
    }   
}