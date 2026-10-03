using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Items;
using MyGame.Logs;
using MyGame.BattleStatus;
using MyGame.NameTables;

namespace MyGame.BattleSystems
{
    public static class BattleLog
    {
        public static void LogCurrentStat(PokemonRuntime playerPokemon,PokemonRuntime enemyPokemon)
        {
            GameLog.Info($"내 {NameTable.GetPokemon(playerPokemon!.Id)}의 현재 상태 [Lv.{playerPokemon.Level} hp: {playerPokemon.CurrentHp}/{playerPokemon.MaxHp}]");
            GameLog.Info($"상대 {NameTable.GetPokemon(enemyPokemon!.Id)}의 현재 상태 [Lv.{enemyPokemon.Level} hp: {enemyPokemon.CurrentHp}/{enemyPokemon.MaxHp}]");
        }

        public static void LogEffective(float finalMultiplier)
        {
            if(finalMultiplier > 1) GameLog.Info("효과가 굉장했다!");
            else if(finalMultiplier < 1) GameLog.Info("효과가 별로인 듯하다...");
        }

        public static void LogAttack(this IBattlePokemon attacker, MoveData move) 
            => GameLog.Info($"{NameTable.GetPokemon(attacker.Id)}의 {MoveNameTable.Get(move.Key)}!");
       
        public static void LogDamage(this IBattlePokemon defender, int damage) 
            => GameLog.Info($"{NameTable.GetPokemon(defender.Id)}에게 {damage}의 피해를 입혔다!");

        public static void LogStatusDamged(IBattlePokemon pokemon, EffectState status, int damage)
        {
            GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}이 {status}로 인해 {damage}의 피해를 입혔다!");
        }

        public static void LogFaint(this PokemonRuntime defender) 
        {
            GameLog.Info($"{NameTable.GetPokemon(defender.Id)}이(가) 쓰러졌다.");
            GameLog.Info("-------------------------------------");
        }
        public static void LogBattleResult(IBattlePokemon attacker, IBattlePokemon defender, MoveData move, int damage, float finalMultiplier)
        {
            GameLog.Info("-------------------------------------");
            attacker.LogAttack(move);
            Thread.Sleep(3000);
            defender.LogDamage(damage);
            Thread.Sleep(3000);
            LogEffective(finalMultiplier);
            GameLog.Info("-------------------------------------");
            Thread.Sleep(3000);
        }

        public static void LogSelectAction()
        {
            GameLog.Info("[1. 공격  ]  [3. 교체  ]");
            GameLog.Info("[2.아이템 ]  [4. 도망  ]");
        }

        public static void LogChoiceMove(this PokemonRuntime pokemon)
        {
            GameLog.Info("-------------------------------------");
            GameLog.Info($"{NameTable.GetPokemon(pokemon.Id)}은 어떤 스킬을 사용할까?");

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
    }
}