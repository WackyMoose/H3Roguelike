using Autofac;
using MooseEngine.Core;

namespace MooseEngine.DependencyInjection;

internal class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Application>()
            .As<IApplication>()
            .InstancePerLifetimeScope();
    }
}
