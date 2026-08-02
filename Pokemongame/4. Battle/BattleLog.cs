using static Pokemongame.GameLog;
namespace Pokemongame
{
    public static class BattleLog
    {
        public static void LogCurrentStat(PokemonRuntime playerPokemon,PokemonRuntime enemyPokemon)
        {
            Info($"내 {playerPokemon.Name}의 현재 상태 [Lv.{playerPokemon.Level} hp: {playerPokemon.CurrentHp}/{playerPokemon.MaxHp}]");
            Info($"상대 {enemyPokemon.Name}의 현재 상태 [Lv.{enemyPokemon.Level} hp: {enemyPokemon.CurrentHp}/{enemyPokemon.MaxHp}]");
        }

        public static void LogEffective(float finalMultiplier)
        {
            if(finalMultiplier > 1) Info("효과가 굉장했다!");
            else if(finalMultiplier < 1) Info("효과가 별로인 듯하다...");
        }

        public static void LogAttack(this PokemonRuntime attacker, MoveData move) 
            => Info($"{attacker.Name}의 {move.Name}!");
       
        public static void LogDamage(this PokemonRuntime defender, int damage) 
            => Info($"{defender.Name}에게 {damage}의 피해를 입혔다!");

        public static void LogFaint(this PokemonRuntime defender) 
        {
            Info($"{defender.Name}이(가) 쓰러졌다.");
            Info("-------------------------------------");
        }
        public static void LogBattleResult(this PokemonRuntime attacker, PokemonRuntime defender, MoveData move, int damage, float finalMultiplier)
        {
            Info("-------------------------------------");
            attacker.LogAttack(move);
            defender.LogDamage(damage);
            LogEffective(finalMultiplier);
            Info("-------------------------------------");
        }

        public static void LogSwitchFailed(SwitchResult result)
        {
            if(result == SwitchResult.NoPokemonInSlot)
            {
                Info("포켓몬이 없습니다.");
            }
            else
            {
                Info("포켓몬이 기절하여 교체할 수 없습니다.");
            }
        }

        public static void LogSelectAction()
        {
            Info("[1. 공격  ]  [3. 교체  ]");
            Info("[2.아이템 ]  [4. 도망  ]");
        }

        public static void LogChoiceMove(this PokemonRuntime pokemon)
        {
            int emptyIndex = pokemon.GetFirstEmptyIndex();
            int maxSlot = 4;
            Info("-------------------------------------");
            Info($"{pokemon.Name}은 어떤 스킬을 사용할까?");

            for(int i = 0; i<maxSlot; i++)
            {   
                if(i<emptyIndex || emptyIndex == -1)
                    Info($" {i+1}.[ {pokemon.CurrentMoves[i]!.Data.Name} ]");
                else
                    Info($" {i+1}.[ 없음 ]\n");
            }
        }
    }
}