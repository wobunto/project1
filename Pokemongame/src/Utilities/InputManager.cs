using MyGame.Logs;

namespace MyGame.Inputs
{
    public enum InputType
    {
        None,    // 아무 키도 안 눌림 or 유효하지 않은 키
        Number,  // 0~9 숫자
        Cancel   // 취소 (백스페이스, Z)
    }

    public readonly struct Input
    {
        public InputType Type { get; }
        public int Value { get; }
        
        public bool IsCancel => Type == InputType.Cancel;
        public bool HasValue => Type == InputType.Number;

        private Input(InputType type, int value)
        {
            Type = type;
            Value = value;
        }
        public static Input None 
            => new Input(InputType.None, -1);
        public static Input Cancel 
            => new Input(InputType.Cancel, -1);
        public static Input Number(int value) 
            => new Input(InputType.Number, value);
    }
    
    public static class ConsoleInputManager
    {
        public static Input GetNumberKey()
        {
            if (!Console.KeyAvailable)  //입력 안됨
                return Input.None;

            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
              
            if (keyInfo.Key == ConsoleKey.Backspace || keyInfo.Key == ConsoleKey.Z) //취소
                return Input.Cancel; 
            
            char keyChar = keyInfo.KeyChar;

            if (keyChar >= '0' && keyChar <= '9')  //숫자키 입력
                return Input.Number(keyChar - '0');
            
            return Input.None;    //무시
        }

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