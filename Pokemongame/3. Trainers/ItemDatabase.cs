using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokemongame
{
    public static class ItemDatabase
    {
        private static Dictionary<int, ItemData> _items = new();

        public static IReadOnlyDictionary<int, ItemData> Items => _items;
        
        public static void LoadItemDatabase()
        {
            string filePath = "ItemData.json";

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

            List<ItemData>? itemList = JsonSerializer.Deserialize<List<ItemData>>(jsonString, options);

            if (itemList != null)
            {
                foreach (var items in itemList)
                {
                    _items[items.Key] = items;
                }
            }
        }
        
        public static bool TryGetItem(int key, out ItemData? data)
        {
            return _items.TryGetValue(key, out data);
        }
    }   
}