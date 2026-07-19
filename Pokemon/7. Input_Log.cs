using System.Security.Claims;
namespace Pokemongame
{
     public static class InputManager
    {
        public static bool GetYesOrNo()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 입력한 키가 화면에 안 보이게 읽음
                if (keyInfo.Key == ConsoleKey.Y) return true;
                if (keyInfo.Key == ConsoleKey.N) return false;

                Console.WriteLine("\nY 또는 N 만 입력 가능합니다.");
            }       
        }
        public static int GetMoveSlotChoice(int maxSlots = 4)
        {
            //키보드나 키패드로 1~4까지 입력
            while(true)
            {   
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9) ||
                    (keyInfo.Key >= ConsoleKey.NumPad1 && keyInfo.Key <= ConsoleKey.NumPad9))
                {
                    int choice = (int)char.GetNumericValue(keyInfo.KeyChar) - 1; // 인덱스 - 1 
                    if (choice >= 0 && choice < maxSlots)
                    {
                        return choice+1;                        }
                    Console.WriteLine($"\n1부터 {maxSlots} 사이의 숫자만 눌러주세요.");
                }
                
            }
        }
    }

    public static class EnumRandom<T> where T : Enum
    {
        private static readonly T[] Values = (T[])Enum.GetValues(typeof(T));
        private static readonly Random Random = new Random();

        public static T GetRandom()
        {
            int index = Random.Next(Values.Length);
            return Values[index];
        }
    }
    
      public static class GameLog
    {
        //1. 일반 스태틱 메서드 (인자가 없거나 복합적)
        public static void LogBattleStart()
            => Console.WriteLine("포켓몬 배틀을 시작합니다.!");

        public static void LogSelectForgetMove()
            => Console.WriteLine($"잊을 기술의 번호를 선택하세요 (1~4):");
        
        public static void LogRunFail()
            => Console.WriteLine("도망칠 수 없다.");
        public static void LogSelectAct()
        {
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 아이템");
            Console.WriteLine("3. 교체");
            Console.WriteLine("4. 도망");
        }
        public static void LogWarnningNull()
            => Console.WriteLine($"현재 반환한 컴포넌트가 Null 입니다.");
        public static void LogCurrentStat(CharacterComponent playerPokemon,CharacterComponent enemyPokemon)
        {
            Console.WriteLine($"내 {playerPokemon.PokemonName}의 현재 상태 [Lv.{playerPokemon.Runtime.Level} hp: {playerPokemon.Runtime.CurrentHp}/{playerPokemon.Runtime.MaxHp}]");
            Console.WriteLine($"상대 {enemyPokemon.PokemonName}의 현재 상태 [Lv.{enemyPokemon.Runtime.Level} hp: {enemyPokemon.Runtime.CurrentHp}]");
        }    
         public static void LogEffective(float finalMultiplier)
        {
            if(finalMultiplier > 1) Console.WriteLine("효과가 굉장했다!");
            else if(finalMultiplier < 1) Console.WriteLine("효과가 별로인 듯하다...");
        }

        //확장 메서드(characterComponenet)
        
        
        //사용 예: attacker.LogAttack();
        public static void LogAttack(this CharacterComponent attacker) 
            => Console.WriteLine($"{attacker.PokemonName}의 공격!");
        //사용 예: defender.LogDamage(damage);
        public static void LogDamage(this CharacterComponent defender, int damage) 
            => Console.WriteLine($"{defender.PokemonName}에게 {damage}의 피해를 입혔다!");
        public static void LogFaint(this CharacterComponent defender) 
            => Console.WriteLine($"{defender.PokemonName}이(가) 쓰러졌다.");
        public static void LogFinal(this CharacterComponent attacker, CharacterComponent defender, int damage, float finalMultiplier)
        {
            attacker.LogAttack();
            defender.LogDamage(damage);
            LogEffective(finalMultiplier);
        }
        public static void LogLearnMove(this CharacterComponent pokemon, MoveData newMove)
        => Console.WriteLine($"{pokemon.PokemonName}은(는) 새로운 기술 {newMove.Name}을(를) 배웠다!");

        // 기술 칸이 꽉 차서 질문할 때
        public static void LogMoveSlotsFull(this CharacterComponent pokemon, MoveData newMove)
        {
            Console.WriteLine($"{pokemon.PokemonName}은(는) 새로운 기술 {newMove.Name}을(를) 배우고 싶다...");
            Console.WriteLine($"하지만 이미 기술이 4개로 가득 차 있다!");
            Console.WriteLine($"새로운 기술을 위해 기존 기술 하나를 잊으시겠습니까?");
        }
      
        // 기존 기술을 잊었을 때
        public static void LogForgetMove(this CharacterComponent pokemon, MoveData oldMove)
            => Console.WriteLine($"{pokemon.PokemonName}은(는) {oldMove.Name}을(를) 깨끗이 잊었다!");

        // 배움을 포기했을 때
        public static void LogGiveUpLearning(this CharacterComponent pokemon, MoveData newMove)
            => Console.WriteLine($"{pokemon.PokemonName}은(는) {newMove.Name} 배우기를 포기했다.");
        

        public static void LogChoiceMove(this CharacterComponent pokemon)
        {
            int emptyIndex = pokemon.Runtime.GetFirstEmptyIndex();
            int maxSlot = 4;

            Console.WriteLine($"{pokemon.PokemonName}은 어떤 스킬을 사용할까?");

            for(int i = 0; i<maxSlot; i++)
            {   
                if(i<emptyIndex || emptyIndex == -1)
                    Console.Write($" {i+1}.[ {pokemon.Moves[i].Data.Name} ]");
                else
                    Console.Write($" {i+1}.[ 없음 ]\n");
            }
        }
        public static void LogWantLearnMove(this CharacterComponent pokemon, MoveData move)
        {
            Console.WriteLine($"{pokemon.PokemonName}은 {move.Name}을 배우고 싶어 한다. \n 배우시겠습니까? [Y/N]");
        }
        public static void LogReadPokemon(this CharacterComponent pokemon)
        {
            string typeInfo = string.Join("/",pokemon.Data.Types);
            //타입이 몇 개든 알아서 ,로 구분
            Console.WriteLine($"포켓몬 : {pokemon.PokemonName}");
            Console.WriteLine($"체력 : {pokemon.Data.BaseHp}/{pokemon.Runtime.MaxHp}");
            Console.WriteLine($"공격력 : {pokemon.Data.BaseAttack}(공격력 랭크:{pokemon.Runtime.AttackStage})");
            Console.WriteLine($"속도 : {pokemon.Data.BaseSpeed}(속도 랭크:{pokemon.Runtime.SpeedStage})");
            Console.WriteLine($"타입 : {typeInfo}");
        }
    }
}