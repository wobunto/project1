namespace Pokemongame
{
    public class BattleSystem
    {
    
        public void StartBattle(PlayerRuntime player, EnemyRuntime enemy)
        {
            player.SetFirstActivePokemon();
            
            var context = new BattleContext(player, enemy);
    
            var playerActor = new Actor();

            GameLog.Info("포켓몬 배틀을 시작합니다!");

            while (player.CanBattle() && enemy.CanBattle())        
            {
                BattleLog.LogCurrentStat(player.ActivePokemon, enemy.ActivePokemon);

              //플레이어가 행동을 결정하고 리스트에 담음
           


               //플레이어가 항상 먼저 담기니 속도가 같을 경우 플레이어 우선인 코드 





        
                GameLog.Info("-----------------------------------");
            }
        }


    
    }
}

