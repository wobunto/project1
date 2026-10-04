namespace MyGame.Types
{
    public static class TypeChart
    {
        private static int Max => (int)PokemonType.Max;
        private static readonly float[] _chart = new float[Max * Max];

        static TypeChart()
        {
            Array.Fill(_chart, 1.0f);

            // Normal
            PokemonType.Normal.Set(new[]
            {
                (PokemonType.Rock, 0.5f),
                (PokemonType.Ghost, 0.0f),
                (PokemonType.Steel, 0.5f)
            });

            // Fire
            PokemonType.Fire.Set(new[]
            {
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Grass, 2.0f),
                (PokemonType.Ice, 2.0f),
                (PokemonType.Bug, 2.0f),
                (PokemonType.Rock, 0.5f),
                (PokemonType.Dragon, 0.5f),
                (PokemonType.Steel, 2.0f)
            });

            // Water
            PokemonType.Water.Set(new[]
            {
                (PokemonType.Fire, 2.0f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Ground, 2.0f),
                (PokemonType.Rock, 2.0f),
                (PokemonType.Dragon, 0.5f)
            });

            // Electric
            PokemonType.Electric.Set(new[]
            {
                (PokemonType.Water, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Electric, 0.5f),
                (PokemonType.Ground, 0.0f),
                (PokemonType.Flying, 2.0f),
                (PokemonType.Dragon, 0.5f)
            });

            // Grass
            PokemonType.Grass.Set(new[]
            {
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Poison, 0.5f),
                (PokemonType.Ground, 2.0f),
                (PokemonType.Flying, 0.5f),
                (PokemonType.Bug, 0.5f),
                (PokemonType.Rock, 2.0f),
                (PokemonType.Dragon, 0.5f),
                (PokemonType.Steel, 0.5f)
            });

            // Ice
            PokemonType.Ice.Set(new[]
            {
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Grass, 2.0f),
                (PokemonType.Ice, 0.5f),
                (PokemonType.Ground, 2.0f),
                (PokemonType.Flying, 2.0f),
                (PokemonType.Dragon, 2.0f),
                (PokemonType.Steel, 0.5f)
            });

            // Fighting
            PokemonType.Fighting.Set(new[]
            {
                (PokemonType.Normal, 2.0f),
                (PokemonType.Ice, 2.0f),
                (PokemonType.Poison, 0.5f),
                (PokemonType.Flying, 0.5f),
                (PokemonType.Psychic, 0.5f),
                (PokemonType.Bug, 0.5f),
                (PokemonType.Rock, 2.0f),
                (PokemonType.Ghost, 0.0f),
                (PokemonType.Dark, 2.0f),
                (PokemonType.Steel, 2.0f)
            });

            // Poison
            PokemonType.Poison.Set(new[]
            {
                (PokemonType.Grass, 2.0f),
                (PokemonType.Poison, 0.5f),
                (PokemonType.Ground, 0.5f),
                (PokemonType.Rock, 0.5f),
                (PokemonType.Ghost, 0.5f),
                (PokemonType.Steel, 0.0f)
            });

            // Ground
            PokemonType.Ground.Set(new[]
            {
                (PokemonType.Fire, 2.0f),
                (PokemonType.Electric, 2.0f),
                (PokemonType.Grass, 0.5f),
                (PokemonType.Poison, 2.0f),
                (PokemonType.Flying, 0.0f),
                (PokemonType.Bug, 0.5f),
                (PokemonType.Rock, 2.0f),
                (PokemonType.Steel, 2.0f)
            });

            // Flying
            PokemonType.Flying.Set(new[]
            {
                (PokemonType.Electric, 0.5f),
                (PokemonType.Grass, 2.0f),
                (PokemonType.Fighting, 2.0f),
                (PokemonType.Bug, 2.0f),
                (PokemonType.Rock, 0.5f),
                (PokemonType.Steel, 0.5f)
            });

            // Psychic
            PokemonType.Psychic.Set(new[]
            {
                (PokemonType.Fighting, 2.0f),
                (PokemonType.Poison, 2.0f),
                (PokemonType.Psychic, 0.5f),
                (PokemonType.Steel, 0.5f),
                (PokemonType.Dark, 0.0f)
            });

            // Bug
            PokemonType.Bug.Set(new[]
            {
                (PokemonType.Fire, 0.5f),
                (PokemonType.Grass, 2.0f),
                (PokemonType.Fighting, 0.5f),
                (PokemonType.Poison, 0.5f),
                (PokemonType.Flying, 0.5f),
                (PokemonType.Psychic, 2.0f),
                (PokemonType.Ghost, 0.5f),
                (PokemonType.Dark, 2.0f),
                (PokemonType.Steel, 0.5f)
            });

            // Rock
            PokemonType.Rock.Set(new[]
            {
                (PokemonType.Fire, 2.0f),
                (PokemonType.Ice, 2.0f),
                (PokemonType.Fighting, 0.5f),
                (PokemonType.Ground, 0.5f),
                (PokemonType.Flying, 2.0f),
                (PokemonType.Bug, 2.0f),
                (PokemonType.Steel, 0.5f)
            });

            // Ghost
            PokemonType.Ghost.Set(new[]
            {
                (PokemonType.Normal, 0.0f),
                (PokemonType.Psychic, 2.0f),
                (PokemonType.Ghost, 2.0f),
                (PokemonType.Dark, 0.5f)
            });

            // Dragon
            PokemonType.Dragon.Set(new[]
            {
                (PokemonType.Dragon, 2.0f),
                (PokemonType.Steel, 0.5f)
            });

            // Dark
            PokemonType.Dark.Set(new[]
            {
                (PokemonType.Fighting, 0.5f),
                (PokemonType.Psychic, 2.0f),
                (PokemonType.Bug, 0.5f),
                (PokemonType.Ghost, 2.0f),
                (PokemonType.Dark, 0.5f),
                (PokemonType.Steel, 0.5f)
            });

            // Steel
            PokemonType.Steel.Set(new[]
            {
                (PokemonType.Fire, 0.5f),
                (PokemonType.Water, 0.5f),
                (PokemonType.Electric, 0.5f),
                (PokemonType.Ice, 2.0f),
                (PokemonType.Rock, 2.0f),
                (PokemonType.Steel, 0.5f)
            });
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static int GetIndex(
            PokemonType attack,
            PokemonType defend)
            => (int)attack * Max + (int)defend;

        private static void Set(
            this PokemonType attack,
            (PokemonType defend, float multiplier)[] values)
        {
            foreach (var (defend, multiplier) in values)
            {
                _chart[GetIndex(attack, defend)] = multiplier;
            }
        }

        public static float GetTypeMultiplier(
            this PokemonType attack,
            PokemonType defend)
            => _chart[GetIndex(attack, defend)];
    }

    public static class TypeEffectiveness
    {
        /// <summary>
        /// 듀얼 타입 방어 상성 누적 계산
        /// </summary>
        public static float CalculateTypeMultiplier(
            this PokemonType attackType,
            IReadOnlyList<PokemonType> defenseTypes)
        {
            float finalMultiplier = 1.0f;

            for (int i = 0; i < defenseTypes.Count; i++)
            {
                float multiplier =
                    attackType.GetTypeMultiplier(defenseTypes[i]);

                // 하나라도 무효면 최종 결과는 0배
                if (multiplier <= 0f)
                    return 0f;

                finalMultiplier *= multiplier;
            }

            return finalMultiplier;
        }
    }
}