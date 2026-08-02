using static Pokemongame.GameLog;
namespace Pokemongame
{
    public static class PokemonLog
    {
        public static void LogReadPokemon(this PokemonRuntime pokemon)
        {
            string typeInfo = string.Join("/",pokemon.Types);
            //타입이 몇 개든 알아서 ,로 구분
            Info($"포켓몬 : {pokemon.Name}");
            Info($"체력 : {pokemon.CurrentHp}/{pokemon.MaxHp}");
            Info($"공격력 : {pokemon.CurrentAttack}(공격력 랭크:{pokemon.AttackStage})");
            Info($"속도 : {pokemon.CurrentSpeed}(속도 랭크:{pokemon.SpeedStage})");
            Info($"타입 : {typeInfo}");
        }

     
        public static void LogForgetMove(this PokemonRuntime pokemon, MoveData oldMove)
            => Info($"{pokemon.Name}은(는) {oldMove.Name}을(를) 깨끗이 잊었다!");
   
        public static void LogGiveUpLearning(this PokemonRuntime pokemon, MoveData newMove)
            => Info($"{pokemon.Name}은(는) {newMove.Name} 배우기를 포기했다.");

    }
}