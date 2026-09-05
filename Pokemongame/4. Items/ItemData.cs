namespace MyGame.Items
{
    public enum ItemEffectType
    {
        Heal,
        FullRestore,          // 모든 상태 회복
        Revive,        //기절 회복
        Capture           //아직 구현 x
    
    }

    public record ItemData(
        int Key, 
        string Name, 
        string Description, 
        ItemEffectType Effect, 
        int EffectValue
        );
}