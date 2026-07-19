namespace Pokemongame
{
    public abstract class MonoBehaviour : Component
    {
        //내가 직접 만드는 컴포넌트 시작
        public T? GetComponent<T>() where T : Component        
            =>GameObject.GetComponent<T>();
    }

    public class CharacterComponent : MonoBehaviour
    {
        //포켓몬의 변하지 않는 데이터 컴포넌트.
        public PokemonRuntime Runtime {get; private set;} = null!;
        public PokemonData Data => Runtime.Data;
        public string PokemonName => Data.Name;
        public List<LevelUpOutoMove> LevelUpOutoMoves => Data.LevelUpOutoMoves;
        
        public IReadOnlyList<MoveRuntime> Moves => Runtime.CurrentMoves;
        public bool IsFainted => Runtime.CurrentHp <= 0;
       
        public void Initialize(PokemonData data, int level)
        {
            Runtime = new PokemonRuntime(data, level);
        }
        public void ReadPokemon()
        {
            this.LogReadPokemon();
        }
    }

    public class MoveComponent : MonoBehaviour
    {
        //포켓몬의 기술을 추가 및 교체하는 컴포넌트
        private int _lastcheckedMoveIndex = 0;
        public void LearnMove(CharacterComponent pokemon, MoveData newMove)
        {
            if (pokemon.Runtime.IsFulledMoveSlot())  
            {   
                 //가득 차 있을 때
                pokemon.LogMoveSlotsFull( newMove );
                if(InputManager.GetYesOrNo())
                {
                    GameLog.LogSelectForgetMove();
                    int slotToForget = InputManager.GetMoveSlotChoice();
                    
                    pokemon.Runtime.InsertMove(newMove,slotToForget);
                }
                else                                     
                {
                    //기술을 안배울 때
                    pokemon.LogGiveUpLearning(newMove);
                }
            }
            else                                
            {
                //가독 차지 않았을 때
                int emptySlotIndex = pokemon.Runtime.GetFirstEmptyIndex();
                //앞에서부터 빈슬롯 넣기
                pokemon.Runtime.InsertMove(newMove, emptySlotIndex);   
            }
        }
        public void IsOutoLearnMove(CharacterComponent pokemon)
        {
            if(pokemon.LevelUpOutoMoves[_lastcheckedMoveIndex].Level == pokemon.Runtime.Level)
            {
                int key = pokemon.LevelUpOutoMoves[_lastcheckedMoveIndex].MoveKey;
                MoveData move = MoveCategory.Move[key];

                if(pokemon.LevelUpOutoMoves[_lastcheckedMoveIndex+1] != null)
                    _lastcheckedMoveIndex ++;

                pokemon.LogWantLearnMove(move);

                if(InputManager.GetYesOrNo())
                {
                    LearnMove(pokemon,move);
                }
            }
        }
        /*
        public void Initialize(int LearnMovesKey)
        {
            var pokemon = GetComponent<CharacterComponent>();
        }
        */
    }
}
