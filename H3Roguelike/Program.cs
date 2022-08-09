using Raylib_cs;

Console.WriteLine("Hello, World!");

Raylib.InitWindow(800, 480, "Hello World");

Raylib.SetTargetFPS(1);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    Raylib.DrawText("Hello world!", 12, 12, 20, Color.BLACK);

    var frametime = Raylib.GetFrameTime();
    Raylib.DrawText($"Frame time: {frametime / 1000.0f} S", 12, 36, 20, Color.BLACK);
    Raylib.DrawText($"FPS: {1.0f / frametime} FPS", 12, 60, 20, Color.BLACK);

    Raylib.DrawCircle(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2, 20, Color.RED);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();