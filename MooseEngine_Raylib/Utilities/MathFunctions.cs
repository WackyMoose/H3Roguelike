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
        return (v - minValue) / (maxValue - minValue);
    }

    public static float InverseDistanceSquaredBetween(Coords2D positionA, Coords2D positionB)
    {
        Vector2 distance = new Vector2(positionB.X - positionA.X, positionB.Y - positionA.Y);

        return (1.0f / (distance.X * distance.X + distance.Y * distance.Y));
    }

    public static float DistanceSquaredBetween(Coords2D positionA, Coords2D positionB)
    {
        Coords2D distance = new Coords2D(positionB.X - positionA.X, positionB.Y - positionA.Y);

        return distance.X * distance.X + distance.Y * distance.Y;
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

    public static bool AABBCollisionDetection(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
    {
        var aLft = positionA.X - sizeA.X;
        var bRgt = positionB.X + sizeB.X;

        if (aLft > bRgt) { return false; }

        var aRgt = positionA.X + sizeA.X;
        var bLft = positionB.X - sizeB.X;

        if (aRgt < bLft) { return false; }

        var aBtm = positionA.Y + sizeA.Y;
        var bTop = positionB.Y - sizeB.Y;

        if (aBtm < bTop) { return false; }

        var aTop = positionA.Y - sizeA.Y;
        var bBtm = positionB.Y + sizeB.Y;

        if (aTop > bBtm) { return false; }

        return true;
    }
}