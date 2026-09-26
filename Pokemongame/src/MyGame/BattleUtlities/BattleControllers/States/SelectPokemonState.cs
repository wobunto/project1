using MyGame.BattleControllers;
using MyGame.Inputs;
using MyGame.Pokemons;
using MyGame.Utilities;
namespace MyGame.ControllerStates
{
    public class SelectPokemonState : PlayerState
    {
        private readonly Action<int> _onSelected;
        private readonly Func<PokemonRuntime, bool> _filter;
        private readonly bool _canCancel;

        public SelectPokemonState(Action<int> onSelected, Func<PokemonRuntime, bool> filter, bool canCancel = true)
        {
            _onSelected = onSelected;
            _filter = filter;
            _canCancel = canCancel;
        }
            
        public override void Enter(IBattleStateContext context)
        {
            context.View.DisplayPartyMenu(context.Player.Party);
        }
        
         public override void HandleInput(
            IBattleStateContext context,
            Input input)
        {
            if (input.IsCancel)
            {
                if (_canCancel)
                    context.PopState(); // 이전 상태(메뉴)로 복귀
                                
                else
                    // 강제 교체일 때는 취소 불가 메시지 출력
                    context.View.DisplayMessage("지금은 교체를 취소할 수 없습니다. 포켓몬을 선택하세요!");
                return;
            }

            int index = input.Value - 1;
            
            if(!Utility.IsValidIndex(index, context.Player.Party.Count))
            {
                context.View.DisplayMessage("선택 가능한 포켓몬 번호를 입력해주세요.");
                return;
            }

            var pokemon = context.Player.Party[index];
            
            if (!_filter(pokemon))
            {
                context.View.DisplayMessage("선택할 수 없는 포켓몬입니다.");
                return;
            }
            
            context.PopState();
            _onSelected?.Invoke(index); // 상위 상태에서 등록한 콜백 실행  
        }
    }
}