namespace Pokemongame
{
    public abstract class BattleActor
    {
        public IBattleParticipant Participant {get;}
        
        public PokemonRuntime Pokemon => Participant.ActivePokemon;

        protected BattleActor(IBattleParticipant participant)
        {
             Participant = participant;
        }

        public abstract BattleAction SelectAction();
    }
}