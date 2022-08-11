using Raylib_cs;

namespace MooseEngine.Core;

public class Window
{
    private WindowSpecification _specification;

    public int Width { get { return _specification.Width; } }
    public int Height { get { return _specification.Height; } }

    public Window(WindowSpecification specification)
    {
        _specification = specification;
    }

    public void Initialize()
    {
        Raylib.InitWindow(_specification.Width, _specification.Height, _specification.Title);
    }

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }

    public void Update()
    {
        Raylib.SwapScreenBuffer();
    }
}
