using System.Numerics;

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

    public static int DistanceBetween(Vector2 positionA, Vector2 positionB)
    {
        Vector2 distance = new Vector2(positionB.X - positionA.X, positionB.Y - positionA.Y);

        return (int)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
    }

    public static int DistanceBetween(Coords2D positionA, Coords2D positionB)
    {
        Coords2D distance = new Coords2D(positionB.X - positionA.X, positionB.Y - positionA.Y);

        return (int)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
    }
}