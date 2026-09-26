using MyGame.Moves;
using MyGame.Types;
using MyGame.BattleStatus;

namespace MyGame.Pokemons
{
    public interface IBattlePokemon
    {  
        string Name {get;}
        int MaxHp { get; }
        bool IsPlayers {get; }
        IReadOnlyList<PokemonType> Types {get;}
        IReadOnlyList<MoveRuntime> CurrentMoves {get;}

        int Level {get;}
        int CurrentAttackDamage { get; }
        int CurrentSpeed {get;}
        bool IsFainted {get;}
        public EffectState CurrentEffectState {get;}
        
        bool TryHeal(int amount);
        void TakeDamage(int damage); 

        void ModifySpeedStage(int amount);
        void ModifyAttackStage(int amount);
        bool TrySetEffectState(EffectState state);

        BeforeActionResult TryExecute();
        MoveUsageResult TryGetUsableMove(int index, out MoveRuntime? move);
        bool HasAnyUsableMove();
    }

    public interface IItemTarget
    {
        string Name {get;}
        int MaxHp { get; }

        bool IsFainted { get; }
        int CurrentHp { get; }
    
        bool TryHeal(int amount);
        bool TryFullHeal();
        bool TryRevive();
        
        bool TrySetEffectState(EffectState state);
        void ModifySpeedStage(int amount);
        void ModifyAttackStage(int amount);
    }
}