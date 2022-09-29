using Raylib_cs;

namespace MooseEngine.Core.Inputs;

internal class RaylibInput : IInputAPI
{
    public bool IsKeyPressed(Keycode keycode)
    {
        return Raylib.IsKeyPressed((KeyboardKey)keycode);
    }

    public bool IsKeyDown(Keycode keycode)
    {
        return Raylib.IsKeyDown((KeyboardKey)keycode);
    }

    public bool IsKeyReleased(Keycode keycode)
    {
        return Raylib.IsKeyReleased((KeyboardKey)keycode);
    }

    public bool IsKeyUp(Keycode keycode)
    {
        return Raylib.IsKeyUp((KeyboardKey)keycode);
    }
}