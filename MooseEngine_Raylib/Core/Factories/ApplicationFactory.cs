using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factory;

namespace MooseEngine.Core.Factories;

public interface IApplicationFactory : IFactory
{
    IApplication CreateApplication(ApplicationSpecification applicationSpecification, IGame game, IWindow window, ISceneFactory sceneFactory);
}

internal class ApplicationFactory : IApplicationFactory
{
    public IApplication CreateApplication(ApplicationSpecification applicationSpecification, IGame game, IWindow window, ISceneFactory sceneFactory)
    {
        return new Application(applicationSpecification, game, window, sceneFactory);
    }
}
