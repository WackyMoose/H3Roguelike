using MooseEngine.Graphics;

namespace GameV1.Interfaces
{
    public interface ILightSource : IItem
    {
        public int Range { get; set; }
        public Color TintModifier { get; set; }
    }
}
