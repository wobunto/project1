namespace MyGame.BattleStatus
{
    public enum StatusEvent
    {
        None,

        // 행동 가능
        Thawed,
        WokeUp,

        // 행동 불가
        Frozen,
        Paralyzed,
        Asleep
    }
}