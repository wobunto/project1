namespace Pokemongame
{
    public class AttackAction : IBattleAction
    {
        private PokemonRuntime _attacker;
        private PokemonRuntime _defender;
        private MoveRuntime _move;

        public MovePriority Priority
        {
            get
            {
                if (_attacker.CurrentSpeed >= _defender.CurrentSpeed)
                    return MovePriority.SpeedFaster;

                return MovePriority.SpeedSlower;
            }
        }

        public AttackAction(PokemonRuntime attacker, PokemonRuntime defender, MoveRuntime move)
        {
            _attacker = attacker;
            _defender = defender;
            _move = move;

          
        }

        public void Execute()
        {
            _move.ConsumePP(); 
            
            float TypeMultiplier = _move.Data.Type.CalculateTypeMultipler(_defender.Data.Types);
            int currentAttack = _attacker.CurrentAttack;
            int damage = Calculator.CalculateDamage(currentAttack, TypeMultiplier);
            
            _defender.TakeDamage(damage);

            _attacker.LogBattleResult(_defender, _move.Data,damage, TypeMultiplier);
        }
    }

    public class SwapAction : IBattleAction
    {    
        private TrainerRuntime _trainer;
        private int _index;

        MovePriority IBattleAction.Priority => MovePriority.NonAttackAction;

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

    public class ItemAction : IBattleAction
    {
        private TrainerRuntime _trainer;
        private PokemonRuntime _pokemon;
        private ItemData _item;

        MovePriority IBattleAction.Priority => MovePriority.NonAttackAction;

        public ItemAction(TrainerRuntime trainer,PokemonRuntime pokemon, ItemData item)      // 나중에 야생 포켓몬에게 몬스터볼을 던질 경우도 생각.
        {
            _trainer = trainer;
            _pokemon = pokemon;
            _item = item;
        }

        public void Execute() //현재는 회복 아이템만 가능
        {
            _trainer.ConsumeItem(_item.Key, 1);
            
            switch(_item.Effect)
            {
                case ItemEffectType.Heal:
                    _pokemon.Heal(_item.EffectValue);
                    break;

                case ItemEffectType.Capture:
                    GameLog.Info("미구현");
                    break;
            }
        }
    }   

    public class RunAction : IBattleAction
    {
        MovePriority IBattleAction.Priority => MovePriority.NonAttackAction;

        public void Execute()
        {
            //어차피 RunAction을 반환하지 않으므로 실행 X

            //나중에 야생 적과 조우시 추가
        }
    }   
}
