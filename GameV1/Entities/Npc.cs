using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
