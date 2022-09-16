using System.Data;
using System.Reflection;
using WebAPI.Data.Providers;
using WebAPI.Data.Providers.Databases.MySqlClient;
using WebAPI.Data.Repositories;

namespace Autofac;

public static class MySqlClientProviderContainerBuilderExtensions
{
    public static ContainerBuilder RegisterMySqlClientProvider<TProvider>(this ContainerBuilder builder, MySqlClientProviderSettings mySqlClientProviderSettings)
        where TProvider : class, IMySqlClientProvider
    {
        builder.RegisterProvider<TProvider>();

        builder.RegisterMySqlClientProviderFactory<TProvider>(mySqlClientProviderSettings);

        builder.RegisterRepositories<TProvider>();

        return builder;
    }

    public static void RegisterProvider<TProvider>(this ContainerBuilder builder)
        where TProvider : class, IMySqlClientProvider
    {
        builder.Register((cc, p) =>
        {
            var providerFactory = cc.Resolve<IProviderFactory>();

            if (p.Any())
            {
                providerFactory.CreateProvider<TProvider>(p.TypedAs<IsolationLevel>());
            }
            return providerFactory.CreateProvider<TProvider>();
        })
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    public static void RegisterMySqlClientProviderFactory<TProvider>(this ContainerBuilder builder, MySqlClientProviderSettings mySqlClientProviderSettings)
        where TProvider : class, IMySqlClientProvider
    {
        builder.RegisterType<MySqlClientProviderFactory<TProvider>>()
            .UsingConstructor(typeof(MySqlClientProviderSettings))
            .WithParameter(TypedParameter.From(mySqlClientProviderSettings))
            .As<IProviderFactory<TProvider>>()
            .InstancePerLifetimeScope();
    }

    public static void RegisterRepositories<TProvider>(this ContainerBuilder builder)
        where TProvider : class, IMySqlClientProvider
    {
        builder.RegisterRepositories(typeof(TProvider).Assembly);
    }

    public static void RegisterRepositories(this ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces();
    }
}
