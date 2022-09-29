using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Interfaces.Items
{
    public interface ILightSource : IItem
    {
        int Range { get; set; }
        Color TintModifier { get; set; }
        IDictionary<Vector2, Color> Tints { get; set; }

        IDictionary<Vector2, Color> CreateTints(int range, Color tint);
        void Illuminate(IScene scene);
    }
}
