using MyGame.Types;

namespace MyGame.Moves
{
    public class MoveRuntime
    {
        public MoveData Data { get; private set;}
        public int CurrentPP { get; private set; }

        public PokemonType MoveType { get; init;}
        public string Name { get; init;}
        public int MaxPP => Data.BasePP;

        public bool HasPP => CurrentPP > 0;

        public MoveRuntime(MoveData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            CurrentPP = data.BasePP;
            Name = data.Name;
            MoveType = data.Type;
        }

        // PP 소모 (0 밑으로 내려가지 않도록 방어)
        public bool TryConsumePP(int amount = 1)
        {
            if (CurrentPP < amount)
                return false;

            CurrentPP -= amount;
            return true;
        }

        // PP 회복 (최대 PP를 넘지 않도록 클램핑)
        public void RestorePP(int amount)
        {
            if (amount <= 0) return;
            CurrentPP = Math.Min(MaxPP, CurrentPP + amount);
        }
    }
}
