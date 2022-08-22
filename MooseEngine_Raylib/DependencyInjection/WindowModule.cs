using Autofac;
using MooseEngine.Core;
using MooseEngine.Core.Factories;

namespace MooseEngine.DependencyInjection;

internal class WindowModule : Module
{
    private readonly WindowOptions _windowSpecification;

    public WindowModule(WindowOptions windowSpecification)
    {
        _windowSpecification = windowSpecification;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(cc =>
        {
            var windowFactory = cc.Resolve<IWindowFactory>();
            return windowFactory.CreateWindow(_windowSpecification);
        })
            .As<IWindow>()
            .SingleInstance()
            .InstancePerLifetimeScope();
    }
}
