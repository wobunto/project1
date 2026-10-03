using MyGame.Pokemons;
using MyGame.Logs;
using MyGame.NameTables;

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
                GameLog.Info($" {i + 1}.[{ MoveNameTable.Get(move.Data.Key)}] (PP: {move.CurrentPP}/{move.MaxPP})");
            }
        }
        

        public static void LogLearnMove(this PokemonRuntime pokemon, MoveData newMove)
            => GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}은(는) 새로운 기술 {MoveNameTable.Get(newMove.Key)}(를) 배웠다!");

        public static void LogMoveSlotsFull(this PokemonRuntime pokemon, MoveData newMove)
        {
            GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}은(는) 새로운 기술 {MoveNameTable.Get(newMove.Key)}(를) 배우고 싶다...");
            GameLog.Info($"하지만 이미 기술이 4개로 가득 차 있다!");
            GameLog.Info($"새로운 기술을 위해 기존 기술 하나를 잊으시겠습니까?");
        }

        public static void LogForgetMove(this PokemonRuntime pokemon, MoveData oldMove)
            => GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}은(는) {MoveNameTable.Get(oldMove.Key)}(를) 깨끗이 잊었다!");
   
        public static void LogGiveUpLearning(this PokemonRuntime pokemon, MoveData newMove)
            => GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}은(는) {MoveNameTable.Get(newMove.Key)}배우기를 포기했다.");



        public static string GetErrorMessage(MoveUsageResult result) 
        => result switch
        {
            MoveUsageResult.NoPP => "PP가 모두 소진되어 사용할 수 없습니다.",
            MoveUsageResult.EmptySlot => "배우지 않은 기술 슬롯입니다.",
            _ => "지금은 그 기술을 사용할 수 없습니다."
        };
    }
}