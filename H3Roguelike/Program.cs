using Raylib_cs;

Console.WriteLine("Hello, World!");

Raylib.InitWindow(800, 480, "Hello World");

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    Raylib.DrawText("Hello world!", 12, 12, 20, Color.BLACK);

    var frametime = Raylib.GetFrameTime();
    Raylib.DrawText($"Frame time: {frametime / 1000.0f}ms", 12, 36, 20, Color.BLACK);
    Raylib.DrawText($"FPS: {1.0f / frametime} FPS", 12, 60, 20, Color.BLACK);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();