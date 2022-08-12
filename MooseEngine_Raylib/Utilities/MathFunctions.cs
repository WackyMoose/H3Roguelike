namespace MooseEngine.Utilities;

public static class MathFunctions
{

    public static double Lerp(double minValue, double maxValue, double n)
    {
        return minValue + n * (maxValue - minValue);
    }

}