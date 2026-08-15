namespace Pokemongame
{
    public class EnemyActionSelector : IActionSelector
    {
        private EnemyRuntime _enemy;
        private PlayerRuntime _player;

        public EnemyActionSelector(BattleContext context)
        {
            _enemy = context.Enemy;
            _player = context.Player;
        }

        public ActionState SelectAction()
        {
            return SelectMove(
                _enemy.ActivePokemon,
                _player.ActivePokemon
            );
            // 아직 미완성, 우선 무작위 공격만
        }

        public ActionState ForceSwitchAction()
        {
            int index = 0;

            for (int i = 0; i < _enemy.NullSlotIndex(); i++)
            {
                if (!_enemy.Party[i]!.IsFainted)
                {
                    // 알고리즘 만들기 전 우선 앞에 있는 적부터 Swap
                    index = i;
                    break;
                }
            }

            return new SwitchState(_enemy, index);
        }

        public ActionState SelectMove(
            PokemonRuntime enemyPokemon,
            PokemonRuntime playerPokemon)
        {
            int firstEmptyIndex = enemyPokemon.GetFirstEmptyIndex();

            if (firstEmptyIndex == 0)
                throw new InvalidOperationException(
                    "[적 포켓몬]이 사용할 기술이 없습니다."
                );

            var validMoves = enemyPokemon.CurrentMoves;

            int index = Random.Shared.Next(
                firstEmptyIndex == -1 ? 4 : firstEmptyIndex
            );

            MoveRuntime move = validMoves[index]!;

            return new AttackState(
                enemyPokemon,
                playerPokemon,
                move
            );
        }

        public EffectState GetEffectState()
        {
            return _enemy.ActivePokemon.effectState;
        }
    }
}