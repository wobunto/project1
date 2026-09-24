using MyGame.Trainers;
using MyGame.Commands;
using MyGame.ControllerStates;
using MyGame.Logs;
using MyGame.Views;
using MyGame.Inputs;

namespace MyGame.Controllers
{
    public class PlayerController : IBattleController
    {   
        public IPlayerView View { get; }
        public IBattleTrainer Player { get; }
        public IBattleTarget Enemy { get; }
        
        public bool ForceSwitch {get; private set;}       //얘가 여기 필요한가? 아직 미구현 
        public bool IsTurnFinished { get; private set; }
        public Command SelectedCommand { get; private set; }
        
        private readonly Stack<PlayerState> _stateStack = new Stack<PlayerState>();
        
        public PlayerState CurrentState => _stateStack.Peek();

        public PlayerController(
            IBattleTrainer player,
            IBattleTarget enemy,
            IPlayerView view)
        {
            Player = player;
            Enemy = enemy;
            View = view;

            SelectedCommand = BattleCommandFactory.CreateErrorCommand();
        }
        public void Start()
        {
            
        }

        public void Enter()
        {
            CurrentState?.Enter(this);
        }
        
        public void HandleInput(Input input)
        {
            CurrentState?.HandleInput(this, input);
        }
    
        public void Update()
        {
            CurrentState?.Update(this);
        }

        public void PushState(PlayerState nextState)
        {
            _stateStack.Push(nextState); 
            CurrentState?.Enter(this);
        }
        
        public void PopState()
        {
            if (_stateStack.Count <= 1)
            {
                GameLog.Warn("메뉴에서는 돌아갈 수 없습니다.");
                return;
            }

            _stateStack.Pop();
        }

        public void ResetState()
        {
             _stateStack.Clear();
            
            SelectedCommand = BattleCommandFactory.CreateErrorCommand();
            
            IsTurnFinished = false;
            PushState(PlayerState.MenuSte);
        }

        public bool TryBackState(Input input)
        {
            if (!input.IsCancel) return false;
            
            PopState();
            
            return true;
        }

        public void FinishedTurn(Command command)
        {
            SelectedCommand = command;
            IsTurnFinished = true;
        }
    }
}