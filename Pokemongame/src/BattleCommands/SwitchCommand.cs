using MyGame.Trainers;
using MyGame.BattleSystems;
using MyGame.NameTables;

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
            int index
            )
        {
            _trainer = trainer;
            _index = index;
            TrainerId = _trainer.NameId;
        }
         
    
        public IReadOnlyList<string> Execute()
        {
            if(!_trainer.TrySetActivePokemon(_index))
                throw new InvalidOperationException("포켓몬이 ActivePokemon으로 지정되지 못헀습니다.");

            string[] str = new string[1];
            str[0] = $"가랏! {NameTable.GetPokemon(_trainer.Party[_index].Id)}!";
            return str;
        }
    }
}

