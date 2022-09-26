using GameV1.UI;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1;

internal class MenuTest : IGame
{
    enum SceneState
    {
        Menu,
        Game
    }
    private SceneState _sceneState = SceneState.Menu;
    private IScene _scene;
    private IScene _menuScene;

    // Main Menu
    private Menu _mainMenu;

    public void Initialize()
    {
        var window = Application.Instance.Window;

        var sceneFactory = Application.Instance.SceneFactory;

        _scene = sceneFactory.CreateScene();
        _scene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _menuScene = sceneFactory.CreateScene();
        _menuScene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _mainMenu = new Menu();
    }

    public void Uninitialize()
    {
    }

    public void Update(float deltaTime)
    {
        if(_sceneState == SceneState.Game)
        {
            _scene.UpdateRuntime(deltaTime);
        }
        else
        {
            _menuScene.UpdateRuntime(deltaTime);
        }
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        if (_sceneState == SceneState.Menu)
        {
            _mainMenu.OnGUI(UIRenderer);
        }
        else
        {
        }
    }
}
