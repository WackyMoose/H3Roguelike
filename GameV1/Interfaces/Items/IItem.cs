using MooseEngine.Interfaces;

namespace GameV1.Interfaces.Items
{
    public interface IItem : IEntity
    {
        int Durability { get; set; }
        int MaxValue { get; set; }
        IEnumerable<IMaterial>? Materials { get; set; }
        bool IsBroken { get; }
    }
}
