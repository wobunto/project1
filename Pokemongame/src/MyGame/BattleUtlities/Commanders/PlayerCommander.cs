using MyGame.Commands;
using MyGame.BattleControllers;
using MyGame.Inputs;
using MyGame.Pokemons;

namespace MyGame.BattleCommanders
{
    public class PlayerCommander : IBattleCommander
    {
        private BattleController _playerController;
        public int NameId {get;}

        public PlayerCommander(BattleController controller)
        {
            _playerController = controller;
            NameId = _playerController.Player.NameId;
        }

        public IBattleCommand SelectCommand()
        {
            _playerController.ResetState();

            while (!_playerController.IsTurnFinished)
            {
                // 입력 감지 (Unity의 Input, 콘솔의 Console.ReadKey 등)
                Input input = ConsoleInputManager.GetNumberKey();
                
                if (input.Type == InputType.None)
                {
                    Thread.Sleep(100);
                    continue;
                }
                
                _playerController.HandleInput(input);
            }

            return _playerController.SelectedCommand;
        }

        public TurnResult IsActivePokemonFainted()
        {
            var activePokemon = _playerController.Player.ActivePokemon!;
            if(activePokemon.IsFainted)
                return TurnResult.None;
                
            _playerController.PushForceSwitchState();

            if(!_playerController.Player.CanBattle())
                return TurnResult.AllFainted;

            while (!_playerController.IsTurnFinished)
            {
                // 입력 감지 (Unity의 Input, 콘솔의 Console.ReadKey 등)
                Input input = ConsoleInputManager.GetNumberKey();
                
                if (input.Type == InputType.None)
                {
                    Thread.Sleep(100);
                    continue;
                }
                
                _playerController.HandleInput(input);
            }

            return TurnResult.SwitchPokemon;
        }
    }
}