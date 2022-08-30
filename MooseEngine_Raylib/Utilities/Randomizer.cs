
namespace MooseEngine.Utilities;

public static class Randomizer
{
    public static Random _generator = new Random(Guid.NewGuid().GetHashCode());

    public static int RandomInt(int minVal, int maxVal)
    {
        return _generator.Next(minVal, maxVal);
    }

    public static int RandomPercent()
    {
        return _generator.Next(0, 100 + 1);
    }

    public static bool CoinFlip()
    {
        return RandomPercent() < 50;
    }
}