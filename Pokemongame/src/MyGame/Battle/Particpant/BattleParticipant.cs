using MyGame.Pokemons;
using MyGame.States;
using MyGame.Trainers;
using MyGame.NameTables;
using MyGame.Controllers;
using MyGame.Commands;

namespace MyGame.BattleParticipants
{
    public class BattleParticipant
    {
        private Command _currentCommand;
        private IBattleController _controller;
        private readonly IBattleTrainer  _trainer;

        public EffectState CurrentPokemonState
            =>  _trainer.ActivePokemon.CurrentEffectState;
        
        public string Name 
            => NameTable.GetTrainer(_trainer.NameId);
        
        public int MaxHp
            => _trainer.ActivePokemon.MaxHp;

        public void TakeDamage(int damage)
            => _trainer.ActivePokemon.TakeDamage(damage);

        public BattleParticipant(
            IBattleController controller,
            IBattleTrainer trainer)
        {
            _currentCommand = new ErrorCommand();
            _controller = controller;
            _trainer = trainer;
        }

        public void SelectCommand()
        {
            _controller.Start();
        }
    }
}