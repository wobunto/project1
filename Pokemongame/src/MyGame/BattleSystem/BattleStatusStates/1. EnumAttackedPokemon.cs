namespace MyGame.BattleStatus
{
    public enum BeforeActionResult
    {
        None,                 //행동 가능 
        Thawed,                 //녹음
        WokeUp,                  //깨어남 

        ASleep,                 // 잠에 들어 행동 불능
        Paralyzed,              // 몸이 저려서 행동 불능
        Frozen                 //얼어붙어서 행동 불능 
    }
}
