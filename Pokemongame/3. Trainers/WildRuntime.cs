namespace Pokemongame
{
    public class WildRuntime
    {
        private readonly PokemonRuntime _wild;
        public PokemonRuntime Wild => _wild; 

        public WildRuntime(PokemonRuntime wild) 
            => _wild = wild;

        public WildRuntime(int initialKey, int initialLevel)
        {
            _wild = PokemonFactory.Create(initialKey, initialLevel);
        }

        public void Encounter(int key, int level)
        {
            if (!PokemonCategory.TryGetPokemon(key, out var data))
                throw new InvalidOperationException($"포켓몬 ID {key}가 존재하지 않습니다.");

            _wild.Reinitialize(data!, level);
        }
    }
}