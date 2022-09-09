using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ITile : IPathMappable
    {
        bool IsWalkable { get; set; }
    }
}
