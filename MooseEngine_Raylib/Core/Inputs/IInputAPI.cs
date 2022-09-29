namespace MooseEngine.Core.Inputs;

public interface IInputAPI
{
    bool IsKeyPressed(Keycode keycode);
    bool IsKeyDown(Keycode keycode);
    bool IsKeyReleased(Keycode keycode);
    bool IsKeyUp(Keycode keycode);
}
