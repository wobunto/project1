using MyGame.Moves;
using MyGame.Types;
using MyGame.States;

namespace MyGame.Pokemons
{
    public interface IBattlePokemon
    {  
        string Name {get;}
        int SpeedStage {get;} 
        int Level {get;}
        int CurrentAttack { get; }
        int AttackStage {get; } 
        int MaxHp { get; }
        bool IsFainted {get;}

        EffectState CurrentEffectState {get; }

        IReadOnlyList<PokemonType> Types {get;}
        IReadOnlyList<MoveRuntime> CurrentMoves {get;}
        
        bool IsAbleMove();
        bool TryGetUseableMove(int index, out MoveRuntime? move);
        void TakeDamage(int damage); 

        MoveRuntime GetStruggle();
    }

    public interface IItemTarget
    {
        bool IsFainted { get; }
        int CurrentHp { get; }
        int MaxHp { get; }
    
        void Heal(int amount);
        void FullHeal();
        void SetEffectState(EffectState state);
        void Revive();
    }
}