using Raylib_cs;
using System.Numerics;
using MooseEngine.Scenes;

// Spritesheet path "..\..\..\Resources\Textures\Tilemap_Modified.png"

namespace MooseEngine.Core
{
    public static class Renderer
    {
        private static Texture2D _spriteSheet;
        private static int _spriteSize;
        private static int _offSet;
        private static int _padding;

        public static void Initialize(string spriteSheetPath, int offSet = 1, int padding = 1, int spriteSize = 8)
        {
            _offSet = offSet;
            _padding = padding;
            _spriteSize = spriteSize;
            _spriteSheet = Raylib.LoadTexture(spriteSheetPath);

            Raylib.SetTargetFPS(60);

            // TODO: add null check
        }

        public static void Begin()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.WHITE);
        }

        public static void End()
        {
            Raylib.EndDrawing();
        }

        public static void RenderEntity(Entity entity)
        {
            Vector2 spritePosition = entity.SpriteCoords;
            Rectangle pixelSourcePosition = new Rectangle(_offSet + ((_spriteSize + _padding) * spritePosition.X), _offSet + ((_spriteSize + _padding) * spritePosition.Y), _spriteSize, _spriteSize);
            Rectangle pixelDestinationPosition = new Rectangle(entity.Position.X, entity.Position.Y, entity.Scale.X, entity.Scale.Y);

            Raylib.DrawTexturePro(_spriteSheet, pixelSourcePosition, pixelDestinationPosition, Vector2.Zero, 0.0f, entity.ColorTint);
        }
    }
}
