using MyGame.BattleControllers;
using MyGame.Inputs;
using MyGame.Pokemons;
using MyGame.Utilities;
namespace MyGame.ControllerStates
{
    public class SelectPokemonState : PlayerState
    {
        private readonly Action<int> _onSelected;

        public Func<PokemonRuntime, bool> Filter {get;}
        public bool CanCancel {get; }

        public SelectPokemonState(
            Action<int> onSelected, 
            Func<PokemonRuntime, bool> filter, 
            bool canCancel = true
            )
        {
            _onSelected = onSelected;
            Filter = filter;
            CanCancel = canCancel;
        }
            
        public override void Enter(IBattleStateContext context)
        {
            if (!CanCancel)
            {

            }
        }
        
         public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
            if (input.IsCancel)
            {
                if (CanCancel)
                    context.PopState(); // 이전 상태(메뉴)로 복귀
                                
                else
                    // 강제 교체일 때는 취소 불가 메시지 출력

                return;
            }

            int select = input.Value - 1;
            
            if(!Utility.IsValidIndex(select, context.Player.Party.Count))
            {
              
                return;
            }

            var pokemon = context.Player.Party[select];
            
            if (!Filter(pokemon))
            {
              
                return;
            }
            
            context.PopState();
            _onSelected.Invoke(select); // 상위 상태에서 등록한 콜백 실행  
        }
    }
}