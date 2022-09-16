using Autofac;
using WebAPI.Services;

namespace WebAPI.H3Roguelite.DependencyInjection;

internal class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
