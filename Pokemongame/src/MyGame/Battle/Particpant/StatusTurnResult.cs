 namespace MyGame.BattleParticipant
 {
    public enum BeforeActionResult
    {
        CanAct,
        ASleep,
        Paralyzed,
        Frozen,
        Thawed,
        WokeUp
    }
    public enum TurnEndResult
    {
        None,            // 아무 일 없음
        Damaged,         // 데미지를 입음
        Fainted          // 데미지를 입고 쓰러짐
    }
}