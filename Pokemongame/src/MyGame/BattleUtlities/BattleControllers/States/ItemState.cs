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
        public override void Enter(IBattleStateContext context)
        {
            IReadOnlyList<InventoryItem> items = GetValidInventory(context);

            context.View.DisplayPokemon(context.Player.ActivePokemon!, context.Enemy.ActivePokemon!);
            context.View.DisplayItemMenu(items);
        }
        
        public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
            if(context.TryBackState(input)) return; 
       
            var items = GetValidInventory(context);

            int index = input.Value - 1;

            if(!Utility.IsValidIndex(index, items.Count))
            {
                context.View.DisplayMessage("아이템의 잘못된 번호를 선택하셨습니다.");
                return;
            }
            
            var selectedItem = items[index];
            ItemData itemData = selectedItem.Data;

            IItemEffect effect = ItemEffectFactory.Create(itemData.Effect);
            
            var selectState = new SelectPokemonState(
                onSelected: (index) =>
                {
                    IItemTarget pokemon = context.Player.Party[index];

                    var itemCmd = BattleCommandFactory.CreateUseItemCommand( context.Player, 
                                                                            pokemon,
                                                                            itemData);
                    
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