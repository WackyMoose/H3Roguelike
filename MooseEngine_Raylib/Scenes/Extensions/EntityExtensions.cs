using Raylib_cs;

namespace MooseEngine.Scenes.Extensions;

public static class EntityExtensions
{
    public static Rectangle ToTextureDestination(this Entity entity)
    {
        return new Rectangle(
            entity.Position.X,
            entity.Position.Y,
            entity.Scale.X,
            entity.Scale.Y);
    }
}
