namespace Pokemongame
{
    public class BattleSystem
    {
        private List<IBattleAction> _turnActions = new();
        
        public void StartBattle(PlayerRuntime player, EnemyRuntime enemy)
        {
            var Context = new BattleContext(player, enemy);
        
            GameLog.Info("포켓몬 배틀을 시작합니다!");

            while (!player.ActivePokemon.IsFainted &&
                   !enemy.ActivePokemon.IsFainted)         //둘 중 하나가 기절하면 종료
            {
                BattleLog.LogCurrentStat(player.ActivePokemon, enemy.ActivePokemon);

                var playerActor = new PlayerActionSelector(Context);   //플레이어가 행동을 결정하고 리스트에 담음
                var enemyActor = new EnemyActionSelector(Context);

                RegisterAction(playerActor.SelectAction());
                RegisterAction(enemyActor.SelectAction());

                ResolveTurn();

                GameLog.Info("-----------------------------------");
            }
        }

        public void RegisterAction(IBattleAction action)
        {
            _turnActions.Add(action);
        }

        public void ResolveTurn()
        {
            // 1. 우선순위(Priority)가 높은 순서대로 내림차순 정렬
            _turnActions = _turnActions.OrderByDescending(a => a.Priority).ToList();  //플레이어가 항상 먼저 담기니 속도가 같을 경우 플레이어 우선

            // 2. 정렬된 순서대로 행동 실행
            foreach (var action in _turnActions)
            {
                action.Execute();
            }

            // 3. 턴이 끝났으므로 리스트 초기화
            _turnActions.Clear();
        }
    }
}

