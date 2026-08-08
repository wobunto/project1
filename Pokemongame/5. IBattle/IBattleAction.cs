namespace Pokemongame
{
    public class MoveAction : IBattleAction
    {
        private PokemonRuntime _playerPokemon;
        private PokemonRuntime _enemyPokemon;
        private MoveRuntime _move;

        MovePriority IBattleAction.Priority => MovePriority.SpeedBased;

        public MoveAction(PokemonRuntime playerPokemon, PokemonRuntime enemyPokemon, MoveRuntime move)
        {
            _playerPokemon = playerPokemon;
            _enemyPokemon = enemyPokemon;
            _move = move;
        }

        public void Execute()
        {
            BattleManager.ExecuteAttack(_playerPokemon,_enemyPokemon,_move);
        }
    }

    public class SwapAction : IBattleAction
    {    
        private TrainerRuntime _trainer;
        private int _index;

        MovePriority IBattleAction.Priority => MovePriority.F;

        public SwapAction(TrainerRuntime trainer, int index)
        {
            _trainer = trainer; 
            _index = index;
        }

        public void Execute()
        {
            _trainer.SwitchActive(_index);
        }
    }
}
