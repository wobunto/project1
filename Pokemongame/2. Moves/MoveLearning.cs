namespace Pokemongame
{
    public static class MoveLearning
    {
    // 트리거가 뭐든(레벨업/TM/이벤트) 공통으로 쓰는 상호작용 절차
        public static void TryLearn(PokemonRuntime pokemon, MoveData move)
        {
            int? forgetSlot = null;

            if (pokemon.IsMoveSlotsFull())
            {
                pokemon.LogMoveSlotsFull(move);
            
                if (!InputManager.GetYesOrNo())
                {
                    pokemon.LogGiveUpLearning(move!);
                    return;
                }
            
                forgetSlot = InputManager.GetSlotChoice(InputManager.MAX_MOVE_SLOTS);
            }    
        
            pokemon.LearnMove(move!.key, forgetSlot);     
            pokemon.LogLearnMove(move);     
        }
    }
}