using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Items;
using MyGame.BattleCommanders;
using MyGame.NameTables;
using MyGame.Rules;

namespace MyGame.Views
{
    public interface IPlayerView
    {
        void DisplayCommandMenu();
        void DisplayPokemon(IViewPokemon playerPokemon, IViewPokemon enemyPokemon);
        void DisplayAttackMenu(IEnumerable<MoveRuntime?> CurrentMoves);
        void DisplayItemMenu(IReadOnlyList<InventoryItem> inventory);
        void DisplayPartyMenu(IEnumerable<PokemonRuntime> party);
        void DisplayMessage(String message);
        void DisplayStartBattle(IBattleCommander Enemy);
        void DisplayBackInfo();
        void DisplayCantBack();
    }
    
    public class ConsolePlayerView : IPlayerView
    {
        public void DisplayMessage(String message)
        {
            Console.WriteLine(message);
        }

        public void DisplayBackInfo()
        {
            DisplayMessage("돌아가시려면 z 나 백 스페이스를 눌러주세요.");
        }

        public void DisplayCantBack()
        {
             DisplayMessage("현재 교체를 해야 합니다.");
        }

        public void DisplayStartBattle(IBattleCommander Enemy)
        {
            DisplayMessage($"{Enemy.NameId}과의 배틀이 시작됐다!");
            Thread.Sleep(2000);
        }

        public void DisplayPokemon(IViewPokemon playerPokemon, IViewPokemon enemyPokemon)
        {
            DisplayMessage("==============================");
            DisplayMessage($"{NameTable.GetPokemon(playerPokemon.Id)} Level : {playerPokemon.Level}  체력: [{playerPokemon.CurrentHp}/{playerPokemon.MaxHp}]");
            DisplayMessage($"{NameTable.GetPokemon(enemyPokemon.Id)} Level : {enemyPokemon.Level}  체력: [{enemyPokemon.CurrentHp}/{enemyPokemon.MaxHp}]");
            DisplayMessage("==============================");
        }

        public void DisplayCommandMenu()
        {
            DisplayMessage("==============================");
            DisplayMessage("1. 싸운다  2. 가방");
            DisplayMessage("3. 교체    4. 도망친다");
            DisplayMessage("==============================");
        }

        public void DisplayAttackMenu(IEnumerable<MoveRuntime?> CurrentMoves)
        {
            
            var move1 = CurrentMoves.ElementAtOrDefault(0);
            var move2 = CurrentMoves.ElementAtOrDefault(1);
            var move3 = CurrentMoves.ElementAtOrDefault(2);
            var move4 = CurrentMoves.ElementAtOrDefault(3);

            
            DisplayMessage("========================================");
            DisplayMessage($" 1. {FormatMove(move1!),-18} 2. {FormatMove(move2!),-18}");
            DisplayMessage($" 3. {FormatMove(move3!),-18} 4. {FormatMove(move4!),-18}");
            DisplayMessage("========================================");
            DisplayBackInfo();
        }

        public void DisplayItemMenu(IReadOnlyList<InventoryItem> items)
        {   
            DisplayMessage("[ 아이템 목록 ]");
           
            if (items.Count == 0)
            {
                DisplayMessage(" 가방이 비어 있습니다.");
                return;
            }

            DisplayMessage("========================================");
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                DisplayMessage($" {i + 1}.[ {item.Data.Name} x {item.Count} ]");
            }
            DisplayMessage("========================================");
            
            DisplayBackInfo();
        }
        
        
        public void DisplayPartyMenu(IEnumerable<PokemonRuntime> party)
        {
    
            PokemonRuntime? pokemon1 = party.ElementAtOrDefault(0);
            PokemonRuntime? pokemon2 = party.ElementAtOrDefault(1);
            PokemonRuntime? pokemon3 = party.ElementAtOrDefault(2);
            PokemonRuntime? pokemon4 = party.ElementAtOrDefault(3);
            PokemonRuntime? pokemon5 = party.ElementAtOrDefault(4);
            PokemonRuntime? pokemon6 = party.ElementAtOrDefault(5);

            DisplayMessage("========================================");
            DisplayMessage($" 1. {FormatPokemon(pokemon1),-18} 2. {FormatPokemon(pokemon2),-18}");
            DisplayMessage($" 3. {FormatPokemon(pokemon3),-18} 4. {FormatPokemon(pokemon4),-18}");
            DisplayMessage($" 5. {FormatPokemon(pokemon5),-18} 6. {FormatPokemon(pokemon6),-18}");
            DisplayMessage("========================================");
        }

        private string FormatMove(MoveRuntime move)
        {
            if (move == null)
            {
                return "------"; // 기술이 등록되지 않은 빈 슬롯 표시
            }

            return $"{MoveNameTable.Get(move.Data.Key)} ({move.CurrentPP}/{move.MaxPP})";
        }
        
        private string FormatPokemon(IViewPokemon? pokemon)
        {
            if (pokemon == null)
            {
                return "--없음--"; // 
            }

            return $"{NameTable.GetPokemon(pokemon.Id)} ({pokemon.CurrentHp}/{pokemon.MaxHp})";
        }
        
    }
}
