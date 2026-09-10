using MyGame.Pokemons;
using MyGame.Items;
using MyGame.Trainers;

namespace MyGame.Commands
{ 
    public class UseItemCommand : Command
    {
        private const int _useOne = 1;
        private IBattleTrainer _trainer;
        private IItemTarget _pokemon;
        private ItemData _item;
        // 나중에 야생 포켓몬을 잡을 경우, 야생 포켓몬도 포켓몬런타임으로 받아야 함.(hp가 적을수록 혹은 특수 타입일 경우, 포획률을 조정해야 하기 때문)
        // 아직 미구현
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

        public override void Execute()
        {
            _trainer.ConsumeItem(_item.Key, _useOne);
            
            IItemEffect effect = ItemEffectFactory.Create(_item.Effect);
            
            effect.Apply(_pokemon, _item.EffectValue);
              
            }
        }
    
}
