using MyGame.Pokemons;
using MyGame.Logs;
using MyGame.Inputs;

/*
namespace MyGame.Moves
{
    
    public class MoveLearningService
    {
        private int _nextLevelUpMoveIndex = 0;

        public void AdvancePendingLevelUpMove() => _nextLevelUpMoveIndex++;

        public bool TryGetPendingLevelUpMoveKey(out int key)   //일정 레벨이 되었는지 판단하는 메서드인데, 무브 데이터 쪽에 있어도 될지도
        {
            var autoMoves = Data.LevelUpAutoMoves;

            if (_nextLevelUpMoveIndex >= autoMoves.Count || 
                autoMoves[_nextLevelUpMoveIndex].Level != Level)
            {
                key = default;
                return false;
            }

            key = autoMoves[_nextLevelUpMoveIndex].MoveKey;

            return true;
        }
        /// <summary>
        /// 플레이어가 기술을 배울 때의 콘솔 UI 상호작용을 처리합니다. 
        /// 나중에는 배틀 중에서도 레벨업하면 Move를 배울 수 있도록 PlayerControoler에 추가.
        /// </summary>
        public static void TeachMoveToPlayerPokemon(PokemonRuntime pokemon, int moveKey)
        {
            if (!MoveDatabase.TryGet(moveKey, out MoveData? moveData) || moveData == null)
            {
                GameLog.Warn($"[LearnMove] {moveKey}번 기술 데이터를 찾을 수 없습니다.");
                return;
            }

            // 1. 빈 슬롯이 있으면 즉시 배움
            if (pokemon.TryAddMove(moveData))
            {
                MoveLog.LogLearnMove(pokemon, moveData);
                return;
            }

            // 2. 슬롯이 꽉 찼을 때 (4개) -> 유저에게 덮어쓸지 묻기
            MoveLog.LogMoveSlotsFull(pokemon, moveData);

            if (!InputManager.GetYesOrNo())
            {
                MoveLog.LogGiveUpLearning(pokemon, moveData);
                return;
            }

            // 3. 잊을 기술 선택
            MoveLog.LogCurrentMoves(pokemon);

            int slotToReplace = InputManager.GetSlotChoice(PokemonRuntime.MaxMoveSlot);
            pokemon.InsertMove(moveData, slotToReplace);
            
            MoveLog.LogLearnMove(pokemon, moveData);
        }
    }
}
*/