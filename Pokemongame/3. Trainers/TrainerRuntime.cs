using MyGame.Pokemons;
using MyGame.Moves;
using MyGame.Logs;


namespace MyGame.Trainers
{   
   public class TrainerRuntime : IBattleTrainer
   {
        public const int MaxPartySlot = 6;
        public const int MaxMoveSlot = 4;

        public IBattlePokemon ActivePokemon {get; private set;}

        private readonly List<PokemonRuntime> _party = new(MaxPartySlot);
        public IReadOnlyList<PokemonRuntime> Party => _party.AsReadOnly();

        protected readonly Dictionary<int, int> _inventory = new();  //itemKey -> count
        public IReadOnlyDictionary<int, int> Inventory => _inventory;

        public TrainerRuntime()
        {
            PokemonRuntime nonePokemon = PokemonFactory.Create(999,1);  // key 999는 임시 포켓몬으로 첫 시작시 포켓몬을 받거나 첫 시작이 아닐 시 오류가 나게 됨
            ActivePokemon = nonePokemon;
        }

        public void SetActivePokemon(int index)
        {
            PokemonRuntime pokemon = Party[index];
            if(pokemon.IsFainted)
                throw new InvalidOperationException("[NULL!] 현재 데이터로 받은 포켓몬이 없습니다.");
            
            ActivePokemon = pokemon;
        }

        public void RemovePokemon(int index)
        {
            if(_party.Count() < index)
            {
                GameLog.Info("포켓몬이 없습니다.");
                return;
            }

            _party.RemoveAt(index);
        }

        public bool CanBattle() 
            => GetAlivePokemonCount() > 0;
                
        public int GetAlivePokemonCount()
            => _party.Count(p => !p.IsFainted);
            
    
            public bool HasItem(int itemKey) 
            => _inventory.TryGetValue(itemKey, out var count) && count > 0;

            public bool TryUseItem(int itemKey)
            => ConsumeItem(itemKey,1);

            public bool ConsumeItem(int itemKey, int amount)
            {
                if (!_inventory.TryGetValue(itemKey, out var current) || 
                    current < amount)
                    return false;

                _inventory[itemKey] = current - amount;
                if (_inventory[itemKey] == 0)
                {
                    _inventory.Remove(itemKey);
                    //아이템을 모두 사용하셨습니다. 라는 메세지 출력
                }
                return true;
            }
            
            public bool CanSwitch(IBattlePokemon pokemon)
            {
                if (pokemon.IsFainted)
                {
                    GameLog.Warn("기절한 포켓몬은 교체할 수 없습니다.");
                    return false;
                }
            
                if (pokemon == ActivePokemon)
                {
                    GameLog.Warn("현재 배틀 중인 포켓몬입니다.");
                    return false;
                }
            
                return true;
            }

            public void CapturePokemon(PokemonRuntime pokemon)
            {
                if(_party.Count >= MaxPartySlot)
                {
                    GameLog.Info("포켓몬 슬롯이 꽉 차있습니다.");
                    return;
                }
                _party.Add(pokemon);
            }
    }
}