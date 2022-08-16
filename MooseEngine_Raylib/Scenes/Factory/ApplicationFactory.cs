using MooseEngine.Core;

namespace MooseEngine.Scenes.Factory;

public interface IApplicationFactory : IFactory
{
    IApplication CreateApplication(ApplicationSpecification applicationSpecification);
}

public class ApplicationFactory : FactoryBase, IApplicationFactory
{
    private IGame _game;
    private ISceneFactory _sceneFactory;

    public ApplicationFactory(IGame game, ISceneFactory sceneFactory)
    {
        _game = game;
        _sceneFactory = sceneFactory;
    }

    public IApplication CreateApplication(ApplicationSpecification applicationSpecification)
    {
        return new Application(applicationSpecification, _game, _sceneFactory);
    }
}
