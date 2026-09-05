using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Items;
using MyGame.Trainers;
using MyGame.Logs;

namespace MyGame.BattleSystem
{
    public static class BattleLog
    {
        public static void LogCurrentStat(PokemonRuntime playerPokemon,PokemonRuntime enemyPokemon)
        {
            GameLog.Info($"내 {playerPokemon.Name}의 현재 상태 [Lv.{playerPokemon.Level} hp: {playerPokemon.CurrentHp}/{playerPokemon.MaxHp}]");
            GameLog.Info($"상대 {enemyPokemon.Name}의 현재 상태 [Lv.{enemyPokemon.Level} hp: {enemyPokemon.CurrentHp}/{enemyPokemon.MaxHp}]");
        }

        public static void LogEffective(float finalMultiplier)
        {
            if(finalMultiplier > 1) GameLog.Info("효과가 굉장했다!");
            else if(finalMultiplier < 1) GameLog.Info("효과가 별로인 듯하다...");
        }

        public static void LogAttack(this IBattlePokemon attacker, MoveData move) 
            => GameLog.Info($"{attacker.Name}의 {move.Name}!");
       
        public static void LogDamage(this IBattlePokemon defender, int damage) 
            => GameLog.Info($"{defender.Name}에게 {damage}의 피해를 입혔다!");

        public static void LogFaint(this PokemonRuntime defender) 
        {
            GameLog.Info($"{defender.Name}이(가) 쓰러졌다.");
            GameLog.Info("-------------------------------------");
        }
        public static void LogBattleResult(this IBattlePokemon attacker, IBattlePokemon defender, MoveData move, int damage, float finalMultiplier)
        {
            GameLog.Info("-------------------------------------");
            attacker.LogAttack(move);
            defender.LogDamage(damage);
            LogEffective(finalMultiplier);
            GameLog.Info("-------------------------------------");
        }

        public static void LogSwitchFailed(SwitchResult result)
        {
            if(result == SwitchResult.NoPokemonInSlot)
            {
                GameLog.Info("포켓몬이 없습니다.");
            }
            else
            {
                GameLog.Info("포켓몬이 기절하여 교체할 수 없습니다.");
            }
        }

        public static void LogSelectAction()
        {
            GameLog.Info("[1. 공격  ]  [3. 교체  ]");
            GameLog.Info("[2.아이템 ]  [4. 도망  ]");
        }

        public static void LogChoiceMove(this PokemonRuntime pokemon)
        {
            GameLog.Info("-------------------------------------");
            GameLog.Info($"{pokemon.Name}은 어떤 스킬을 사용할까?");

            MoveLog.LogCurrentMoves(pokemon);
        }

        public static void LogInventory(IReadOnlyDictionary<int, int> inventory)
        {
            GameLog.Info("[ 아이템 목록 ]");
            int i = 0;
            foreach (var (key, count) in inventory)
            {
                ItemDatabase.TryGetItem(key, out var data);
                GameLog.Info($" {i}.[ {data?.Name ?? "알 수 없음"} x{count} ]");
                i++;
            }
        }

        public static void LogParty(IReadOnlyList<PokemonRuntime> party)
        {
            GameLog.Info("[ 포켓몬 목록 ]");

            for (int i = 0; i < party.Count; i++)
            {
                PokemonRuntime? pokemon = party[i];

                if (pokemon is null)
                {
                    GameLog.Info($" {i + 1}.[ 없음 ]");
                    continue;
                }

                GameLog.Info($" {i + 1}.[ {pokemon.Name} Lv.{pokemon.Level} HP:{pokemon.CurrentHp}/{pokemon.MaxHp} ]");
            }
        }
    }
}