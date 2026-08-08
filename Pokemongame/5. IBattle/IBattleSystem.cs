using System.Collections;
using System.Runtime.CompilerServices;
namespace Pokemongame
{
    public class BattleSystem
    {
        private List<IBattleAction> _turnActions = new List<IBattleAction>();

        public void StartBattle(TrainerRuntime player, TrainerRuntime enemy)
        {
            GameLog.Info("포켓몬 배틀을 시작합니다!");
            
            while (!player.ActivePokemon.IsFainted &&
                   !enemy.ActivePokemon.IsFainted)         //둘 중 하나가 기절하면 종료
            {
                BattleLog.LogCurrentStat(player.ActivePokemon, enemy.ActivePokemon);

                RegisterAction(player);   //플레이어가 행동을 결정하고 리스트에 담음
                RegisterAction(enemy);



                GameLog.Info("-----------------------------------");
            }
        }

        public void RegisterAction(TrainerRuntime trainer)
        {
            _turnActions.Add(BattleManager.SelectAction(trainer));
        }

        public void ResolveTurn()
        {
            // 1. 우선순위(Priority)가 높은 순서대로 내림차순 정렬
            // (SwapAction이 100이므로 가장 먼저, 그다음 스피드가 빠른 포켓몬의 공격)
            _turnActions.Sort((a, b) => b.Priority.CompareTo(a.Priority));

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

