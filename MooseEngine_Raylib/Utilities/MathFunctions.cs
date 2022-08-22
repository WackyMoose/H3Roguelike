namespace MooseEngine.Utilities;

public static class MathFunctions
{
    public static float Lerp(float minValue, float maxValue, float n)
    {
        return minValue + n * (maxValue - minValue);
    }

    public static float InverseLerp(float minValue, float maxValue, float v) 
    {
        return ( v - minValue) / (maxValue - minValue);
    }
}