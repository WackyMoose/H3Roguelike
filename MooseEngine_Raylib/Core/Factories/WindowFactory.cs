namespace MooseEngine.Core.Factories;

public interface IWindowFactory : IFactory
{
    IWindow CreateWindow(WindowOptions windowSpecification);
}

internal class WindowFactory : IWindowFactory
{
    public IWindow CreateWindow(WindowOptions windowSpecification)
    {
        return new Window(windowSpecification);
    }
}
