using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace GameV1;

public static class SceneExtensions
{
    public static IEntityLayer<TEntity> AddLayer<TEntity>(this IScene scene, EntityLayer layer)
        where TEntity : class, IEntity
    {
        return scene.AddLayer<TEntity>((int)layer);
    }
}
