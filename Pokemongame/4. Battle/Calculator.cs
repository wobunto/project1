namespace Pokemongame{
    public static class Calculator
    {
        public static bool IsFaster(int speedA, int speedB)
            => speedA > speedB;

        public static int CalculateMaxHp(int BaseHp, int level) 
            => BaseHp + level * 3;

        public static int CalculateCurrentSpeed(int BaseSpeed,int speedStage) 
            => BaseSpeed + speedStage;
            
        public static int CalculateCurrentAttack(int BaseAttack,int attackStage) 
            => BaseAttack + attackStage;
      
        public static float CalculateTypeMultipler(this PokemonType attackType, List<PokemonType> defenseTypes)
        {
            float finalMultiplier =  1.0f;
            for (int i = 0; i < defenseTypes.Count; i++)
            {
                finalMultiplier *= attackType.GetTypeMultiplier(defenseTypes[i]);
            }
            return finalMultiplier;
        }

         public static int CalculateDamage(int effectiveAttack, float typeMultiplier ) 
        {
            int damage = (int)(effectiveAttack * typeMultiplier);
            return Math.Max(1,damage);
        }
    }
}