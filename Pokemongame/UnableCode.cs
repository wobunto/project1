namespace Pokemongame
{
    /*
    public class Pokemon
    {
        //포켓몬의 변하지 않는 데이터 컴포넌트.
        public PokemonRuntime Runtime;
        public PokemonData Data =>.Data;
        public List<LevelUpMove> LevelUpAutoMoves => Data.LevelUpAutoMoves;
        public IReadOnlyList<MoveRuntime?> Moves =>.CurrentMoves;
        public IReadOnlyList<PokemonType> Types =>.Data.Types;
        public string PokemonName => Data.Name;
        public int Level =>.Level;
        public int CurrentHp =>.CurrentHp;
        public int MaxHp =>.MaxHp;
        public int CurrentAttack =>.CurrentAttack;
        public int CurrentSpeed =>.CurrentSpeed;
        public int AttackStage =>.AttackStage;
        public int SpeedStage =>.SpeedStage;

        
       
        public void Initialize(PokemonData data, int level)
        {
            Runtime = new PokemonRuntime(data, level); 
        }
        public void ReadPokemon()
        {
            this.LogReadPokemon();
        }
        public void GetPokemon(int key)
        {
            PokemonData? pokemon;
            if (PokemonCategory.TryGetPokemon(key, out pokemon))
            {
                Initialize(pokemon!, 50); //!!! 임의 레벨 50
            }
        }
    }
 
 


    public class MoveComponent 
    {
        //포켓몬의 기술을 추가 및 교체하는 컴포넌트
        private int _lastcheckedMoveIndex = 0;
        public void LearnMove(PokemonRuntime pokemon, MoveData newMove)
        {
            if (pokemon.IsMoveSlotsFull())  
            {   
                 //가득 차 있을 때
                pokemon.LogMoveSlotsFull( newMove );
                if(InputManager.GetYesOrNo())
                {
                    GameLog.Info($"잊을 기술의 번호를 선택하세요: [1~4]");
                    int slotToForget = InputManager.GetMoveSlotChoice();
                    
                    pokemon.InsertMove(newMove,slotToForget);
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
                int emptySlotIndex = pokemon.GetFirstEmptyIndex();
                //앞에서부터 빈슬롯 넣기
                pokemon.InsertMove(newMove, emptySlotIndex);   
            }
        }
        public void IsOutoLearnMove(PokemonRuntime pokemon)
        {
            if(pokemon.LevelUpAutoMoves[_lastcheckedMoveIndex].Level == pokemon.Level)
            {
                int key = pokemon.LevelUpAutoMoves[_lastcheckedMoveIndex].MoveKey;
                MoveData move = MoveCategory.Move[key];

                if(_lastcheckedMoveIndex < pokemon.LevelUpAutoMoves.Count)
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
        
    }
    */
}
