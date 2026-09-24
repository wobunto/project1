using MyGame.Views;
using MyGame.BattleParticipants;

namespace MyGame.BattleSystem
{
    public class BattleSystem
    {
        private IPlayerView _battleView;

        public BattleSystem(IPlayerView view)
        {
            _battleView = view;
        }

        public void StartTrianerBattle(BattleParticipant player, BattleParticipant Enemy)
        {
             
            _battleView.DisplayMessage($"{Enemy.Name}과의 배틀이 시작됐다!");

            while (true)        
            {
                 _battleView.DisplayMessage("-----------------------------------");
                
            }
        }
    
        public void TurnSelect(BattleParticipant selector)
        {
                selector.SelectCommand();
        }
    }

    public class BattleTurnResolver
    {
       
        private bool CheckBattleEnd() => /* 모든 포켓몬 기절 여부 확인 */ false;
        private void ProcessTurnEndEffects() { /* 화상/독 데미지 등 */ }
    }
}


