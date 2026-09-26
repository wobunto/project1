using MyGame.Views;
using MyGame.Commands;
using MyGame.BattleCommanders;

namespace MyGame.BattleSystem
{
    public class BattleSystem
    {
        private IPlayerView _battleView;
        private List<IBattleCommand> _actionList = new();

        public BattleSystem(IPlayerView view)
        {
            _battleView = view;
        }

        public void StartTrianerBattle(PlayerCommander player, AiCommander enemy)
        {
            _battleView.DisplayStartBattle(enemy);

            while (true)        
            {
                _battleView.DisplayMessage("-----------------------------------");
                
               SelectAction(player);
               SelectAction(enemy);
                
                _actionList.Sort(CompareCommand);

                ExecuteAction();
                
                ActionResult(player, enemy);
                
            }
        }
    
        private void SelectAction(IBattleCommander selector)
        {
            _actionList.Add(selector.SelectCommand());
        }
        
        private void ExecuteAction()
        {
            _actionList[0].Execute();
            _actionList.RemoveAt(0);
        }

        private void ActionResult(PlayerCommander player, AiCommander enemy)
        {
            player.IsActivePokemonFainted();
            enemy.IsActivePokemonFainted();
        }

        private int CompareCommand(IBattleCommand x, IBattleCommand y)
        {
            int Result = y.Priority.CompareTo(x.Priority);

             if (Result != 0)
                return Result;

            if (x is AttackCommand attackX &&
                y is AttackCommand attackY)
            {
                return attackY.AttackerSpeed.CompareTo(attackX.AttackerSpeed);
            }

            return 0;
        }

        private bool CheckBattleEnd() => /* 모든 포켓몬 기절 여부 확인 */ false;
        private void ProcessTurnEndEffects() { /* 화상/독 데미지 등 */ }

        
    }
}


