using MyGame.Utilities;

namespace MyGame.BattleCalculators
{
    public static class BattleCalculator
    {    
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
        public static int CalculateDamage(int effectiveAttack, int movePower, int defense, float typeMultiplier)
        {          
            if (typeMultiplier <= 0f)
                return 0;

            defense = Math.Max(1, defense);
                
            float damage = (float)effectiveAttack * movePower / defense;
            int result = (int)(damage * typeMultiplier);
        
            // 데미지가 들어가는 공격이면 최소 1 보장
            return Math.Max(1, result);
        }
    }
}