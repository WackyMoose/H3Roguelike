using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;

namespace MooseEngine.UnitTesting.TestData;

internal class UnitTestGame : IGame
{
    public void Initialize()
    {
    }

    public void Uninitialize()
    {
    }

    public void Update(float deltaTime)
    {
    }

    public void UIRender(IWindowData windowData)
    {
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        throw new System.NotImplementedException();
    }
}
