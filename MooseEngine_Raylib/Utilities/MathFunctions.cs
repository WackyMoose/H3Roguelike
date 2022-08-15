namespace MooseEngine.Utilities;

public static class MathFunctions
{
    public static float Lerp(float minValue, float maxValue, float n)
    {
        return minValue + n * (maxValue - minValue);
    }
}