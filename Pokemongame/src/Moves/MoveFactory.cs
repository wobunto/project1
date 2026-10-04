using MyGame.Rules;

namespace MyGame.Moves
{
    public static class MoveFactory
    {
        private static MoveRuntime? _cachedStruggle;     //상대도 같은 인스턴스를 공유하지만 현재 정식 발매 할 것도 아니고 버그가 크지 않음

        public static MoveRuntime Create(int key)
        {
            if (!MoveDatabase.TryGet(key, out var data))
                    throw new InvalidOperationException($"기술 ID {key}가 존재하지 않습니다.");
            
            return new MoveRuntime(data!);             
        }

        public static MoveRuntime GetStruggle()
        {
            return _cachedStruggle ??= Create(MoveRules.Struggle);
        }
    }
}
        