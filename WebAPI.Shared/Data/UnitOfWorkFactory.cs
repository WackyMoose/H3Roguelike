using System.Data;

namespace WebAPI.Data;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
}

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    public UnitOfWorkFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }

    public IUnitOfWork CreateUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
    {
        return new UnitOfWork(ServiceProvider, isolationLevel);
    }
}
