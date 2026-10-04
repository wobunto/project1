using System.Threading.Tasks;
using MyGame.Commands;
using MyGame.BattleControllers;
using MyGame.Inputs;
using MyGame.Trainers;
using System;

namespace MyGame.BattleCommanders
{
    public class PlayerCommander : IBattleCommander
    {
        private readonly BattleController _playerController;
        
         // 비동기 입력을 기다리기 위한 약속(Promise) 객체
        private TaskCompletionSource<IBattleCommand>? _commandTcs;

        public int NameId { get; }
        public IBattleTrainer Trainer => _playerController.Player;

        public PlayerCommander(BattleController controller)
        {
            _playerController = controller;
            NameId = _playerController.Player.NameId;

            // 컨트롤러에서 턴이 끝났다는 신호가 오면 Task를 완료 처리함
            _playerController.OnCommandFinished += HandleCommandFinished;
            
            if (!_playerController.Player.TrySetFirstActivePokemon())
                throw new InvalidOperationException("현재 player의 ActivePokemon이 null 입니다.");
        }

        private void HandleCommandFinished(IBattleCommand command)
        {
            // 기다리고 있던 Task에 결과 전달 -> await가 풀리고 다음 로직 진행
            _commandTcs?.TrySetResult(command);
        }

        // 1. 일반 명령 선택
        public async Task<IBattleCommand> SelectCommandAsync()
        {
            _commandTcs = new TaskCompletionSource<IBattleCommand>();
            
            // 상태를 메뉴로 리셋 (Blazor 화면이 메뉴로 갱신됨)
            _playerController.ResetState();

            // 플레이어가 웹 화면에서 최종 행동을 클릭할 때까지 대기! (스레드 프리즈 없음)
            return await _commandTcs.Task;
        }

        // 2. 강제 교체 처리
        public async Task<IBattleCommand> GetForcedSwitchCommandAsync()
        {
            _commandTcs = new TaskCompletionSource<IBattleCommand>();
            
            // 강제 교체 상태 Push (Blazor 화면에 교체창 강제 출력)
            _playerController.PushForceSwitchState();

            return await _commandTcs.Task;
        }
    }
}