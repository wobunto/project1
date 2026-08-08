using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokemongame
{
    public static class MoveDatabase
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
            return _moves.TryGetValue(key, out move);
        }

        public static MoveData Get(int key)
        {
            if (_moves.TryGetValue(key, out var move))
            {
                return move;
            }
            // TryGet이 아닌 Get에서는 데이터가 없으면 심각한 에러이므로 예외를 던짐
            throw new KeyNotFoundException($"MoveDatabase: {key}번 기술 데이터가 누락되었습니다. 엑셀 데이터를 확인하세요.");
        }
    }   
}