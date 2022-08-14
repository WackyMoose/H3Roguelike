using Raylib_cs;
using System.Numerics;
using MooseEngine.Scenes;
using MooseEngine.Extensions.Runtime;
using MooseEngine.Utilities;

// Spritesheet path "..\..\..\Resources\Textures\Tilemap_Modified.png"

namespace MooseEngine.Core
{
    public static class Renderer
    {
        private static Texture2D _spriteSheet;
        private static int _spriteSize;
        private static int _offSet;
        private static int _padding;
        public static Camera2D camera;

        // TODO: Remove hard-coded values
        public static void Initialize(string spriteSheetPath, int offSet = 1, int padding = 1, int spriteSize = 9)
        {
            _offSet = offSet;
            _padding = padding;
            _spriteSize = spriteSize;
            _spriteSheet = Raylib.LoadTexture(spriteSheetPath);

            //camera.rotation = 0.0f;
            //camera.zoom = 0.1f;
            //camera.offset = new Vector2(Application.Instance.Window.Width / 2.0f - (_spriteSize * camera.zoom), Application.Instance.Window.Height / 2.0f - (_spriteSize * camera.zoom));

            Raylib.SetTargetFPS(60);

            // TODO: add null check
            Throw.IfNull(_spriteSheet, $"Failed to load spritesheet, path: {spriteSheetPath}");
        }

        public static void Shutdown()
        {
        }

        public static void Begin(Camera camera)
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);

            Raylib.BeginMode2D(camera.RaylibCamera);
        }

        public static void End()
        {
            Raylib.EndMode2D();

            Raylib.EndDrawing();
        }

        public static void RenderTexture(Texture2D texture, int x, int y)
        {
            Raylib.DrawTextureEx(texture, Vector2.Zero, 0.0f, 1.0f, Color.WHITE);
        }

        public static void RenderEntity(Entity entity)
        {
            Coords2D spritePosition = entity.SpriteCoords;
           
            Rectangle pixelSourcePosition = new Rectangle(
                _offSet + ((_spriteSize + _padding) * spritePosition.X), 
                _offSet + ((_spriteSize + _padding) * spritePosition.Y), 
                _spriteSize, 
                _spriteSize);
            
            Rectangle pixelDestinationPosition = new Rectangle(
                entity.Position.X + (entity.Position.X/_spriteSize * 0), // Adds a one "pixel" distance between sprites
                entity.Position.Y + (entity.Position.Y/_spriteSize * 0), 
                entity.Scale.X, 
                entity.Scale.Y);

            Raylib.DrawTexturePro(_spriteSheet, pixelSourcePosition, pixelDestinationPosition, Vector2.Zero, 0.0f, entity.ColorTint);
        }
    }
}
