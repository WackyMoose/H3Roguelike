namespace MooseEngine.Scenes.Factory;

public abstract class FactoryBase
{
    private Scene? _scene;

    public FactoryBase(Scene scene)
    {
        _scene = scene;
    }

    protected TEntity CreateEntity<TEntity>()
        where TEntity : Entity
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
}
