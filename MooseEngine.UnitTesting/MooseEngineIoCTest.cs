using Autofac;
using MooseEngine.Core;
using MooseEngine.DependencyInjection;
using MooseEngine.Interfaces;
using MooseEngine.UnitTesting.TestData;

namespace MooseEngine.UnitTesting;

[TestClass]
public class MooseEngineIoCTest
{
    [TestMethod]
    public void Is_Application_An_Instance_Of_Application()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<ApplicationModule>();
        containerBuilder.RegisterType<UnitTestGame>()
            .As<IGame>()
            .InstancePerLifetimeScope();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        scope.Dispose();

        var application = container.Resolve<IApplication>();
        Assert.IsInstanceOfType(application, typeof(Application));
    }

    [TestMethod]
    public void Is_Application_Registered()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<ApplicationModule>();
        containerBuilder.RegisterType<UnitTestGame>()
            .As<IGame>()
            .InstancePerLifetimeScope();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        scope.Dispose();

        var isApplicationRegistered = container.IsRegistered<IApplication>();
        Assert.IsTrue(isApplicationRegistered);
    }
}
