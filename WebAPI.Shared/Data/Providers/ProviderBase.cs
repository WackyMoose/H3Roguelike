using System.Data;

namespace WebAPI.Data.Providers;

public class ProviderBase : Disposeable, IProvider
{
    public ProviderBase(IsolationLevel isolationLevel)
    {
        IsolationLevel = isolationLevel;
    }

    public IsolationLevel IsolationLevel { get; }

    public virtual void Commit()
    {
    }

    public virtual void Rollback()
    {
    }
}
