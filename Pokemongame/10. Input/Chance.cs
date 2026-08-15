namespace Pokemongame
{

    public static class Chance
    {
        private static readonly Random Random = new Random();

        public static bool TryChance(int value)
        {
            int rand = Random.Shared.Next(100) + 1;  //1~100까지 
            if(rand <= value)              //value가 25일 때, 1~25 숫자는 true. 즉 25% 확률
                return true;
            else
                return false;
        }
    }
}