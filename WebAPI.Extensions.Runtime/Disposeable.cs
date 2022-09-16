namespace WebAPI;

public class Disposeable : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            DisposeManagedState();
        }

        FreeUnmanagedResources();

        ClearLargeFields();

        _disposed = true;
    }

    protected virtual void DisposeManagedState()
    {
    }

    protected virtual void FreeUnmanagedResources()
    {
    }

    protected virtual void ClearLargeFields()
    {
    }
}