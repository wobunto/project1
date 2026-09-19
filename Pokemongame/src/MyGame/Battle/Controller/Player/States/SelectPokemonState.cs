using MyGame.Controllers;
using MyGame.Inputs;
using MyGame.Logs;
using MyGame.Pokemons;
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
            
        public override void Enter(PlayerController context)
        {
            context.View.DisplayPartyMenu(context.Player.Party);
        }
        
         public override void HandleInput(
            PlayerController context,
            Input input)
        {
            if(_canCancel && context.IsBack(input)) return;

            int index = input.Value - 1;
            
            if(index < 0 || index >= context.Player.Party.Count)
            {
                GameLog.Warn("선택 가능한 포켓몬 번호를 입력해주세요.");
                return;
            }

            
    
            var pokemon = context.Player.Party[index];
            
            if (!_filter(pokemon))
            {
                GameLog.Warn("선택할 수 없는 포켓몬입니다.");
                return;
            }
            
            context.PopState();
            _onSelected?.Invoke(index); // 상위 상태에서 등록한 콜백 실행  
        }
    }
}