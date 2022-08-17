using MooseEngine.Core;

namespace MooseEngine.Scenes.Factory;

public interface IApplicationFactory : IFactory
{
    IApplication CreateApplication(ApplicationSpecification applicationSpecification);
}

public class ApplicationFactory : FactoryBase, IApplicationFactory
{
    private IGame _game;

    public ApplicationFactory(IGame game)
    {
        _game = game;
    }

    public IApplication CreateApplication(ApplicationSpecification applicationSpecification)
    {
        return new Application(applicationSpecification, _game);
    }
}
