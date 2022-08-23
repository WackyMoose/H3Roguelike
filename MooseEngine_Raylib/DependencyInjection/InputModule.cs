using Autofac;
using MooseEngine.Core.Inputs;

namespace MooseEngine.DependencyInjection;

internal class InputModule<TInput> : Module
    where TInput : class, IInputAPI
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TInput>()
            .As<IInputAPI>()
            .InstancePerLifetimeScope();
    }
}
