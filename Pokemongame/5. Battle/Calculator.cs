using MyGame.Utilities;

namespace MyGame.BattleCalculators
{
    public static class Calculator
    {
        /// <summary>
        /// 스피드 비교 (스피드가 같으면 50% 확률로 선공 결정)
        /// </summary>
        public static bool IsFaster(int speedA, int speedB)
        {
            if (speedA == speedB)
                return Chance.TryChance(50); // 동속일 때 50% 랜덤

            return speedA > speedB;
        }

        public static int CalculateMaxHp(int baseHp, int level)
            => baseHp + (level * 3);

        /// <summary>
        /// 랭크 변화에 따른 스피드 배율 계산 (-6 ~ +6)
        /// </summary>
        public static int CalculateCurrentSpeed(int baseSpeed, int speedStage)
        {
            float multiplier = GetStageMultiplier(speedStage);
            return (int)(baseSpeed * multiplier);
        }

        /// <summary>
        /// 랭크 변화에 따른 공격력 배율 계산 (-6 ~ +6)
        /// </summary>
        public static int CalculateCurrentAttack(int baseAttack, int attackStage)
        {
            float multiplier = GetStageMultiplier(attackStage);
            return (int)(baseAttack * multiplier);
        }

        /// <summary>
        /// 랭크 단계(-6 ~ +6)를 배율로 변환 (포켓몬 공식 룰 간소화)
        /// +1: 1.5배, +2: 2.0배 / -1: 0.66배, -2: 0.5배
        /// </summary>
        private static float GetStageMultiplier(int stage)
        {
            if (stage >= 0)
                return (2f + stage) / 2f;
            
            return 2f / (2f - stage);
        }

    
        /// <summary>
        /// 최종 데미지 계산 (무효 상성 시 0 데미지 보장)
        /// </summary>
        public static int CalculateDamage(int effectiveAttack, float typeMultiplier)
        {
            // 상성 무효(0배)일 때는 데미지가 0
            if (typeMultiplier <= 0f)
                return 0;

            int damage = (int)(effectiveAttack * typeMultiplier);
            
            // 데미지가 들어가는 공격이면 최소 1 보장
            return Math.Max(1, damage);
        }
    }
}