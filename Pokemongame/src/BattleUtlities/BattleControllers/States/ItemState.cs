using MyGame.BattleControllers;
using MyGame.Commands;
using MyGame.Logs;
using MyGame.Inputs;
using MyGame.Utilities;
using MyGame.Items;
using MyGame.Pokemons;

namespace MyGame.ControllerStates
{
    public class ItemState : PlayerState
    {
        public IReadOnlyList<InventoryItem> Items {get; private set;}
             = Array.Empty<InventoryItem>();

        public override void Enter(IBattleStateContext context)
        {
            Items = GetValidInventory(context);
        }

        public override void Resume(IBattleStateContext context) 
        {
            Items = GetValidInventory(context);
        }

        
        public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
            if(context.TryBackState(input)) return; 
       
            int select = input.Value - 1;

            if(!Utility.IsValidIndex(select, Items.Count))
            {
                return;
            }
            
            var selectedItem = Items[select];
            ItemData itemData = selectedItem.Data;

            IItemEffect effect = ItemEffectFactory.Create(itemData.Effect);
            
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    IItemTarget pokemon = context.Player.Party[index];

                    var itemCmd = BattleCommandFactory.
                        CreateUseItemCommand( 
                            context.Player, 
                            pokemon,
                            itemData
                        );
                    
                    context.FinishedTurn(itemCmd);
                },
                filter: effect.CanApply // 도메인에 위임된 규칙
            );

            context.PushState(selectState);
            //아이템으로 회복은 물론 상태회복,PP회복, 
            //데미지, 스피드 등의 랭크업도 가능하니 IBattle로 많은 기능
        }
        
        private List<InventoryItem> GetValidInventory(IBattleStateContext context)
        {
            var result = new List<InventoryItem>();

            foreach(var (itemId, count) in context.Player.Inventory)
            {
                if (ItemDatabase.TryGetItem(itemId, out var data))
                {
                    var invenItem = new InventoryItem(data!, count);
                    result.Add(invenItem);
                }
                else 
                {
                    GameLog.Error($"존재하지 않는 아이템 ID({itemId})가 인벤토리에 있습니다.");
                }
            }

            return result;
        }
    }
}