using MyGame.Views;
using MyGame.Commands;
using MyGame.BattleCommanders;
using MyGame.Rules;

namespace MyGame.BattleSystems
{
     public class BattleSystem
    {
        private IPlayerView _battleView;
        private List<IBattleCommand> _actionList = new();
        private bool _isBattleOver = false;
        private bool _isPlayerDefeated = false;
        private readonly List<IBattleCommander> _participants = new();

        public BattleSystem(IPlayerView view)
        {
            _battleView = view;
        }

        public void SetupBattle(IEnumerable<IBattleCommander> participants)
        {
            _participants.Clear();
            _participants.AddRange(participants);
        }

        public void StartTrainerBattle() //우선은 트레이너 배틀만. 나중에 Dry를 생각해서 야생 포켓몬도 호환 가능하게 만들 예정
        {
            foreach (var p in _participants)
            {
                p.Trainer.TrySetFirstActivePokemon();
                // 필요하다면 UI에 "000이 나타났다!" 출력
            }
            
            RunBattleLoop();
        }

        public void RunBattleLoop()
        {
            while (!IsBattleEnd())        
            {
                foreach (var p in _participants)
                {
                    _actionList.Add(p.SelectCommand());
                }
                
                _actionList.Sort(CompareCommand);
                
                ExecuteActions();
            }
        }
 
        private void ExecuteActions()
        {
            while(TryExecuteNextAction())
            {
                HandleFaintedPokemon();
                
                if(_isBattleOver)
                    return;
            }
            ProcessTurnEndEffects();
        }

        private bool TryExecuteNextAction()
        {
            if(_actionList.Count == 0)
                return false;

            _actionList[0].Execute();
            _actionList.RemoveAt(0);

            return true;
        }

        private void ProcessTurnEndEffects() { /* 화상/독 데미지 등 */ }

        private void HandleFaintedPokemon()
        {
            foreach (var p in _participants)
            {
                if(p.Trainer.ActivePokemon!.IsFainted)
                {
                    if(p.Trainer.CanBattle())        
                        RemovePendingActionsFor(p);        
                    else
                    {
                        if(p.Trainer.NameId == PokemonRules.PlayerId)
                         {
                               _isPlayerDefeated = true; 
                         }
                        _isBattleOver = true;      
                    }     
                }
            }
        }
        
        private void RemovePendingActionsFor(IBattleCommander commander)
        {
            _actionList.RemoveAll(c => c.TrainerId == commander.Trainer.NameId);
        }

        private bool IsBattleEnd()
        {
            if(!_isBattleOver)
                return false;

            if(_isPlayerDefeated)
            {
                //player가 졌을 때
                return true;
            }

            //player가 이겼을 때

            return true;
        }

        private int CompareCommand(IBattleCommand x, IBattleCommand y) //둘 다 행동이 같다면 트레이너 우선.
        {
            int result = y.Priority.CompareTo(x.Priority);

             if (result != 0)
                return result;

            if (x is AttackCommand attackX &&
                y is AttackCommand attackY)
            {
                return attackY.AttackerSpeed.CompareTo(attackX.AttackerSpeed);
            }
            return 0;
        }
    }
}


