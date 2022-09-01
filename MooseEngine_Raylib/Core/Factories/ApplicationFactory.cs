using MooseEngine.Core.Inputs;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factories;

namespace MooseEngine.Core.Factories;

public interface IApplicationFactory : IFactory
{
    IApplication CreateApplication(ApplicationOptions applicationSpecification, IGame game, IWindow window, IInputAPI inputAPI, IRenderer renderer, IUIRenderer uiRenderer, ISceneFactory sceneFactory);
}

internal class ApplicationFactory : IApplicationFactory
{
    public IApplication CreateApplication(ApplicationOptions applicationSpecification, IGame game, IWindow window, IInputAPI inputAPI, IRenderer renderer, IUIRenderer uiRenderer, ISceneFactory sceneFactory)
    {
        return new Application(applicationSpecification, game, window, inputAPI, renderer, uiRenderer, sceneFactory);
    }
}
