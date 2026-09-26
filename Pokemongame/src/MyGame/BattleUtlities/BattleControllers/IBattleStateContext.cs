using MyGame.Views;
using MyGame.Trainers;
using MyGame.ControllerStates;
using MyGame.Commands;
using MyGame.Inputs;

namespace MyGame.BattleControllers
{
    public interface IBattleStateContext
    {
        IPlayerView View { get; }
        IBattleTrainer Player { get; }
        IBattleTargetTrainer Enemy { get; }
        bool ForceSwitch { get; }
        
        void PushState(PlayerState nextState);
        void PopState();
        bool TryBackState(Input input);
        void FinishedTurn(IBattleCommand command);
    }
}