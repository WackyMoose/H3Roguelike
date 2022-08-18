using MooseEngine.Utilities;

namespace MooseEngine.UnitTesting;

[TestClass]
public class Coord2DTest
{
    [TestMethod]
    public void Coord2D_2_Plus_2_Add_Operator()
    {
        var coord1 = new Coords2D(2, 2);
        var coord2 = new Coords2D(2, 2);
        var expected = new Coords2D(4, 4);

        var result = coord1 + coord2;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }

    [TestMethod]
    public void Coord2D_10_Plus_31_Add_Operator()
    {
        var coord1 = new Coords2D(10, 31);
        var coord2 = new Coords2D(31, 10);
        var expected = new Coords2D(41, 41);

        var result = coord1 + coord2;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }

    [TestMethod]
    public void Coord2D_2_Plus_2_Minus_Operator()
    {
        var coord1 = new Coords2D(2, 2);
        var coord2 = new Coords2D(2, 2);
        var expected = new Coords2D(0, 0);

        var result = coord1 - coord2;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }

    [TestMethod]
    public void Coord2D_10_Plus_31_Minus_Operator()
    {
        var coord1 = new Coords2D(10, 31);
        var coord2 = new Coords2D(31, 10);
        var expected = new Coords2D(-21, 21);

        var result = coord1 - coord2;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }

    [TestMethod]
    public void Coord2D_10_Multiply_Negative10_Multiply_Operator()
    {
        var coord1 = new Coords2D(10, -10);
        var expected = new Coords2D(-100, 100);

        var result = coord1 * -10;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }

    [TestMethod]
    public void Coord2D_10_Multiply_10_Multiply_Operator()
    {
        var coord1 = new Coords2D(10, 10);
        var expected = new Coords2D(100, 100);

        var result = coord1 * 10;

        var x = result.X == expected.X;
        var y = result.Y == expected.Y;
        Assert.IsTrue(x && y);
    }
}
