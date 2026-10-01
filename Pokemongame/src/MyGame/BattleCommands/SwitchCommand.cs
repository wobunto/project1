using MyGame.Trainers;
using MyGame.BattleSystems;

namespace MyGame.Commands
{
   public class SwitchCommand : IBattleCommand
    {
        private IBattleTrainer _trainer;
        private int _index;

        public int TrainerId {get;}

        public BattlePriority Priority 
        {
            get => BattlePriority.Behavior;
        }  

         public SwitchCommand(
            IBattleTrainer trainer,
            int index,
            int trainerId)
        {
            _trainer = trainer;
            _index = index;
            TrainerId = trainerId;
        }
         
    
        public void Execute()
        {
            if(!_trainer.TrySetActivePokemon(_index))
                throw new InvalidOperationException("포켓몬이 ActivePokemon으로 지정되지 못헀습니다.");
        }
    }
}

