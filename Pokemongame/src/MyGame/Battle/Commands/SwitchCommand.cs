using MyGame.Trainers;

namespace MyGame.Commands
{
   public class SwitchCommand : Command
    {
        private IBattleTrainer _trainer;
        private int _index;

        public SwitchCommand(
            IBattleTrainer trainer,
            int index)
        {
            _trainer = trainer;
            _index = index;
        }

        public override void Execute()
        {
            _trainer.SetActivePokemon(_index);
        }
    }
}

