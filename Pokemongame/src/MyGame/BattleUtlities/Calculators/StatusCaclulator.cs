namespace MyGame.BattleCalculators
{
    public static class StatusEffectCalculator
    {
        public static int BurnDamage(int maxHp)
            => Math.Max(1, maxHp / 16);

        public static int PoisonDamage(int maxHp)
            => Math.Max(1, maxHp / 8);

        public static int ToxicDamage(int maxHp, int turn)
            => Math.Max(1, maxHp / 16 * turn);
}
}