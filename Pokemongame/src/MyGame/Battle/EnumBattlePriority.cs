namespace MyGame.BattleSystems
{
    public enum BattlePriority
    {
        Behavior = 3,   // 트레이너/시스템 행동 (도망, 교체, 아이템 사용, 스킵 등 - 공격보다 무조건 먼저 처리)
        ForceFirst = 2,  //선공 기술
        Speed = 1,    //단순 속도 비교
        ForceLast = 0  // 후공
    }
}