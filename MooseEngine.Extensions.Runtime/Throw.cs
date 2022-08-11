namespace MooseEngine.Extensions.Runtime;

public static class Throw
{
    public static void IfNull(object? obj, string message)
    {
        if (obj == null)
        {
            throw new Exception(message);
        }
    }
}
