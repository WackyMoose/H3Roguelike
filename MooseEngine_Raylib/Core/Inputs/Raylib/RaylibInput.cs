using Raylib_cs;

namespace MooseEngine.Core.Inputs;

internal class RaylibInput : IInputAPI
{
    public bool IsKeyPressed(Keycode keycode)
    {
        return Raylib.IsKeyPressed((KeyboardKey)keycode);
    }
}
