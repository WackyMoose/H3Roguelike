namespace MooseEngine.Interfaces;

public interface IGame
{
    public void Initialize();
    public void Uninitialize();
    public void Update(float deltaTime);
}
