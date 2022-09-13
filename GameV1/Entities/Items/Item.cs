using GameV1.Interfaces;
using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Entities.Items
{
    public abstract class Item : Entity, IItem
    {
        public int Durability { get; set; }
        public int MaxValue { get; set; }
        public IEnumerable<IMaterial>? Materials { get; set; }
        public bool IsBroken { get { return Durability <= 0; } }

        public Item(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(name, spriteCoords, colorTint)
        {
            Durability = durability;
            MaxValue = maxValue;
            Materials = new List<IMaterial>();
        }

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
