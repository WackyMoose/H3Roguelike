namespace WebAPI.Data.Providers;

public interface IProvider : IDisposable
{
    void Commit();
    void Rollback();
}
