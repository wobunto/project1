namespace MyGame.Utilities
{

    public static class Chance
    {
        /// <summary>
        /// 지정한 정수 백분율(0 ~ 100)로 성공 여부를 반환합니다.
        /// 사용 예: if (Chance.TryChance(50)) // 50% 확률
        /// </summary>
        public static bool TryChance(int percentage)
        {
            if (percentage <= 0) return false;
            if (percentage >= 100) return true;

            return Random.Shared.Next(100) < percentage;
        }

        public static bool TryChance(float percentage)
        {
            if (percentage <= 0f) return false;
            if (percentage >= 100f) return true;

            return (Random.Shared.NextDouble() * 100.0) < percentage;
        }
    }
}