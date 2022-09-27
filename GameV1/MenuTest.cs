using GameV1.UI;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1;

internal class MenuTest : IGame
{
    enum SceneState
    {
        Login,
        Menu,
        Load,
        Game
    }
    private SceneState _sceneState = SceneState.Login;
    private IScene _scene;
    private IScene _menuScene;

    // Menu
    private LoginPanel _loginPanel;
    private Menu _mainMenu;
    private SaveGamesPanel _loadPanel;

    private ButtonOptions _backToMenuButton;

    public void Initialize()
    {
        var window = Application.Instance.Window;

        var sceneFactory = Application.Instance.SceneFactory;

        _scene = sceneFactory.CreateScene();
        _scene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _menuScene = sceneFactory.CreateScene();
        _menuScene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        var buttonPosition = new UIScreenCoords(10, 10);
        var buttonSize = new UIScreenCoords(200, 30);
        _backToMenuButton = new ButtonOptions(buttonPosition, buttonSize, "Back to Menu");

        _loginPanel = new LoginPanel();
        _loginPanel.OnLoginButtonPressed += OnLoginButtonPressed;

        _mainMenu = new Menu();
        _mainMenu.OnPlayButtonPressed += OnPlayButtonPressed;
        _mainMenu.OnLoadButtonPressed += OnLoadButtonPressed;

        _loadPanel = new SaveGamesPanel();
        _loadPanel.OnBackButtonPressed += OnBackButtonPressed;
    }

    private void OnBackButtonPressed()
    {
        _sceneState = SceneState.Menu;
    }

    private void OnLoginButtonPressed()
    {
        _sceneState = SceneState.Menu;
    }

    private void OnLoadButtonPressed()
    {
        _sceneState = SceneState.Load;
    }

    private void OnPlayButtonPressed()
    {
        _sceneState = SceneState.Game;
    }

    public void Uninitialize()
    {
    }

    public void Update(float deltaTime)
    {
        if (_sceneState == SceneState.Game)
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
        switch (_sceneState)
        {
            case SceneState.Login: _loginPanel.OnGUI(UIRenderer); break;
            case SceneState.Load: _loadPanel.OnGUI(UIRenderer); break;
            case SceneState.Game: DrawBackToMenuButton(UIRenderer); break;
            case SceneState.Menu:
            default: _mainMenu.OnGUI(UIRenderer); break;
        }
    }

    private void DrawBackToMenuButton(IUIRenderer UIRenderer)
    {
        if (UIRenderer.DrawButton(_backToMenuButton))
        {
            _sceneState = SceneState.Menu;
        }
    }
}