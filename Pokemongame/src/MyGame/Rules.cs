namespace MyGame.Rules
{
    public static class PokemonRules
    {
        public const int PlayerId = 0;
        public const int Reset = 0;        
        public const int MaxMoveSlot = 4;       // 포켓몬 기술 최대 개수
        public const int MaxPartySlot = 6;      // 파티 최대 포켓몬 수
        public const int MaxLevel = 100;        // 최대 레벨
    }

    public static class MoveRules
    {
        public const int Tackle = 101;
        public const int FlameThrower = 102;
        public const int Surf = 103;
        public const int SolarBeam = 104;
        public const int ThunderPunch = 105;
        public const int Fly = 106;
        public const int StoneEdge = 107;
        public const int FirePunch = 108;
        public const int AquaTail = 109;
        public const int LeafBlade = 110;
        public const int Thunderbolt = 111;
        public const int Earthquake = 112;
        public const int RockSlide = 113;
        public const int AirSlash = 114;
        public const int IcePunch = 115;
        public const int MegaDrain = 116;
        public const int QuickAttack = 117;

        public const int Struggle = 999;
    }
}
