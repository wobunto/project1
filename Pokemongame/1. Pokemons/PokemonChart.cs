namespace Pokemongame
{
        public static class TypeChart
    {
        private static int Max => (int)PokemonType.Max;
        private static readonly float[] _chart = new float[Max * Max];
    
        static TypeChart()
        {
            Array.Fill(_chart, 1.0f);
            
           PokemonType.Normal.Set( new[]
                {
                    (PokemonType.Stone, 0.5f)
                } );
                
            // Fire
           PokemonType.Fire.Set(new[]
                {
                    (PokemonType.Fire, 0.5f),
                    (PokemonType.Water, 0.5f),
                    (PokemonType.Grass, 2.0f),
                    (PokemonType.Stone, 0.5f)
                } );

                // Water
                PokemonType.Water.Set(new[]
                {
                    (PokemonType.Fire, 2.0f),
                    (PokemonType.Water, 0.5f),
                    (PokemonType.Grass, 0.5f),
                    (PokemonType.Ground, 2.0f),
                    (PokemonType.Stone, 2.0f)
                } );

                // Grass
                PokemonType.Grass.Set(new[]
                {
                    (PokemonType.Fire, 0.5f),
                    (PokemonType.Water, 2.0f),
                    (PokemonType.Grass, 0.5f),
                    (PokemonType.Ground, 2.0f),
                    (PokemonType.Wind, 0.5f),
                    (PokemonType.Stone, 2.0f)
                } );

                // Electric
                PokemonType.Electric.Set(new[]
                {
                    (PokemonType.Water, 2.0f),
                    (PokemonType.Grass, 0.5f),
                    (PokemonType.Electric, 0.5f),
                    (PokemonType.Ground, 0.0f),
                    (PokemonType.Wind, 2.0f)
                } );

                // Ground
                PokemonType.Ground.Set(new[]
                {
                    (PokemonType.Fire, 2.0f),
                    (PokemonType.Grass, 0.5f),
                    (PokemonType.Electric, 2.0f),
                    (PokemonType.Wind, 0.0f),
                    (PokemonType.Stone, 2.0f)
                } );

                // Wind (Flying)
                PokemonType.Wind.Set(new[]
                {
                    (PokemonType.Grass, 2.0f),
                    (PokemonType.Electric, 0.5f),
                    (PokemonType.Stone, 0.5f)
                } );

                // Stone (Rock)
                PokemonType.Stone.Set(new[]
                {
                    (PokemonType.Fire, 2.0f),
                    (PokemonType.Ground, 0.5f),
                    (PokemonType.Wind, 2.0f)
                } );
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static int GetIndex(PokemonType attack, PokemonType defend)
            =>(int)attack * Max + (int)defend;

        private static void Set(this PokemonType attack, (PokemonType defend, float multiplier)[] values)
            {
                int atkIdx = (int)attack;
                foreach (var (defend, multiplier) in values)
                {
                    _chart[GetIndex(attack,defend)] = multiplier;  //나중에 함수로 만들어서 실수 없이 
                }
            }

        public static float GetTypeMultiplier(this PokemonType attack, PokemonType defend)
            =>_chart[GetIndex(attack,defend)];
    }
}