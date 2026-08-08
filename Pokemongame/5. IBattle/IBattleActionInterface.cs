namespace Pokemongame
{
    public interface IBattleAction
    {
        public MovePriority Priority { get; }
        public void Execute();       // 실제 행동 실행
    }

}
   