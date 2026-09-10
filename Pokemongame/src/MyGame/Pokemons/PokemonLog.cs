using MyGame.Logs;

namespace MyGame.Pokemons
{
    public static class PokemonLog
    {
        public static void LogReadPokemon(this PokemonRuntime pokemon)
        {
            string typeInfo = string.Join("/",pokemon.Types);
            //타입이 몇 개든 알아서 ,로 구분
            GameLog.Info($"포켓몬 : {pokemon.Name}");
            GameLog.Info($"체력 : {pokemon.CurrentHp}/{pokemon.MaxHp}");
            GameLog.Info($"공격력 : {pokemon.CurrentAttack}(공격력 랭크:{pokemon.AttackStage})");
            GameLog.Info($"속도 : {pokemon.CurrentSpeed}(속도 랭크:{pokemon.SpeedStage})");
            GameLog.Info($"타입 : {typeInfo}");
        }
    }
}