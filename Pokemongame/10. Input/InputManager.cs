namespace Pokemongame
{
    public static class InputManager
    {
        public const int MAX_MOVE_SLOTS = 4;
        public const int MAX_SELECT_SLOTS = 4;
        public const int MAX_PARTY_SLOTS = 6;

        public static bool GetYesOrNo()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 입력한 키가 화면에 안 보이게 읽음
                if (keyInfo.Key == ConsoleKey.Y) return true;
                if (keyInfo.Key == ConsoleKey.N) return false;

                GameLog.Info("Y 또는 N 만 입력 가능합니다.");
            }       
        }

        public static int GetSlotChoice(int maxSlots)
        {
            GameLog.Info($"[1 ~ {maxSlots}]");
            //키보드나 키패드로 1~4까지 입력
            while(true)
            {   
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9) ||
                    (keyInfo.Key >= ConsoleKey.NumPad1 && keyInfo.Key <= ConsoleKey.NumPad9))
                {
                    int choice = (int)char.GetNumericValue(keyInfo.KeyChar); // 
                    
                    if (choice >= 1 && choice <= maxSlots)
                        return choice - 1;
                }
                    GameLog.Error($"\n1부터 {maxSlots} 사이의 숫자만 가능합니다.");
            }
        }
    }
}