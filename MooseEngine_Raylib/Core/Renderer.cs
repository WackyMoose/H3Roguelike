using Raylib_cs;
using MooseEngine.Scene;
using System.Numerics;

// Spritesheet path "..\..\..\Resources\Textures\Tilemap_Modified.png"

namespace MooseEngine.Core
{
    public static class Renderer
    {
        private static Texture2D _spriteSheet;
        private static int _conversionFactor;
        private static int _spriteSize;

        public static void Initialize(string spriteSheetPath, int offSet = 1, int padding = 1, int spriteSize = 8)
        {;
            _conversionFactor = offSet + (spriteSize + padding);
            _spriteSize = spriteSize;
            _spriteSheet = Raylib.LoadTexture(spriteSheetPath); 

            // TODO: add check for null
        }

        public static void Begin()
        {
            Raylib.BeginDrawing();
        }

        public static void End()
        {
            Raylib.EndDrawing();
        }

        public static void RenderEntity(Entity entity)
        {
            Rectangle spritePosition = entity.SpriteCoords;
            Rectangle pixelSourcePosition = new Rectangle(_conversionFactor * spritePosition.x, _conversionFactor * spritePosition.y, _spriteSize, _spriteSize);
            Rectangle pixelDestinationPosition = new Rectangle(entity.Position.X, entity.Position.Y, entity.Scale.X, entity.Scale.Y);

            Raylib.DrawTexturePro(_spriteSheet, pixelSourcePosition, pixelDestinationPosition, Vector2.Zero, 0.0f, entity.ColorTint);
        }
    }
}
