namespace Pokemongame
{
    public static class BattleManager
    {
        public static IBattleAction SelectAction(TrainerRuntime trainer)
        {
            
        } 

        public static void ExecuteAttack(PokemonRuntime attacker, PokemonRuntime defender,MoveRuntime move)
        {
            // 내 공격력, 기술의 타입, 상대방의 타입을 가져와서 배틀 시스템 공식 적용
            move.ConsumePP(); 
            
            float TypeMultiplier = move.Data.Type.CalculateTypeMultipler(defender.Data.Types);
            int currentAttack = attacker.CurrentAttack;
            int damage = Calculator.CalculateDamage(currentAttack,TypeMultiplier);
            
            defender.TakeDamage(damage);

            attacker.LogBattleResult(defender,move.Data,damage,TypeMultiplier);
        }
    }
}