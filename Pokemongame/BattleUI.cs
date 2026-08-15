namespace Pokemongame
{
    /*
    public class BattleUI : IDisposable
    {
        private PokemonRuntime? _target;

        public BattleUI(PokemonRuntime target)
        {
            _target = target;
            // 객체 생성과 동시에 구독 시작
            _target.OnHpChanged += UpdateHpBar;
        }

        private void UpdateHpBar(int CurrentHp)
        {
            // UI를 그릴 수 없으니 콘솔 출력으로 대체
            GameLog.Info($" 현재 체력: {CurrentHp}");
        }

        public void Dispose()
        {
            if (_target != null)
            {
                // 이벤트 구독 해지 (가장 중요)
                _target.OnHpChanged -= UpdateHpBar;
                _target = null;
            }
        }
    }
    */
}