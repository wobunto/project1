namespace MyGame.BattleStatus
{
    public readonly struct BeforeActionResult
    {
        public bool CanAct { get; }
        public StatusEvent Event { get; }

        private BeforeActionResult(bool canAct, StatusEvent statusEvent)
        {
            CanAct = canAct;
            Event = statusEvent;
        }

        public static BeforeActionResult Pass
            => new(true, StatusEvent.None);

        public static BeforeActionResult Thawed
            => new(true, StatusEvent.Thawed);

        public static BeforeActionResult Frozen
            => new(false, StatusEvent.Frozen);

        public static BeforeActionResult Paralyzed
            => new(false, StatusEvent.Paralyzed);

        public static BeforeActionResult Asleep
            => new(false, StatusEvent.Asleep);

        public static BeforeActionResult WokeUp
            => new(true, StatusEvent.WokeUp);
    }
}
   
