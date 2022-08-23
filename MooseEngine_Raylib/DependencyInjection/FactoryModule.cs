using Autofac;
using MooseEngine.Core.Factories;

namespace MooseEngine.DependencyInjection;

internal class FactoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IFactory>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
