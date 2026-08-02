namespace Pokemongame
{
    public class PlayerActor : BattleActor
    {
        public PlayerActor(IBattleParticipant pokemon)
            : base(pokemon)
        {
        }

        public override BattleAction SelectAction()
        {
            BattleLog.LogSelectAction();
           
            int input = InputManager.GetSlotChoice(InputManager.MAX_SELECT_SLOTS);
            
            switch(input)
            {
                case 0:
                    MoveRuntime move = Pokemon.SelectMove();
                    return new AttackAction(this, move);

                case 1:
                    return new ItemAction(this);

                case 2:
                    return SelectSwitchAction();
                    
                default:
                    return new RunAction(this);
            }
        }

        private BattleAction SelectSwitchAction()
        {
            if (Participant is not ISwitchable switchable)
            {
                GameLog.Error("이 참가자는 교체를 지원하지 않습니다.");
                return SelectAction();                  // 메뉴로 복귀, 턴 소모 없음
            }

            while (true)
            {
                int targetIndex = InputManager.GetSlotChoice(InputManager.MAX_PARTY_SLOTS);
                SwitchResult result = switchable.CanSwitchTo(targetIndex);

                if (result == SwitchResult.Success)
                    return new SwitchAction(this, targetIndex);

                BattleLog.LogSwitchFailed(result); // 사유별 메시지 출력
                // 루프가 다시 돌아 재선택 — 턴은 아직 시작도 안 했으므로 소모될 게 없음
            }
        }
    }
}