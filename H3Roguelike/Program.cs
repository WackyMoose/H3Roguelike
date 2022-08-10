using Raylib_cs;
using System.Numerics;

const int WINDOW_WIDTH = 1600;
const int WINDOW_HEIGHT = 900;

Console.WriteLine("Hello, World!");

Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Hello World");

Raylib.SetTargetFPS(15);

var texture = Raylib.LoadTexture("Resources/Textures/colored_tilemap_packed.png");

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    Raylib.DrawText("Hello world!", 12, 12, 20, Color.BLACK);
    Raylib.DrawFPS(12, 36);

    Raylib.DrawCircle(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 20, Color.RED);

    var source = new Rectangle(0, 0, 8, 8);
    var dest = new Rectangle(128, 128, 64, 64);
    Raylib.DrawTextureTiled(texture, source, dest, Vector2.Zero, 0.0f, 8.0f, Color.WHITE);

    source = new Rectangle(8, 0, 8, 8);
    dest = new Rectangle(128 + 64, 128, 64, 64);
    Raylib.DrawTextureTiled(texture, source, dest, Vector2.Zero, 0.0f, 8.0f, Color.WHITE);

    source = new Rectangle(16, 0, 8, 8);
    dest = new Rectangle(128 + 128, 128, 64, 64);
    Raylib.DrawTextureTiled(texture, source, dest, Vector2.Zero, 0.0f, 8.0f, Color.WHITE);

    source = new Rectangle(24, 0, 8, 8);
    dest = new Rectangle(128 + 128 + 64, 128, 64, 64);
    Raylib.DrawTextureTiled(texture, source, dest, Vector2.Zero, 0.0f, 8.0f, Color.WHITE);

    Raylib.DrawTextureEx(texture, new Vector2(), 0.0f, 1.0f, Color.WHITE);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();