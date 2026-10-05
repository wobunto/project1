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

        public static string LogEffective(float finalMultiplier)
        {
            if (finalMultiplier > 1)
                return "효과가 굉장했다!";

            if (finalMultiplier > 0 && finalMultiplier < 1)
                return "효과가 별로인 듯하다.";

            if (finalMultiplier <= 0)
                return "효과가 없다...";

            return "";
        }

        public static string LogBeforeAction(int id, StatusEvent status)
        {
            string name = NameTable.GetPokemon(id);

            switch (status)
            {
                case StatusEvent.Thawed:
                    return $"{name}의 얼음이 녹았다.";

                case StatusEvent.WokeUp:
                    return $"{name}이 깨어났다.";

                case StatusEvent.Frozen:
                    return $"{name}은 얼어서 움직일 수 없다.";

                case StatusEvent.Paralyzed:
                    return $"{name}은 마비되어 움직일 수 없다.";

                case StatusEvent.Asleep:
                    return $"{name}은 잠들어 있어서 움직일 수 없다.";

                case StatusEvent.None:
                    return string.Empty;

                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static string LogAttack(
            this IBattlePokemon attacker,
            MoveData move)
        {
            return $"{NameTable.GetPokemon(attacker.Id)}의 {MoveNameTable.Get(move.Key)}!";
        }

        public static string LogDamage(
            this IBattlePokemon defender,
            int damage)
        {
            return $"{NameTable.GetPokemon(defender.Id)}에게 {damage}의 피해를 입혔다!";
        }

        public static void LogStatusDamged(
            IBattlePokemon pokemon,
            EffectState status,
            int damage)
        {
            GameLog.Info(
                $"{NameTable.GetPokemon(pokemon.Id)}이 {status}로 인해 {damage}의 피해를 입혔다!");
        }

        public static void LogFaint(this PokemonRuntime defender)
        {
            GameLog.Info(
                $"{NameTable.GetPokemon(defender.Id)}이(가) 쓰러졌다.");

            GameLog.Info("-------------------------------------");
        }

        public static IReadOnlyList<string> LogBattleResult(
            IBattlePokemon attacker,
            IBattlePokemon defender,
            MoveData move,
            int damage,
            float finalMultiplier,
            IReadOnlyList<string> effectMessages)
        {
            var messages = new List<string>
            {
                attacker.LogAttack(move),
                defender.LogDamage(damage),
                LogEffective(finalMultiplier)
            };

            messages.AddRange(effectMessages);

            return messages;
        }

        public static string LogSetStatus(EffectState status)
        {
            return $"{status} 상태가 되었다!";
        }

        public static string LogMyStatge(MoveEffect effect)
        {
            return $"내 포켓몬의 {effect}!";
        }

        public static string LogEnemyStatge(MoveEffect effect)
        {
            return $"상대 포켓몬의 {effect}!";
        }
    }
}