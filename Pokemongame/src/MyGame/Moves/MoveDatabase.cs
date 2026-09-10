using System.Text.Json;
using System.Text.Json.Serialization;

using MyGame.Logs;

namespace MyGame.Moves
{
    public static class MoveDatabase
    {
        private static readonly Dictionary<int, MoveData> _moves = new();

        public static IReadOnlyDictionary<int, MoveData> Moves => _moves;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static void LoadMoveDatabase()
        {
           string filePath = "MoveData.json";

            if (!File.Exists(filePath))
            {
                GameLog.Error("JSON 파일을 찾을 수 없습니다.");
                return;
            }

            try
            {

                string jsonString = File.ReadAllText(filePath);
                List<MoveData>? moveList = JsonSerializer.Deserialize<List<MoveData>>(jsonString, _jsonOptions);
                
                if(moveList == null)
                {
                    GameLog.Error("[Movedatabase] 데이터가 비어 있습니다.");

                    return;
                }

                _moves.Clear();

                foreach (var move in moveList)
                {
                    if (!_moves.TryAdd(move.Key, move))
                        GameLog.Warn($"[MoveDatabase] 중복된 기술 Key가 발견되었습니다! Key: {move.Key}, Name: {move.Name}");
                }

                GameLog.Info($"[MoveDatabase] 총 {_moves.Count}개의 기술 데이터가 성공적으로 로드되었습니다.");
            }
            catch (Exception ex)
            {
                GameLog.Error($"[MoveDatabase] JSON 파싱 중 오류 발생: {ex.Message}");
            }
        }

        public static bool TryGet(int key, out MoveData? move)
            => _moves.TryGetValue(key, out move);
        

        public static MoveData Get(int key)
        {
            if (_moves.TryGetValue(key, out var move))
                return move;
            
            // TryGet이 아닌 Get에서는 데이터가 없으면 심각한 에러이므로 예외를 던짐
            throw new KeyNotFoundException($"MoveDatabase: {key}번 기술 데이터가 누락되었습니다. 엑셀 데이터를 확인하세요.");
        }
    }   
}