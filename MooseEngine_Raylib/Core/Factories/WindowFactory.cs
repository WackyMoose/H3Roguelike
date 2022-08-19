namespace MooseEngine.Core.Factories;

public interface IWindowFactory : IFactory
{
    IWindow CreateWindow(WindowSpecification windowSpecification);
}

internal class WindowFactory : IWindowFactory
{
    public IWindow CreateWindow(WindowSpecification windowSpecification)
    {
        return new Window(windowSpecification);
    }
}
