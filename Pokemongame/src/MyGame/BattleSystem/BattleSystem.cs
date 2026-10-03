using System.Linq;

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

        public void SetupBattle(IEnumerable<IBattleCommander> participants) //이때 플레이어가 제일 첫번쨰로 들어감.
        {
            _participants.Clear();
            _participants.AddRange(participants);

            _actionList.Clear();

            _isBattleOver = false;
            _isPlayerDefeated = false;
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

        private void RunBattleLoop()
        {
            while (!_isBattleOver)        
            {
                foreach (var p in _participants)
                {
                    _actionList.Add(p.SelectCommand());
                }
                
                 _actionList = _actionList             
                    .OrderByDescending(command => command.Priority) //Priority가 높은 순서로 정렬
                    .ThenByDescending(GetAttackSpeed)              //priority가 같고 둘다 Attack이면 Speed 순으로 정렬
                    .ToList();
                
                ExecuteActions();
            }
            BattleEnd();
        }
 
        private void ExecuteActions()
        {
            while(TryExecuteNextAction())
            {
                if(_isBattleOver)
                    return;
            }

            ProcessTurnEndEffects();

            if(_isBattleOver)
                return;
        }

        private bool TryExecuteNextAction()
        {
            if(_actionList.Count == 0)
                return false;

            _actionList[0].Execute();
            _actionList.RemoveAt(0);

            HandleFaintedPokemon();

            return true;
        }

        private void ProcessTurnEndEffects() 
        {
            foreach (var p in _participants)
            {             
                p.Trainer.ActivePokemon!.OnAfterAction();    /* 화상/독 데미지 등 */ 
                HandleFaintedPokemon();
            }
        }

        private void HandleFaintedPokemon()
        {
            foreach (var p in _participants)
           {            
                if(p.Trainer.ActivePokemon!.IsFainted)   
                {
                    if(p.Trainer.CanBattle())        
                        {
                            RemovePendingActionsFor(p);  
                            var switchCommand = p.GetForcedSwitchCommand();
                            switchCommand.Execute(); 
                        }     
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
            _actionList.RemoveAll(c => c.TrainerId == commander.Trainer.NameId); //나중에 포켓몬의 id로 판별 
        }

        private void BattleEnd()
        {
            if(_isPlayerDefeated)
            {
                //player가 졌을 때 돈을 잃고 기절한 포켓몬을 전부 살림.
         
            }

            //player가 이겼을 때 돈을 얻고 포켓몬 유지.
        }

        private int GetAttackSpeed(IBattleCommand command)  
        {
            if (command is AttackCommand attack)
                return attack.AttackerSpeed;

            return 0;
        }
    }
}


