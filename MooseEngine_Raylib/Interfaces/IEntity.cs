using MooseEngine.Graphics;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Interfaces
{
    public interface IEntity
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        Coords2D SpriteCoords { get; set; }
        Color ColorTint { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }

        void Initialize();
        void Update(float deltaTime);
    }
}
