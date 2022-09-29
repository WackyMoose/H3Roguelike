using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities;

public class Player : Creature
{
    public Player(string name, int health, Coords2D spriteCoords) : base(name, health, spriteCoords)
    {
    }

    public Player(string name, int health, Coords2D spriteCoords, Color colorTint) : base(name, health, spriteCoords, colorTint)
    {
    }
}
