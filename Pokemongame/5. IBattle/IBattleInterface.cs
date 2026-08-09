namespace Pokemongame
{
    public interface IBattleAction
    {
        public MovePriority Priority { get;}
        public void Execute();       // 실제 행동 실행
    }

    public enum MovePriority
    {
        ForcedLast = -1,   //무조건 후공
        SpeedSlower = 0,
        SpeedFaster = 1,   //속도 비교해서 선공
        ForcedFirst = 2,     //무조건 선공
        NonAttackAction = 3 // 교체, 아이템, 도망
    }
}
   