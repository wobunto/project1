namespace Pokemongame
{
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
}