using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Items;
using MyGame.BattleCommanders;
using MyGame.Rules;

namespace MyGame.Views
{
    public interface IPlayerView
    {
        void DisplayCommandMenu();
         void DisplayPokemon(IBattlePokemon playerPokemon, IBattlePokemon enemyPokemon);
        void DisplayAttackMenu(IReadOnlyList<MoveRuntime?> CurrentMoves);
        void DisplayItemMenu(IReadOnlyList<InventoryItem> inventory);
        void DisplayPartyMenu(IReadOnlyList<PokemonRuntime> party);
        void DisplayMessage(String message);
        void DisplayStartBattle(IBattleCommander Enemy);
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

        public void DisplayStartBattle(IBattleCommander Enemy)
        {
            Console.Clear();
            DisplayMessage($"{Enemy.NameId}과의 배틀이 시작됐다!");
            Thread.Sleep(2000);
        }

        public void DisplayPokemon(IBattlePokemon playerPokemon, IBattlePokemon enemyPokemon)
        {
            Console.Clear();
            DisplayMessage("==============================");
            DisplayMessage($"{playerPokemon.Name} Level : {playerPokemon.Level}  체력: [{playerPokemon.CurrentHp}/{playerPokemon.MaxHp}]");
            DisplayMessage($"{enemyPokemon.Name} Level : {enemyPokemon.Level}  체력: [{enemyPokemon.CurrentHp}/{enemyPokemon.MaxHp}]");
            DisplayMessage("==============================");
        }

        public void DisplayCommandMenu()
        {
            DisplayMessage("==============================");
            DisplayMessage("1. 싸운다  2. 가방");
            DisplayMessage("3. 교체    4. 도망친다");
            DisplayMessage("==============================");
            DisplayBackInfo();
        }

        public void DisplayAttackMenu(IReadOnlyList<MoveRuntime?> CurrentMoves)
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
        
        
        public void DisplayPartyMenu(IReadOnlyList<PokemonRuntime> party)
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
            DisplayBackInfo();
        }

        private string FormatMove(MoveRuntime move)
        {
            if (move == null)
            {
                return "------"; // 기술이 등록되지 않은 빈 슬롯 표시
            }

            return $"{move.Name} ({move.CurrentPP}/{move.MaxPP})";
        }
        
        private string FormatPokemon(PokemonRuntime? pokemon)
        {
            if (pokemon == null)
            {
                return "--없음--"; // 
            }

            return $"{pokemon.Name} ({pokemon.CurrentHp}/{pokemon.MaxHp})";
        }
        
    }
}
