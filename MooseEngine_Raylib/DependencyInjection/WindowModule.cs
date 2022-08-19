using Autofac;
using MooseEngine.Core;
using MooseEngine.Core.Factories;

namespace MooseEngine.DependencyInjection;

internal class WindowModule : Module
{
    private readonly WindowSpecification _windowSpecification;

    public WindowModule(WindowSpecification windowSpecification)
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
