using MyGame.Pokemons;
using MyGame.Log;

namespace MyGame.Moves
{
    public static class MoveLog
    {
        public static void LogCurrentMoves(this PokemonRuntime pokemon)
        {
            for (int i = 0; i < pokemon.CurrentMoves.Count; i++)
            {
                var move = pokemon.CurrentMoves[i];
                // 출력 예시: 1. [ 몸통박치기 ] (PP: 30/35, 위력: 40)
                GameLog.Info($" {i + 1}. [ {move.Data.Name} ] (PP: {move.CurrentPP}/{move.MaxPP})");
            }
        }
        

        public static void LogLearnMove(this PokemonRuntime pokemon, MoveData newMove)
            => GameLog.Info($"{pokemon.Name}은(는) 새로운 기술 {newMove.Name}을(를) 배웠다!");

        public static void LogMoveSlotsFull(this PokemonRuntime pokemon, MoveData newMove)
        {
            GameLog.Info($"{pokemon.Name}은(는) 새로운 기술 {newMove.Name}을(를) 배우고 싶다...");
            GameLog.Info($"하지만 이미 기술이 4개로 가득 차 있다!");
            GameLog.Info($"새로운 기술을 위해 기존 기술 하나를 잊으시겠습니까?");
        }

        public static void LogForgetMove(this PokemonRuntime pokemon, MoveData oldMove)
            => GameLog.Info($"{pokemon.Name}은(는) {oldMove.Name}을(를) 깨끗이 잊었다!");
   
        public static void LogGiveUpLearning(this PokemonRuntime pokemon, MoveData newMove)
            => GameLog.Info($"{pokemon.Name}은(는) {newMove.Name} 배우기를 포기했다.");
    }
}