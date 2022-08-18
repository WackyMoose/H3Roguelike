using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooseEngine.Core;
using MooseEngine.DependencyInjection;

namespace MooseEngine.UnitTesting;

[TestClass]
public class MooseEngineIoCTest
{
    [TestMethod]
    public void Is_Application_Registered()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<CoreModule>();

        var container = containerBuilder.Build();

        var scope = container.BeginLifetimeScope();

        var isApplicationRegistered = container.IsRegistered<IApplication>();
        Assert.IsTrue(isApplicationRegistered);

        scope.Dispose();
    }
}
