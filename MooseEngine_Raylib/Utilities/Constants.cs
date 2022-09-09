using System.Numerics;

namespace MooseEngine.Utilities
{
    public static class Constants
    {
        public const int DEFAULT_ENTITY_SIZE = 32;
        public const int DEFAULT_FONT_SIZE = 24;

        public static readonly Vector2 Up = new Vector2(0.0f, -DEFAULT_ENTITY_SIZE);
        public static readonly Vector2 Down = new Vector2(0.0f, DEFAULT_ENTITY_SIZE);
        public static readonly Vector2 Left = new Vector2(-DEFAULT_ENTITY_SIZE, 0.0f);
        public static readonly Vector2 Right = new Vector2(DEFAULT_ENTITY_SIZE, 0.0f);
    }
}
