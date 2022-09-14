using GameV1.Interfaces.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities.Containers
{

    // When an item is dropped, a temporary container is automatically created and the item is added to the container.
    // This allows for placing multiple items in the same tile.
    // If containing multiple items, the container is displayerd with a "pile of loot" texture, and when
    // it only contains one item, it is displayer with the texture of that item.
    // When the last item is removed from the temporary container, the container is destroyed.
    internal class TemporaryContainer : Container, ITemporaryContainer
    {
        public TemporaryContainer(int maxSlots) : base(maxSlots)
        {
        }
    }
}
