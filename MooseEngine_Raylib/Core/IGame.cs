namespace MooseEngine.Core;

public interface IGame
{
    public void Initialize();
    public void Uninitialize();
    public void Update(float deltaTime);
}
