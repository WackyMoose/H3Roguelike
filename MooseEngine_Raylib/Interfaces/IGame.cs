using MooseEngine.Core;
using MooseEngine.Graphics;

namespace MooseEngine.Interfaces;

public interface IGame
{
    public void Initialize();
    public void Uninitialize();
    public void Update(float deltaTime);
    public void UIRender(IUIRenderer UIRenderer);
}
