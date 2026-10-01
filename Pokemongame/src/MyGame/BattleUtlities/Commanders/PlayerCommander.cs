using MyGame.Commands;
using MyGame.BattleControllers;
using MyGame.Inputs;
using MyGame.Trainers;

namespace MyGame.BattleCommanders
{
    public class PlayerCommander : IBattleCommander
    {
        private BattleController _playerController;
        
        public int NameId { get; }
        public IBattleTrainer Trainer => _playerController.Player;

        public PlayerCommander(BattleController controller)
        {
            _playerController = controller;
            NameId = _playerController.Player.NameId;
            
            if(!_playerController.Player.TrySetFirstActivePokemon())
                throw new InvalidOperationException("현재 player의 ActivePokemon이 null 입니다.");
        }

        // 1. 일반 명령 선택
        public IBattleCommand SelectCommand()
        {
            _playerController.ResetState();
            
            WaitUntilTurnFinished();

            return _playerController.SelectedCommand;
        }

        // 2. 강제 교체 처리
        public IBattleCommand GetForcedSwitchCommand()
        {
            _playerController.PushForceSwitchState();
     
            WaitUntilTurnFinished();

            return _playerController.SelectedCommand;
        }

        private void WaitUntilTurnFinished()
        {
            while (!_playerController.IsTurnFinished)
            {
                Input input = ConsoleInputManager.GetNumberKey();
                
                if (input.Type == InputType.None)
                {
                    Thread.Sleep(20); 
                    continue;
                }
                
                _playerController.HandleInput(input);
            }
        }
    }
}