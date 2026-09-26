using MyGame.Pokemons;
using MyGame.Items;
using MyGame.Trainers;
using MyGame.BattleSystems;

namespace MyGame.Commands
{ 
    public class UseItemCommand : IBattleCommand
    {
        private IBattleTrainer _trainer;
        private IItemTarget _pokemon;
        private ItemData _item;
        // 나중에 야생 포켓몬을 잡을 경우, 야생 포켓몬도 포켓몬런타임으로 받아야 함.(hp가 적을수록 혹은 특수 타입일 경우, 포획률을 조정해야 하기 때문)
        // 아직 미구현

        public BattlePriority Priority 
        {
            get => BattlePriority.Behavior;
        }  

        public UseItemCommand(
            IBattleTrainer trainer,
            IItemTarget pokemon,
            ItemData item
            )
        {
            _trainer = trainer;
            _pokemon = pokemon;
            _item = item;
        }

        public void Execute()
        {
            if(!_trainer.TryUseItem(_item.Key))
                throw new InvalidOperationException("현재 소지하지 않은 아이템을 선택했습니다.");
            
            IItemEffect effect = ItemEffectFactory.Create(_item.Effect);
            
            effect.Apply(_pokemon, _item.EffectValue);
              
            }
        }
    
}
