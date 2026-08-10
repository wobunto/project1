namespace Pokemongame
{
    public class PlayerActionSelector : IActionSelector
    {
        private TrainerRuntime _player;
        private TrainerRuntime _enemy;

        public PlayerActionSelector(BattleContext context)
        {
            _player = context.Player;
            _enemy = context.Enemy;
        }

        public IBattleAction SelectAction()
        {
            BattleLog.LogSelectAction();

            int input = InputManager.GetSlotChoice(InputManager.MAX_SELECT_SLOTS);
            
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

        public IBattleAction ForceSwitchAction()
        {
            return SelectSwitchAction();
        }
        
        private IBattleAction SelectMoveAction()
        {
            
            PokemonRuntime playerPokemon = _player.ActivePokemon;
            PokemonRuntime enemyPokemon = _enemy.ActivePokemon;

            if (playerPokemon.GetFirstEmptyIndex() == 0)
                throw new InvalidOperationException("[내 포켓몬]은 사용할 수 있는 기술이 없습니다.");
                //본가 포켓몬에서는 사용할 수 있는 기술이 없다면 난동부리기 라는 기술을 사용하니 나중에 바꿀 것!

            while (true)
            {
                playerPokemon.LogChoiceMove();
                int input = InputManager.GetSlotChoice(InputManager.MAX_PARTY_SLOTS);

                if (!playerPokemon.TryGetMove(input, out MoveRuntime? move))
                {
                    GameLog.Error("그 슬롯에는 기술이 없습니다.");
                    continue;
                }
        
                if(move!.CurrentPP <= 0)
                { GameLog.Info("pp가 부족합니다.");
                    continue;
                }
                return new AttackAction(playerPokemon, enemyPokemon, move);   
            }
        }

        private IBattleAction SelectSwitchAction()
        {      
            while (true)
            {
                BattleLog.LogParty(_player.Party!);
                int targetIndex = InputManager.GetSlotChoice(InputManager.MAX_PARTY_SLOTS);
                SwitchResult result = _player.CanSwitchTo(targetIndex);

                if (result == SwitchResult.Success)
                    return new SwitchAction(_player, targetIndex);

                BattleLog.LogSwitchFailed(result); // 사유별 메시지 출력
                // 루프가 다시 돌아 재선택 — 턴은 아직 시작도 안 했으므로 소모될 게 없음
            }
        }
    
        private IBattleAction SelectItemAction()
        {
            if (_player.Inventory.Count == 0)
            {
                GameLog.Info("가지고 있는 아이템이 없습니다.");
                return SelectAction();
            }

            var keys = _player.Inventory.Keys.ToList();   //리스트로 매핑

            while (true)
            {
                BattleLog.LogInventory(_player.Inventory);
                    
                int input = InputManager.GetSlotChoice(keys.Count);
                int itemKey = keys[input];
                    
                if (!ItemDatabase.TryGetItem(itemKey, out ItemData? data))
                {
                    GameLog.Error("아이템 데이터가 존재하지 않습니다."); // 데이터 무결성 문제
                    continue;
                }
                
                BattleLog.LogParty(_player.Party!);
                int targetIndex = InputManager.GetSlotChoice(InputManager.MAX_PARTY_SLOTS);
                
                if (targetIndex > _player.NullSlotIndex())
                {
                     GameLog.Error("그 슬롯에는 포켓몬이 없습니다.");
                    continue;
                }

                var targetPokemon = _player.Party[targetIndex];

                if (targetPokemon!.IsFainted)   // 나중에 부활초, 기력의 조각처럼 기절한 포켓몬을 회복시키는 아이템이 나오면 수정
                {
                    GameLog.Info("포켓몬이 기절하여 아이템을 사용할 수 없습니다.");
                    continue;
                }

                return new ItemAction(_player ,_player.Party[targetIndex]! ,data!);
            }
        }

        private IBattleAction TryRunAction()
        {
            GameLog.Info("지금은 도망칠 수 없다."); 
                                                    //나중에 야생 포켓몬을 만들시 구현
            return SelectAction();                  // 메뉴로 복귀, 턴 소모 없음
            
        }
    }
}
