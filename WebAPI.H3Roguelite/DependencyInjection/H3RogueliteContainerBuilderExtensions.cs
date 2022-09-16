using Autofac;
using WebAPI.H3Roguelite.Data.Providers;

namespace WebAPI.H3Roguelite.DependencyInjection;

public static class H3RogueliteContainerBuilderExtensions
{
    public static ContainerBuilder RegisterH3Roguelite(this ContainerBuilder builder, H3RogueliteOptions h3RogueliteOptions)
    {
        builder.RegisterMySqlClientProvider<H3RogueliteClientProvider>(h3RogueliteOptions.MySqlClientProviderSettings);

        builder.RegisterModule<ServiceModule>();

        return builder;
    }
}
