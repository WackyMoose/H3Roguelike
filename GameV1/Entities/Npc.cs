using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Npc : Creature
    {
        public Npc(string name, int movementPoints, int health, Coords2D spriteCoords) : base(name, movementPoints, health, spriteCoords)
        {
        }

        public Npc(string name, int movementPoints, int health, Coords2D spriteCoords, Color colorTint) : base(name, movementPoints, health, spriteCoords, colorTint)
        {
        }
    }
}
