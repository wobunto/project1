using MyGame.Views;
using MyGame.Commands;
using MyGame.BattleCommanders;

namespace MyGame.BattleSystems
{
    public class BattleSystem
    {
        private IPlayerView _battleView;
        private bool _isPlayerCommand;
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
                
                if(!CanNextAction(player, enemy))
                    return;
              
                ExecuteAction();

                ProcessTurnEndEffects();

                if(!CanNextAction(player, enemy))
                    return;
            }
        }
    
        private void SelectAction(IBattleCommander selector)
            =>_actionList.Add(selector.SelectCommand());
    
        private void ExecuteAction()
        {
            _actionList[0].Execute();
            _isPlayerCommand = _actionList[0].IsPlayerCommand;
            
            _actionList.RemoveAt(0);
            
            Thread.Sleep(5000);
        }

        private bool CanNextAction(PlayerCommander player, AiCommander enemy)
        {
            var playerResult = player.IsActivePokemonFainted();
            var enemyResult = enemy.IsActivePokemonFainted();
            
            if(IsBatteEnd(playerResult, enemyResult))  
                return false;
            
            if(_actionList.Count <= 0)
                return true;

            if (_isPlayerCommand)
            {
                if(enemyResult == TurnResult.SwitchPokemon)
                    _actionList.RemoveAt(0);
            }
            else
            {
                if(playerResult == TurnResult.SwitchPokemon)
                     _actionList.RemoveAt(0);
            }

            return true;
        }

        private bool IsBatteEnd(TurnResult playerResult, TurnResult enemyResult)
        {
            if(playerResult == TurnResult.AllFainted)
            {
                PlayerWin();
                return true;
            }

            if(enemyResult == TurnResult.AllFainted)
            {
                PlayerLose();
                return true;
            }

            return false;
        }

        private void ProcessTurnEndEffects() { /* 화상/독 데미지 등 */ }
 
        private void PlayerWin()
        {
            BattleEnd();
        }

        private void PlayerLose()
        {
            BattleEnd();
        }

        private void BattleEnd()
        {
            //초기화 로직.
        }

        private int CompareCommand(IBattleCommand x, IBattleCommand y) //둘 다 행동이 같다면 트레이너 우선.
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
    }
}


