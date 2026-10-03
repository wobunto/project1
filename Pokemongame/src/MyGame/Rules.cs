namespace MyGame.Rules
{
    public static class PokemonRules
    {
        public const int PlayerId = 0;
        public const int Reset = 0;

        public const int MaxMoveSlot = 4;       // 포켓몬 기술 최대 개수
        public const int MaxPartySlot = 6;      // 파티 최대 포켓몬 수
        public const int MaxLevel = 100;        // 최대 레벨


        // ==========================================
        // Pokemon Key
        // ==========================================

        public const int Bulbasaur = 1;         // 이상해씨
        public const int Charmander = 4;        // 파이리
        public const int Charizard = 6;         // 리자몽
        public const int Squirtle = 7;          // 꼬부기
        public const int Pikachu = 25;          // 피카츄
        public const int Sandshrew = 27;        // 모래두지
        public const int Vulpix = 37;           // 식스테일
        public const int Jigglypuff = 39;       // 푸린
        public const int Psyduck = 54;          // 고라파덕
        public const int Growlithe = 58;        // 가디
        public const int Geodude = 74;          // 꼬마돌
        public const int Staryu = 120;          // 별가사리
        public const int Jynx = 124;            // 루주라
        public const int Electabuzz = 125;      // 에레브
        public const int Lapras = 131;          // 라프라스

        // 추가
        public const int Butterfree = 12;       // 버터플
        public const int Sentret = 19;         // 꼬리선
        public const int Clefairy = 35;         // 삐삐
        public const int Oddish = 43;           // 뚜벅쵸
        public const int Abra = 63;             // 케이시
        public const int Machop = 66;           // 알통몬
        public const int Gastly = 92;           // 고오스
        public const int Magikarp = 129;        // 잉어킹
        public const int Eevee = 133;           // 이브이
    }


    public static class MoveRules
    {
        // ==========================================
        // 기본 기술
        // ==========================================

        public const int Tackle = 101;          // 몸통박치기
        public const int FlameThrower = 102;    // 화염방사
        public const int Surf = 103;            // 파도타기
        public const int SolarBeam = 104;       // 솔라빔
        public const int ThunderPunch = 105;    // 번개펀치
        public const int Fly = 106;             // 공중날기
        public const int StoneEdge = 107;       // 스톤엣지
        public const int FirePunch = 108;       // 불꽃펀치
        public const int AquaTail = 109;        // 아쿠아테일
        public const int LeafBlade = 110;       // 리프블레이드
        public const int Thunderbolt = 111;     // 10만볼트
        public const int Earthquake = 112;      // 지진
        public const int RockSlide = 113;       // 스톤샤워
        public const int AirSlash = 114;        // 에어슬래시
        public const int IcePunch = 115;        // 냉동펀치
        public const int MegaDrain = 116;       // 메가드레인
        public const int QuickAttack = 117;     // 전광석화


        // ==========================================
        // 추가 기술
        // ==========================================

        public const int BrickBreak = 118;      // 깨트리다
        public const int SludgeBomb = 119;      // 오물폭탄
        public const int Psychic = 120;         // 사이코키네시스
        public const int XScissor = 121;        // 시저크로스
        public const int ShadowBall = 122;      // 섀도볼
        public const int DragonPulse = 123;     // 용의파동
        public const int DarkPulse = 124;       // 악의파동
        public const int IronHead = 125;        // 아이언헤드
        public const int PoisonJab = 126;       // 독찌르기
        public const int DragonClaw = 127;      // 드래곤크루


        // ==========================================
        // 특수
        // ==========================================

        public const int Struggle = 999;        // 발버둥
    }
}