using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class Player : Creature
    {
        public Player(string name, int movementPoints, int health, Coords2D spriteCoords) : base(name, movementPoints, health, spriteCoords)
        {
        }

        public Player(string name, int movementPoints, int health, Coords2D spriteCoords, Color colorTint) : base(name, movementPoints, health, spriteCoords, colorTint)
        {
        }
    }
}
