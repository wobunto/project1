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

        public async Task StartTrainerBattleAsync() //우선은 트레이너 배틀만. 나중에 Dry를 생각해서 야생 포켓몬도 호환 가능하게 만들 예정
        {
            foreach (var p in _participants)
            {
                p.Trainer.TrySetFirstActivePokemon();
                // 필요하다면 UI에 "000이 나타났다!" 출력
            }
            _battleView?.DisplayMessage("배틀이 시작되었다!");
            await Task.Delay(1500); // 1.5초 연출 대기
            
            await RunBattleLoopAsync();
        }

        private async Task RunBattleLoopAsync()
        {
            while (!_isBattleOver)        
            {
                _actionList.Clear();

                foreach (var p in _participants)
                {
                    var command = await p.SelectCommandAsync();
                    _actionList.Add(command);
                }
                
                 _actionList = _actionList             
                    .OrderByDescending(command => command.Priority) //Priority가 높은 순서로 정렬
                    .ThenByDescending(GetAttackSpeed)              //priority가 같고 둘다 Attack이면 Speed 순으로 정렬
                    .ToList();
                
                await ExecuteActionsAsync();
            }
            BattleEnd();
        }
 
        private async Task ExecuteActionsAsync()
        {
            while(await TryExecuteNextActionAsync())
            {
                if(_isBattleOver) return;
            }

            await ProcessTurnEndEffectsAsync();
        }

        private async Task<bool> TryExecuteNextActionAsync()
        {
            if(_actionList.Count == 0)
                return false;

            var currentAction = _actionList[0];
            _actionList.RemoveAt(0);

            await ExecuteCommandWithPresentationAsync(currentAction);

            await HandleFaintedPokemonAsync();

            return true;
        }

        private async Task ProcessTurnEndEffectsAsync() 
        {
            foreach (var p in _participants)
            {             
                p.Trainer.ActivePokemon!.OnAfterAction();    /* 화상/독 데미지 등 */ 

                await HandleFaintedPokemonAsync();

                if(_isBattleOver) return;
            }
        }

        private async Task HandleFaintedPokemonAsync()
        {
            foreach (var p in _participants)
           {            
                if(p.Trainer.ActivePokemon != null &&
                p.Trainer.ActivePokemon.IsFainted)   
                {
                     _battleView?.DisplayMessage($"{p.Trainer.ActivePokemon.Id}은(는) 쓰러졌다!");
                    await Task.Delay(1000);

                    if(p.Trainer.CanBattle())        
                    {
                        RemovePendingActionsFor(p);  
                        var switchCommand = await p.GetForcedSwitchCommandAsync();
                        switchCommand.Execute(); 
                        await Task.Delay(1000);
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
                _battleView?.DisplayMessage("눈앞이 캄캄해졌다...");
                //player가 졌을 때 돈을 잃고 기절한 포켓몬을 전부 살림.
                return;
            }
            _battleView?.DisplayMessage("배틀에서 승리했다!");
            //player가 이겼을 때 돈을 얻고 포켓몬 유지.
        }

        private int GetAttackSpeed(IBattleCommand command)  
        {
            if (command is AttackCommand attack)
                return attack.AttackerSpeed;

            return 0;
        }

        private async Task ExecuteCommandWithPresentationAsync(IBattleCommand command)
        {
            // 1. 커맨드 종류에 따른 사전 연출 (필요 시)
            if (command is SwitchCommand)
            {
                _battleView?.DisplayMessage("포켓몬을 교체합니다!");
                await Task.Delay(1000);
            }

            // 2. 실제 데이터 연산(데미지 계산, PP 소모, 교체 등)은 기존 void Execute()로 순수하게 실행!
            var messages = command.Execute();

            // 3. 실행 후 연출 대기 (텍스트나 애니메이션이 보일 시간 부여)
            foreach (var message in messages)
            {
                _battleView?.DisplayMessage(message);
                await Task.Delay(1000); 
            }
        }
    }
}


