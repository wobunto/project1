namespace MyGame.Logs
{ 
    public static class GameLog
    {
        public static void Info(string message) => Console.WriteLine(message);
        public static void Warn(string message) => Console.WriteLine($"[경고] {message}");
        public static void Error(string message) => Console.WriteLine($"[에러] {message}"); 
    }        
}