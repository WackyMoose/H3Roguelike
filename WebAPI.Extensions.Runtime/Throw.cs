namespace WebAPI;

public static class Throw
{
    public static void IfSingletonExists(object? instance, string message)
    {
        if (instance != default)
        {
            throw new Exception(message);
        }
    }

    public static void IfStringIsNullOrWhiteSpace(string? str, string message)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new Exception(message);
        }
    }

    public static void IfNull(object? obj, string message)
    {
        if (obj == null)
        {
            throw new Exception(message);
        }
    }
}
