using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factory;

namespace MooseEngine.Core.Factories;

public interface IApplicationFactory : IFactory
{
    IApplication CreateApplication(ApplicationOptions applicationSpecification, IGame game, IWindow window, IRenderer renderer, ISceneFactory sceneFactory);
}

internal class ApplicationFactory : IApplicationFactory
{
    public IApplication CreateApplication(ApplicationOptions applicationSpecification, IGame game, IWindow window, IRenderer renderer, ISceneFactory sceneFactory)
    {
        return new Application(applicationSpecification, game, window, renderer, sceneFactory);
    }
}
