namespace Pokemongame
{
    public static class MoveLearning
    {
    // 트리거가 뭐든(레벨업/TM/이벤트) 공통으로 쓰는 상호작용 절차
        public static void TryLearn(this PokemonRuntime pokemon, int key)
        {
             if(!MoveDatabase.TryGet(key, out MoveData? move))
             {
                GameLog.Warn($"[LearnMove] {key} 기술을 배울 수 없습니다. (데이터 없음)");
                return;
             }
             
            int changeMoveSlot;


            if (pokemon.IsMoveSlotsFull())
            {
                pokemon.LogMoveSlotsFull(move!);
            
                if (!InputManager.GetYesOrNo())
                {
                    pokemon.LogGiveUpLearning(move!);
                    return;
                }
                changeMoveSlot = InputManager.GetSlotChoice(InputManager.MAX_MOVE_SLOTS);
            }    

            else 
            {
                changeMoveSlot = pokemon.GetFirstEmptyIndex();
            }

            pokemon.InsertMove(move!, changeMoveSlot);     
            pokemon.LogLearnMove(move!);     
        }
    
        public static void ProcessLevelUp(PokemonRuntime pokemon)
        {
            if (pokemon.TryGetPendingLevelUpMoveKey(out int moveKey))
            {
                TryLearn(pokemon, moveKey);
                pokemon.AdvancePendingLevelUpMove();
            }
        }
    }
}