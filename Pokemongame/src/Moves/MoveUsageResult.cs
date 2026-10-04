namespace MyGame.Moves
{
    public enum MoveUsageResult
    {
        Success,       
        InvalidSlot,    // 잘못된 슬롯 인덱스 (음수 또는 최대 슬롯 범위 초과)
        EmptySlot,      // 기술이 등록되어 있지 않은 빈 슬롯
        NoPP          
    }
    
    
}