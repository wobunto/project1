/*
namespace Pokemongame
{
    public class PlayerActionSelector : IActionSelector
    {
        public const int ActionSize = 4;
        private readonly TrainerRuntime _player;
        private readonly TrainerRuntime _enemy;

        public PokemonRuntime ActivePokemon => _player.ActivePokemon;

        public PlayerActionSelector(BattleContext context)
        {
            _player = context.Player;
            _enemy = context.Enemy;
        }

        public IBattleAction SelectAction()
        {
            // 유효한 액션을 고를 때까지 메인 메뉴를 반복
            while (true)
            {
                BattleLog.LogSelectAction(); // 0: 싸운다, 1: 가방, 2: 교체, 3: 도망

                // 4가지 선택지 (0~3)
                int menuChoice = InputManager.GetSlotChoice(ActionSize);

                IBattleAction? action = menuChoice switch
                {
                    0 => SelectMoveAction(),
                    1 => SelectItemAction(),
                    2 => SelectSwitchAction(),
                    3 => TryRunAction(),
                    _ => null
                };

                // 사용자가 뒤로가기를 눌렀거나(null 반환), 행동 선택에 실패한 경우 다시 메뉴 루프 진행
                if (action != null)
                    return action;
            }
        }

        private IBattleAction? SelectMoveAction()
        {
            PokemonRuntime playerPokemon = _player.ActivePokemon;
            PokemonRuntime enemyPokemon = _enemy.ActivePokemon;

            if (!playerPokemon.HasUsableMoves)
            {
                GameLog.Error("사용할 수 있는 기술이 존재하지 않습니다!");
                return null; // 뒤로가기 효과 (메인 메뉴로)
            }

            if (!playerPokemon.HasMovesPP)
            {
                GameLog.Info("사용할 수 있는 기술이 없어 발버둥 쳤다.");
                var move = 
                new AttackAction(playerPokemon, enemyPokemon, move);

            }

            while (true)
            {
                playerPokemon.LogChoiceMove(); // 예: 0~3번 기술, -1번은 뒤로가기
                int input = InputManager.GetSlotChoiceWithCancel(TrainerRuntime.MaxMoveSlot);

                // 뒤로가기 입력 시
                if (input == InputManager.CancelCode)
                    return null;

                if (!playerPokemon.TryGetMove(input, out MoveRuntime? move))
                {
                    GameLog.Error("그 슬롯에는 기술이 없습니다.");
                    continue;
                }

                if (move.CurrentPP <= 0)
                {
                    GameLog.Info("PP가 부족합니다.");
                    continue;
                }

                return new AttackAction(playerPokemon, enemyPokemon, move);
            }
        }

        private IBattleAction? SelectSwitchAction()
        {
            while (true)
            {
                BattleLog.LogParty(_player.Party);

                int targetIndex = InputManager.GetSlotChoiceWithCancel(TrainerRuntime.MaxPartySize);
                if (targetIndex == InputManager.CancelCode)
                    return null; // 뒤로가기

                SwitchResult result = _player.CanSwitchTo(targetIndex);
                if (result == SwitchResult.Success)
                {
                    return new SwitchAction(_player, targetIndex);
                }

                BattleLog.LogSwitchFailed(result);
            }
        }

        private IBattleAction? SelectItemAction()
        {
            if (!_player.HasAnyItem())
            {
                GameLog.Info("가지고 있는 도구가 없습니다.");
                return null; // 메인 메뉴로 복귀
            }

            while (true)
            {
                BattleLog.LogInventory(_player.Inventory);

                int itemSlot = InputManager.GetSlotChoiceWithCancel(_player.InventoryCount);
                if (itemSlot == InputManager.CancelCode)
                    return null; // 뒤로가기

                if (!_player.TryGetItemBySlot(itemSlot, out ItemData? itemData))
                {
                    GameLog.Error("유효하지 않은 도구입니다.");
                    continue;
                }

                // 타겟 포켓몬 선택
                BattleLog.LogParty(_player.Party);
                int targetIndex = InputManager.GetSlotChoiceWithCancel(TrainerRuntime.MaxPartySize);
                if (targetIndex == InputManager.CancelCode)
                    continue; // 도구 선택 화면으로 다시 루프

                if (!_player.TryGetPokemon(targetIndex, out PokemonRuntime? targetPokemon))
                {
                    GameLog.Error("그 슬롯에는 포켓몬이 없습니다.");
                    continue;
                }

                if (targetPokemon.IsFainted)
                {
                    GameLog.Info("기절한 포켓몬에게는 이 도구를 사용할 수 없습니다.");
                    continue;
                }

                return new ItemAction(_player, targetPokemon, itemData);
            }
        }

        private IBattleAction? TryRunAction()
        {
            // 트레이너전인지 야생전인지 확인 후 분기
            GameLog.Info("지금은 도망칠 수 없다!");
            return null; // 도망 불가 시 null을 반환하여 다시 메인 메뉴가 뜨게 함
        }
    }
}
*/