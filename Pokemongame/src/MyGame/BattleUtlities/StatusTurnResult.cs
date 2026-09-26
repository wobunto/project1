 namespace MyGame.BattleCommanders
 {
    public enum BeforeActionResult
    {
        CanAct,                 //행동 가능 
        ASleep,                 // 잠에 들어 행동 불능
        Paralyzed,              // 몸이 저려서 행동 불능
        Frozen,                 //얼어붙어서 행동 불능
        Thawed,                 //녹음
        WokeUp                  //깨어남 
    }

    public enum TurnResult
    {
        None,            // 아무 일 없음
        SwitchPokemon,    // 포켓몬을 교체함
        AllFainted       // 모든 포켓몬이 기절
    }
}