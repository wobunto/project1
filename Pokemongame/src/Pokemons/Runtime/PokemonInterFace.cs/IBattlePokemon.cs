using MyGame.Moves;
using MyGame.Types;
using MyGame.BattleStatus;

namespace MyGame.Pokemons
{
    public interface IBattlePokemon : IViewPokemon
    {   
        bool TryHeal(int amount);
        void TakeDamage(int damage); 

        void ModifySpeedStage(int amount);
        void ModifyAttackStage(int amount);
        bool TrySetEffectState(EffectState state);

        BeforeActionResult CheckBeforeAction();
        void OnAfterAction();
        MoveUsageResult TryGetUsableMove(int index, out MoveRuntime? move);
        bool HasAnyUsableMove();
    }

    public interface IItemTarget
    {
        int Id {get;}
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