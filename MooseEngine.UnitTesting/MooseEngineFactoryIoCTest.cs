using Autofac;
using MooseEngine.Core.Factories;
using MooseEngine.DependencyInjection;
using MooseEngine.Scenes.Factories;

namespace MooseEngine.UnitTesting;

[TestClass]
public class MooseEngineFactoryIoCTest
{
    [TestMethod]
    public void Is_Application_Factory_Registered()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<FactoryModule>();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        scope.Dispose();

        var isApplicationFactoryRegistered = container.IsRegistered<IApplicationFactory>();
        Assert.IsTrue(isApplicationFactoryRegistered);
    }

    [TestMethod]
    public void Is_Window_Factory_Registered()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<FactoryModule>();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        scope.Dispose();

        var isWindowFactoryRegistered = container.IsRegistered<IWindowFactory>();
        Assert.IsTrue(isWindowFactoryRegistered);
    }

    [TestMethod]
    public void Is_Scene_Factory_Registered()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<FactoryModule>();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        scope.Dispose();

        var isSceneFactoryRegistered = container.IsRegistered<ISceneFactory>();
        Assert.IsTrue(isSceneFactoryRegistered);
    }
}
