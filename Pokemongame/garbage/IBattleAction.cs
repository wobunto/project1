namespace Pokemongame
{
    public interface IBattleAction
    {
        PokemonRuntime User { get; }
        int MovePriority { get; } // 기술 자체의 우선도 (기본 0, 전광석화 +1, 교체/도구 +6 등)

        // 행동 실행 직전에 유효한지 검사 (마비/수면, 타겟 사망, 공중날기 등 체크)
        bool CanExecute();

        // 실제 행동 실행
        void Execute();
    }

    public interface IActionSelector
    {
        
    }
}