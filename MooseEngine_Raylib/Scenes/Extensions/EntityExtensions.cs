using MooseEngine.Interfaces;
using Raylib_cs;

namespace MooseEngine.Scenes.Extensions;

public static class EntityExtensions
{
    public static Rectangle ToTextureDestination(this IEntity entity, float scale)
    {
        return new Rectangle(
            entity.Position.X,
            entity.Position.Y,
            entity.Scale.X * scale,
            entity.Scale.Y * scale);
    }
}
