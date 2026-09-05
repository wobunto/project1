/*
namespace Pokemongame
{
    public class AttackAction : IBattleAction
    {
        private readonly PokemonRuntime _attacker;
        private readonly PokemonRuntime _defender;
        private readonly MoveRuntime _move;

        public PokemonRuntime User => _attacker;
        public int MovePriority => _move.Data.Priority; // 기술 데이터의 우선도

        public AttackAction(PokemonRuntime attacker, PokemonRuntime defender, MoveRuntime move)
        {
            _attacker = attacker;
            _defender = defender;
            _move = move;
        }

        public bool CanExecute()
        {
            // 1. 공격자가 행동 전 기절했는가?
            if (_attacker.IsFainted)
                return false;

            // 2. 공격자의 상태이상(수면, 마비, 얼음 등)으로 행동이 불가능한가?
            if (!_attacker.CanMoveThisTurn())
                return false;

            // 3. 타겟이 이미 기절했는가?
            if (_defender.IsFainted)
            {
                GameLog.Info("하지만 타겟이 이미 쓰러져 있다!");
                return false;
            }

            // 4. 타겟이 공격 불가능 상태(공중날기, 다이빙 등)인가?
            if (_defender.IsInvulnerable && !_move.Data.CanHitInvulnerable)
            {
                GameLog.Info($"{_defender.Name}에게는 공격이 닿지 않았다!");
                return false;
            }

            return true;
        }

        public void Execute()
        {
            _move.ConsumePP();

            float multiplier = _move.Data.Type.CalculateTypeMultipler(_defender.Data.Types);
            int damage = Calculator.CalculateDamage(_attacker.CurrentAttack, multiplier);

            _defender.TakeDamage(damage);
            _attacker.LogBattleResult(_defender, _move.Data, damage, multiplier);

            // 공격 후 상대가 기절했는지 체크
            if (_defender.IsFainted)
            {
                GameLog.Info($"{_defender.Name}은(는) 쓰러졌다!");
            }
        }
    }

    public class SwitchAction : IBattleAction
{
    private readonly TrainerRuntime _trainer;
    private readonly int _targetIndex;

    public PokemonRuntime User => _trainer.ActivePokemon;
    public int MovePriority => 6; // 교체는 최우선 순위

    public SwitchAction(TrainerRuntime trainer, int targetIndex)
    {
        _trainer = trainer;
        _targetIndex = targetIndex;
    }

    public bool CanExecute()
    {
        // 교체할 대상 포켓몬이 기절 상태가 아닌지 등 검증
        return _trainer.CanSwitchTo(_targetIndex);
    }

    public void Execute()
    {
        _trainer.SwitchActive(_targetIndex);
    }
}
}
*/