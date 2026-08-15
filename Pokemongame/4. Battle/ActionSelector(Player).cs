namespace Pokemongame
{
    public class PlayerActionSelector : IActionSelector
    {
        private TrainerRuntime _player;
        private TrainerRuntime _enemy;

        public PokemonRuntime ActivePokemon => _player.ActivePokemon; 

        public PlayerActionSelector(BattleContext context)
        {
            _player = context.Player;
            _enemy = context.Enemy;
        }

        public ActionState SelectAction()
        {
            BattleLog.LogSelectAction();

            int input = InputManager.GetSlotChoice(TrainerRuntime.MaxPartySize);
            
            switch(input)
            {
                case 0:
                    return SelectMoveAction();

                case 1:
                    return SelectItemAction();
                    
                case 2:
                    return SelectSwitchAction();
                    
                default:
                    return TryRunAction();
            }
        } 
        
        private ActionState SelectMoveAction()
        {
            PokemonRuntime playerPokemon = _player.ActivePokemon;
            PokemonRuntime enemyPokemon = _enemy.ActivePokemon;

            if (playerPokemon.GetFirstEmptyIndex() == 0)
                throw new InvalidOperationException("[내 포켓몬]은 사용할 수 있는 기술이 없습니다.");

            while (true)
            {
                playerPokemon.LogChoiceMove();
                int input = InputManager.GetSlotChoice(TrainerRuntime.MaxMoveSlot);

                if (!playerPokemon.TryGetMove(input, out MoveRuntime? move))
                {
                    GameLog.Error("그 슬롯에는 기술이 없습니다.");
                    continue;
                }
        
                if(move!.CurrentPP <= 0)
                {
                    GameLog.Info("pp가 부족합니다.");
                    continue;
                }

                return new AttackState(playerPokemon, enemyPokemon, move);   
            }
        }

        private ActionState SelectSwitchAction()
        {      
            while (true)
            {
                BattleLog.LogParty(_player.Party!);

                int targetIndex = InputManager.GetSlotChoice(TrainerRuntime.MaxPartySize);

                SwitchResult result = _player.CanSwitchTo(targetIndex);

                if (result == SwitchResult.Success)
                    return new SwitchState(_player, targetIndex);

                BattleLog.LogSwitchFailed(result);
            }
        }
    
        private ActionState SelectItemAction()
        {
            if (_player.Inventory.Count == 0)
            {
                GameLog.Info("가지고 있는 아이템이 없습니다.");
                SelectAction();
            }

            var keys = _player.Inventory.Keys.ToList();

            while (true)
            {
                BattleLog.LogInventory(_player.Inventory);
                    
                int input = InputManager.GetSlotChoice(keys.Count);
                int itemKey = keys[input];
                    
                if (!ItemDatabase.TryGetItem(itemKey, out ItemData? data))
                {
                    GameLog.Error("아이템 데이터가 존재하지 않습니다.");
                    continue;
                }
                
                BattleLog.LogParty(_player.Party!);

                int targetIndex = InputManager.GetSlotChoice(TrainerRuntime.MaxPartySize);
                
                if (targetIndex > _player.NullSlotIndex())
                {
                    GameLog.Error("그 슬롯에는 포켓몬이 없습니다.");
                    continue;
                }

                var targetPokemon = _player.Party[targetIndex];

                if (targetPokemon!.IsFainted)
                {
                    GameLog.Info("포켓몬이 기절하여 아이템을 사용할 수 없습니다.");
                    continue;
                }

                return new UseItemState(
                    _player,
                    targetPokemon,
                    data!
                );
            }
        }

        private ActionState TryRunAction()
        {
            GameLog.Info("지금은 도망칠 수 없다.");
            SelectAction();
            
            return new ErrorState();
        }
    }
}