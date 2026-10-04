namespace MyGame.Moves
{
    public enum MoveEffect
    {
        //상대에게 상태이상
        InflictBurn, 
        InflictPoison, 
        InflictBadlyPoison, 
        InflictParalysis, 
        InflictSleep, 
        InflictFreeze,    
        Confuse,
        
        //랭크 변화
        RaiseAttack, 
        LowerAttack, 
        RaiseDefense, 
        LowerDefense, 
        RaiseSpeed, 
        LowerSpeed, 
        RaiseSpecialAttack, 
        LowerSpecialAttack, 
        RaiseSpecialDefense, 
        LowerSpecialDefense, 
        RaiseAccuracy, 
        LowerAccuracy, 
        RaiseEvasion, 
        LowerEvasion,

        //상태 변화
        Heal,
        
        //턴 차징
        SemiInvulnerableCharge,   //공중 날기 등 (1턴 숨기 -> 2턴 공격)
        ChargeTurn               //1턴 모으기 -> 2턴 공격
    }
}