using MyGame.Pokemons;
using  MyGame.Utilities;

namespace MyGame.BattleStatus
{   
    public class FreezeState : PokemonState
    {
        private int _stateTurn = 0;

        public FreezeState(IBattlePokemon pokemon) : base(pokemon) { }
        public override EffectState Kind => EffectState.Freeze;

        public override BeforeActionResult OnBeforeAction()
        {
            _stateTurn++;

            // 4턴째이거나 20% 확률로 해제
            if (_stateTurn > 3 || Utility.TryChance(20))
            {
                // _pokemon.CureStatus();  포켓몬 상태를 Normal/None으로 변경
                return BeforeActionResult.Thawed; // "얼음이 녹았다!"
            }

            return BeforeActionResult.Frozen; // "얼어붙어서 움직일 수 없다!"
        }
    }
}