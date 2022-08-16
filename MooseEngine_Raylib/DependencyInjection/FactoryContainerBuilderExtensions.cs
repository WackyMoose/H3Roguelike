using Autofac;
using MooseEngine.Scenes.Factory;
using System.Reflection;

namespace MooseEngine.DependencyInjection;

public static class FactoryContainerBuilderExtensions
{
    public static void RegisterFactory<TFactory>(this ContainerBuilder builder)
        where TFactory : FactoryBase, IFactory
    {
        builder
            .RegisterType<TFactory>()
            .As<IFactory>()
            .InstancePerLifetimeScope();
    }

    public static void RegisterFactories(this ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
            .AssignableTo<IFactory>()
            .AsImplementedInterfaces();
    }
}
