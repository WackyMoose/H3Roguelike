
using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;

namespace GameV1.Interfaces.Items
{
    public interface IConsumable : IItem
    {
        ICreatureStats StatModifier { get; set; }
    }
}
