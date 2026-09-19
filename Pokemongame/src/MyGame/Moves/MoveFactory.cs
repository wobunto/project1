namespace MyGame.Moves
{
    public static class MoveFactory
    {
        private static MoveRuntime? _cachedStruggle;

        public static MoveRuntime Create(int key)
        {
            if (!MoveDatabase.TryGet(key, out var data))
                    throw new InvalidOperationException($"기술 ID {key}가 존재하지 않습니다.");
            
            return new MoveRuntime(data!);             
        }

        public static MoveRuntime GetStruggle()
        {
            return _cachedStruggle ??= Create(MoveDatabase.IdStruggle);
        }
    }
}
        