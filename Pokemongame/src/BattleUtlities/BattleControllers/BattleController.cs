using MyGame.Trainers;
using MyGame.Commands;
using MyGame.ControllerStates;
using MyGame.Logs;
using MyGame.Views;
using MyGame.Inputs;
using MyGame.Pokemons;

namespace MyGame.BattleControllers
{
    public class BattleController : IBattleStateContext
    {   
        private readonly Stack<PlayerState> _stateStack = new Stack<PlayerState>();
        
        public event Action? OnStateChanged;
        public event Action<IBattleCommand>? OnCommandFinished;
        
        public PlayerState? CurrentState 
        {
                get => _stateStack.Count > 0 
                ? _stateStack.Peek() 
                : null;
        }

        public IBattleTrainer Player { get; }
        public IBattleTargetTrainer Enemy { get; }

        public bool ForceSwitch {get; private set;}
        public bool IsTurnFinished { get; private set; }

        public IBattleCommand SelectedCommand { get; private set; }
        
        public BattleController(
            IBattleTrainer player,
            IBattleTargetTrainer enemy
            )
        {
            Player = player;
            Enemy = enemy;

            ForceSwitch = false;
            SelectedCommand = BattleCommandFactory.Error;
        }

        public void Enter()
        {
            CurrentState?.Enter(this);
        }
        
        public void HandleInput(Input input)
        {
            CurrentState?.HandleInput(this, input);
        }
    
        public void PushState(PlayerState nextState)
        {
            _stateStack.Push(nextState); 
            CurrentState?.Enter(this);
            NotifyStateChanged(); 
        }
        
        public void PopState()
        {
            if (_stateStack.Count <= 1)
            {
                GameLog.Warn("메뉴에서는 돌아갈 수 없습니다.");
                return;
            }

            _stateStack.Pop();
            CurrentState?.Resume(this);
            NotifyStateChanged(); 
        }

        public void ResetState()
        {
             _stateStack.Clear();
            
            SelectedCommand = BattleCommandFactory.Error;
            
            IsTurnFinished = false;
            PushState(PlayerState.MenuState);
        }

        public bool TryBackState(Input input)
        {
            if (!input.IsCancel) 
                return false;
    
            PopState();
            return true;
        }

        public void FinishedTurn(IBattleCommand command)
        {
            SelectedCommand = command;
            ForceSwitch = false;
            IsTurnFinished = true;

             _stateStack.Push(PlayerState.WaitingState);
            OnCommandFinished?.Invoke(command);
            NotifyStateChanged(); 
        }

        public IBattlePokemon GetActivePokemon()
        {
            if(Player.ActivePokemon == null)
                throw new InvalidOperationException("현재 ActivePokemon이 null입니다.");

            return Player.ActivePokemon;
        }
        
        public void PushForceSwitchState()
        {
            ForceSwitch = true;
            IsTurnFinished = false;

            PushState(PlayerState.SwitchState);
        }

        private void NotifyStateChanged() => OnStateChanged?.Invoke();
    }
}