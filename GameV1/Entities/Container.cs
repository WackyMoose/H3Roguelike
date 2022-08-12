using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{

    // TODO: Consider renaming to Storage unit

    public abstract class Container : Item, IContainer
    {
        public Container(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }

        public List<Item?>? Items { get; set; }
    }

    public class Chest : Container
    {
        public Chest(int durability, int maxValue) 
            : base(durability, maxValue, "Chest", new Coords2D(1,1), Color.WHITE)
        {
        }
    }
}
