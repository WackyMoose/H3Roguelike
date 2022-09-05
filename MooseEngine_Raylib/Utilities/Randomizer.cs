
namespace MooseEngine.Utilities;

public static class Randomizer
{
    public static Random _generator = new Random(Guid.NewGuid().GetHashCode());

    public static int RandomInt(int minVal, int maxVal)
    {
        return _generator.Next(minVal, maxVal);
    }

    public static float RandomFloat(float minVal, float maxVal)
    {
        return minVal + _generator.NextSingle() * (maxVal - minVal);
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