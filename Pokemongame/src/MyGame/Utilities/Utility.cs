namespace MyGame.Utilities
{
    public static class Utility
    {
        /// <summary>
        /// 지정한 정수나 실수 백분율(0 ~ 100)로 성공 여부를 반환합니다.
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

        /// <summary>
        /// index가 양수이며 count 보다 작은지 검사하는 함수.
        /// </summary>
        public static bool IsValidIndex(int index, int count)
            => index >= 0 && index < count ;
    }
}