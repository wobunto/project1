using MyGame.Logs;

namespace MyGame.BattleSystem
{
    public class BattleSystem
    {
        
        

        public void StartBattle()
        {
            GameLog.Info("포켓몬 배틀을 시작합니다!");

            while (true)        
            {
 




                GameLog.Info("-----------------------------------");
            }
        }
    }

    public class BattleTurnResolver
    {
       
        private bool CheckBattleEnd() => /* 모든 포켓몬 기절 여부 확인 */ false;
        private void ProcessTurnEndEffects() { /* 화상/독 데미지 등 */ }
    }
}


