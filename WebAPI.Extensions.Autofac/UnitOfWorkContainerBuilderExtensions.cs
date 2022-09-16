using Autofac.Core;
using System.Data;
using WebAPI.Data;

namespace Autofac;

public static class UnitOfWorkContainerBuilderExtensions
{
    public static ContainerBuilder RegisterUnitOfWorkFactory(this ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWorkFactory>()
            .As<IUnitOfWorkFactory>()
            .InstancePerLifetimeScope();

        builder.Register(UnitOfWorkFactory)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    private static IUnitOfWork UnitOfWorkFactory(IComponentContext cc, IEnumerable<Parameter> parameters) 
    {
        var factory = cc.Resolve<IUnitOfWorkFactory>();

        if(!parameters.Any())
        {
            return factory.CreateUnitOfWork();
        }
        else
        {
            var isolationLevel = parameters.TypedAs<IsolationLevel>();
            return factory.CreateUnitOfWork(isolationLevel);
        }
    }
}
