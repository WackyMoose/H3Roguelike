using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface IItem : IEntity
    {
        int Durability { get; set; }
        int MaxValue { get; set; }
        List<Material> Materials { get; set; }
        bool IsBroken { get; }
    }
}
