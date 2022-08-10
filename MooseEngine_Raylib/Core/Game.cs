namespace MooseEngine.Core;

public abstract class Game
{
    public Game()
    {
    }

    public abstract void Start();
    public abstract void Update(float deltaTime);
    public abstract void Render();
}
