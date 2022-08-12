namespace MooseEngine.Scenes.Factory;

public interface IEntityFactory
{
    void SetSceneContext(Scene scene);
}

public abstract class EntityFactoryBase : IEntityFactory
{
    private Scene? _scene;

    public TEntity CreateEntity<TEntity>()
        where TEntity : Entity, new()
    {
        var entity = Activator.CreateInstance<TEntity>();
        if(entity == default)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to create entity!");
            Console.ResetColor();
            return default!;
        }

        _scene?.Add(entity);
        return entity;
    }

    public void SetSceneContext(Scene scene)
    {
        _scene = scene;
    }
}
