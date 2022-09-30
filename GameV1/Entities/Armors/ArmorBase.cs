using GameV1.Entities.Items;
using GameV1.Interfaces.Armors;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Armors
{
    public abstract class ArmorBase : ItemBase, IArmor
    {
        public int DamageReduction { get; set; }

        public ArmorBase() : base()
        {

        }

        protected ArmorBase(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}
