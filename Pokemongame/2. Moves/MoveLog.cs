using static Pokemongame.GameLog;
namespace Pokemongame
{
    public static class MoveLog
    {
       public static void LogLearnMove(this PokemonRuntime pokemon, MoveData newMove)
            => Info($"{pokemon.Name}은(는) 새로운 기술 {newMove.Name}을(를) 배웠다!");

        public static void LogMoveSlotsFull(this PokemonRuntime pokemon, MoveData newMove)
        {
            Info($"{pokemon.Name}은(는) 새로운 기술 {newMove.Name}을(를) 배우고 싶다...");
            Info($"하지만 이미 기술이 4개로 가득 차 있다!");
            Info($"새로운 기술을 위해 기존 기술 하나를 잊으시겠습니까?");
        }
    }
}