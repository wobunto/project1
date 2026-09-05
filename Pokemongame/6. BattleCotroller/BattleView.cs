using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Items;
using MyGame.Logs;

namespace MyGame.Views
{
    public interface IPlayerView
    {
        void DisplayCommandMenu();
        void DisplayAttackMenu(IReadOnlyList<MoveRuntime?> CurrentMoves);
        void DisplayItemMenu(IReadOnlyList<InventoryItem> inventory);
        void DisplayPartyMenu(IReadOnlyList<PokemonRuntime> party);
    }
    
    public class ConsolePlayerView : IPlayerView
    {
        
        public void DisplayCommandMenu()
        {
            GameLog.Info("==============================");
            GameLog.Info("1. 싸운다  2. 가방");
            GameLog.Info("3. 교체    4. 도망친다");
            GameLog.Info("==============================");
        }

        public void DisplayAttackMenu(IReadOnlyList<MoveRuntime?> CurrentMoves)
        {
            
            var move1 = CurrentMoves.ElementAtOrDefault(0);
            var move2 = CurrentMoves.ElementAtOrDefault(1);
            var move3 = CurrentMoves.ElementAtOrDefault(2);
            var move4 = CurrentMoves.ElementAtOrDefault(3);

            
            GameLog.Info("========================================");
            GameLog.Info($" 1. {FormatMove(move1!),-18} 2. {FormatMove(move2!),-18}");
            GameLog.Info($" 3. {FormatMove(move3!),-18} 4. {FormatMove(move4!),-18}");
            GameLog.Info("========================================");
        }

        public void DisplayItemMenu(IReadOnlyList<InventoryItem> items)
        {   
            GameLog.Info("[ 아이템 목록 ]");
           
            if (items.Count == 0)
            {
                GameLog.Info(" 가방이 비어 있습니다.");
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                GameLog.Info($" {i + 1}.[ {item.Name} x {item.Count} ]");
            }
        }
        
        
        public void DisplayPartyMenu(IReadOnlyList<PokemonRuntime> party)
        {
            var pokemon1 = party.ElementAtOrDefault(0);
            var pokemon2 = party.ElementAtOrDefault(1);
            var pokemon3 = party.ElementAtOrDefault(2);
            var pokemon4 = party.ElementAtOrDefault(3);
            var pokemon5 = party.ElementAtOrDefault(4);
            var pokemon6 = party.ElementAtOrDefault(5);

            GameLog.Info("========================================");
            GameLog.Info($" 1. {FormatPokemon(pokemon1!),-18} 2. {FormatPokemon(pokemon2!),-18}");
            GameLog.Info($" 3. {FormatPokemon(pokemon3!),-18} 4. {FormatPokemon(pokemon4!),-18}");
            GameLog.Info($" 5. {FormatPokemon(pokemon5!),-18} 6. {FormatPokemon(pokemon6!),-18}");
            GameLog.Info("========================================");
        }

        private string FormatMove(MoveRuntime move)
        {
            if (move == null)
            {
                return "------"; // 기술이 등록되지 않은 빈 슬롯 표시
            }

            return $"{move.Name} ({move.CurrentPP}/{move.MaxPP})";
        }
        
        private string FormatPokemon(PokemonRuntime pokemon)
        {
            if (pokemon == null)
            {
                return "--없음--"; // 
            }

            return $"{pokemon.Name} ({pokemon.CurrentHp}/{pokemon.MaxHp})";
        }
        
    }
}
