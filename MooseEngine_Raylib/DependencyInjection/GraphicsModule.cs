using Autofac;
using MooseEngine.Graphics;

namespace MooseEngine.DependencyInjection;

internal class GraphicsModule<TRenderer, TRendererOptions> : Module
    where TRenderer : class, IRenderer
    where TRendererOptions : class, IRendererOptions
{
    public GraphicsModule(TRendererOptions rendererOptions)
    {
        RendererOptions = rendererOptions;
    }

    public TRendererOptions RendererOptions { get; }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TRenderer>()
        .UsingConstructor(typeof(TRendererOptions))
        .WithParameter(TypedParameter.From<TRendererOptions>(RendererOptions))
        .As<IRenderer>()
        .SingleInstance()
        .InstancePerLifetimeScope();
    }
}
