using System.Collections;
namespace Pokemongame
{
    /*
    public class BattleSystem
    {
        public void StartBattle(BattleActor player, BattleActor enemy)
        {
            GameLog.Info("포켓몬 배틀을 시작합니다!");
        
            while (!player.Pokemon.IsFainted &&
                   !enemy.Pokemon.IsFainted)         //둘 중 하나가 기절하면 종료
            {
                BattleLog.LogCurrentStat(player.Pokemon,enemy.Pokemon);

                BattleAction PlayerAction = player.SelectAction();
                BattleAction EnemyAction = enemy.SelectAction(); 

                ExecuteTurn(PlayerAction,EnemyAction);

                GameLog.Info("-----------------------------------");
            }
        }

        public void ExecuteTurn(BattleAction first, BattleAction second)
        { 
            int firstSpeed = first.Actor.Pokemon.CurrentSpeed;
            int secondSpeed = second.Actor.Pokemon.CurrentSpeed;

            bool isSecondFaster = Calculator.IsFaster(secondSpeed,firstSpeed);

            if(isSecondFaster)
            {
                BattleAction temp = first;
                first = second;
                second = temp;     
            }  
            
            ExecuteAction(first,second);
            if(second.Actor.Pokemon.IsFainted)
            {
                second.Actor.Pokemon.LogFaint();
                return;
            }  

            ExecuteAction(second,first);

            if(first.Actor.Pokemon.IsFainted)
            {
                first.Actor.Pokemon.LogFaint();
                return;
            }  
        }
        
        public void ExecuteAction(BattleAction action, BattleAction deffender)
        {
            switch(action)
            {
                case AttackAction attack:
                    ExecuteAttack(attack.Actor.Pokemon, deffender.Actor.Pokemon,attack.Move);
                    break;

                case SwitchAction sw:
                    if (sw.Actor.Participant is ISwitchable switchable)
                    {
                        var result = switchable.SwitchActive(sw.TargetIndex);
                        if (result != SwitchResult.Success)
                        {
                            GameLog.Error("포켓몬 교체에 실패했습니다.");
                        }
                    }
                    else
                    {
                        GameLog.Error("이 참가자는 교체를 지원하지 않습니다.");  // 야생은애초에 SwitchAction을 생성할 경로 자체가 없으므로 사실상 도달 불가
                    }
                    break;

                case ItemAction item:
                    Console.WriteLine("아직 미완성 ㅎㅎ;");
                    break;
            }
        }

        public void ExecuteAttack(PokemonRuntime attacker, PokemonRuntime deffender,MoveRuntime move)
        {
            // 내 공격력, 기술의 타입, 상대방의 타입을 가져와서 배틀 시스템 공식 적용
            move.ConsumePP(); 
            
            float TypeMultiplier = move.Data.Type.CalculateTypeMultipler(deffender.Data.Types);
            int currentAttack = attacker.CurrentAttack;
            int damage = Calculator.CalculateDamage(currentAttack,TypeMultiplier);
            
            deffender.TakeDamage(damage);

            attacker.LogBattleResult(deffender,move.Data,damage,TypeMultiplier);
        }
    }
    */
}
