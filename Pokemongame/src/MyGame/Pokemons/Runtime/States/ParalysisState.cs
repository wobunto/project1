using MyGame.Pokemons;
using MyGame.BattleParticipant;
using MyGame.Utilities;

namespace MyGame.States
{

   public class ParalysisState : PokemonState
{
    public ParalysisState(IBattlePokemon pokemon) : base(pokemon) { }

    public override BeforeActionResult OnBeforeAction()
    {
        // 25% 확률로 몸이 저려 행동 불가
        if (Utility.TryChance(25))
            return BeforeActionResult.Paralyzed;

        return BeforeActionResult.CanAct;
    }
}
}