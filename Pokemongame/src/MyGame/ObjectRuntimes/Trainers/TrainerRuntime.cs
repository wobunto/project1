using MyGame.Pokemons;
using MyGame.Logs;
using MyGame.Utilities;
using static MyGame.Rules.PokemonRules;

namespace MyGame.Trainers
{   
   public class TrainerRuntime : IBattleTrainer
   {
        // ==================================================
        // [1] 필드 및 프로퍼티
        // ==================================================
        private readonly List<PokemonRuntime> _party = new(MaxPartySlot);
        protected readonly Dictionary<int, int> _inventory = new();  //itemKey -> count

        public int NameId {get; init;}    
        public IBattlePokemon? ActivePokemon { get; private set;}

        public IReadOnlyList<PokemonRuntime> Party => _party;
        public IReadOnlyDictionary<int, int> Inventory => _inventory;

        public TrainerRuntime(int nameId)
        {
            NameId = nameId; 
            ActivePokemon = null;  //전투 진입 시점에 활성화.
        }
        // ==================================================
        // [2] 전투 필드 및 교체 (Battle Field / Active Pokemon)
        // ==================================================
         public bool CanBattle() 
            => GetAlivePokemonCount() > 0;
                
        public int GetAlivePokemonCount()
            => _party.Count(p => !p.IsFainted);

        public bool CanSwitch(IBattlePokemon targetPokemon)
        {
            if(targetPokemon == ActivePokemon || targetPokemon.IsFainted) //나중에 상태 중에서 교체 불가 상태를 검사
                return false;
            // bool이 아닌 enum으로 교체가 불가능한 이유를 추가할 수 있음.
            return true;
        } 

        public bool TrySetActivePokemon(int index)
        {
            if(!Utility.IsValidIndex(index, Party.Count))
            {
                GameLog.Warn($"유효하지 않은 파티 슬롯 인덱스입니다: {index}");
                return false;
            }

            PokemonRuntime pokemon = Party[index];

            if(pokemon.IsFainted)
              {
                GameLog.Warn("기절한 포켓몬은 전투에 내보낼 수 없습니다.");
                return false;
            }
            
            ActivePokemon = pokemon;
            return true;
        }

        public bool TrySetFirstActivePokemon()
        {
            for(int i = 0; i < Party.Count; i++)
                if(TrySetActivePokemon(i)) return true;
        
            return false;
        }

        public void ClearActivePokemon()
        {
            ActivePokemon = null;
        }
        // ==================================================
        // [3] 파티 관리 (Party Management)
        // ==================================================
        public bool TryAddPokemon(PokemonRuntime pokemon)
        {
            if (_party.Count >= MaxPartySlot)
            {
                GameLog.Info("파티 슬롯이 꽉 차있습니다. PC 보관함으로 전송을 고려해야 합니다.");
                return false; // 외부에서 false를 받아 PC 박스로 보낼지 판단 가능
            }

            _party.Add(pokemon);
            return true;
        }

        public bool TryRemovePokemon(int index)
        {
            if(!Utility.IsValidIndex(index, Party.Count))
            {
                GameLog.Warn("선택한 슬롯에 포켓몬이 없습니다.");
                return false;
            }
            _party.RemoveAt(index);
            
            return true;
        }
        // ==================================================
        // [4] 인벤토리 및 아이템 (Inventory Management)
        // ==================================================
        public bool HasItem(int itemKey) 
            => _inventory.TryGetValue(itemKey, out var count) && count > 0;
        
        public void AddItem(int itemKey, int amount = 1)
        {
            if (amount <= 0) return;

            _inventory[itemKey] = _inventory.GetValueOrDefault(itemKey, 0) + amount;
        }

        public bool TryUseItem(int itemKey)
        {
            if (!_inventory.TryGetValue(itemKey, out var current) || 
                current <= 0)
                return false;
            
            current -= 1;
            _inventory[itemKey] = current;

            if(current == 0)
                _inventory.Remove(itemKey);

            return true;
        }
    }
}