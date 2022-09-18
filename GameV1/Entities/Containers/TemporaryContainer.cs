using GameV1.Interfaces.Containers;
using MooseEngine.Graphics;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities.Containers
{

    // When an item is dropped, a temporary container is automatically created, and the item is added to the container.
    // This allows for placing multiple items in the same tile.
    // If any container already exists at the position, the item is added to the existing container instead.
    // If containing multiple items, the temporary container is displayerd with a "pile of loot" texture, and when
    // it only contains one item, it is displayer with the texture of that item.
    // When the last item is removed from the temporary container, it is destroyed.
    internal class TemporaryContainer : Container, ITemporaryContainer
    {
        public TemporaryContainer(int maxSlots) : base(maxSlots)
        {
        }

        public TemporaryContainer(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(maxSlots, durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
