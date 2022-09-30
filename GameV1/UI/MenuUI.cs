using GameV1.UI.Components;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

enum MenuState
{
    Login,
    Register,
    MainMenu,
    Create,
    Load
}

internal class MenuUI : IUIElement
{
    private MenuState _menuState = MenuState.Login;

    private LoginFormComponent _loginForm;
    private RegisterFormComponent _registerForm;

    private CreateWorldPanel _createWorldPanel;
    private SaveGamesPanel _loadPanel;

    private const int PANEL_WIDTH = 300;
    private const int PANEL_HEIGHT = 250;
    private const int PADDING = 10;

    private PanelOptions _panelOptions;
    private LabelOptions _greetingsLabel;

    private ButtonOptions _playButton;
    private ButtonOptions _loadButton;
    private ButtonOptions _quitButton;

    private string _username;

    public event Action<string, int> OnCreateNewGameButtonClicked;

    public MenuUI()
    {
        _loginForm = new LoginFormComponent();
        _loginForm.OnLoginButtonClicked += LoginForm_OnLoginButtonClicked;
        _loginForm.OnRegisterButtonClicked += LoginForm_OnRegisterButtonClicked;

        _registerForm = new RegisterFormComponent();
        _registerForm.OnLoginButtonClicked += RegisterForm_OnLoginButtonClicked;
        _registerForm.OnRegisterButtonClicked += RegisterForm_OnRegisterButtonClicked;

        _createWorldPanel = new CreateWorldPanel();
        _createWorldPanel.OnCreateButtonClicked += OnCreateButtonClicked;
        _createWorldPanel.OnBackButtonClicked += OnBackButtonClicked;

        _loadPanel = new SaveGamesPanel();
        _loadPanel.OnBackButtonPressed += OnBackButtonClicked;

        var window = Application.Instance.Window;

        var panelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var panelPosition = new UIScreenCoords((window.Width - panelSize.X) / 2, (window.Height - panelSize.Y) / 2);
        _panelOptions = new PanelOptions(panelPosition, panelSize, "Main Menu", false);

        var panelHeaderRect = _panelOptions.GetBoundsWithoutStatusBar();
        var panelHeaderPosition = new UIScreenCoords((int)panelHeaderRect.x, (int)panelHeaderRect.y);
        var panelHeaderSize = new UIScreenCoords((int)panelHeaderRect.width, (int)panelHeaderRect.height);

        _greetingsLabel = new LabelOptions(new UIScreenCoords(panelHeaderPosition.X + 10, panelHeaderPosition.Y + 10), "Hello, xxx", 24);

        var buttonSize = new UIScreenCoords(275, 50);
        var buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, panelPosition.Y + 65);
        _playButton = new ButtonOptions(buttonPosition, buttonSize, "Play");

        buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, (panelPosition.Y + 115) + PADDING);
        _loadButton = new ButtonOptions(buttonPosition, buttonSize, "Load");

        buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, (panelPosition.Y + 165) + (2 * PADDING));
        _quitButton = new ButtonOptions(buttonPosition, buttonSize, "Quit");
    }

    private void OnCreateButtonClicked(string name, int seed)
    {
        OnCreateNewGameButtonClicked?.Invoke(name, seed);
    }

    private void OnBackButtonClicked()
    {
        _menuState = MenuState.MainMenu;
    }

    private void RegisterForm_OnRegisterButtonClicked()
    {
        _menuState = MenuState.MainMenu;
    }

    private void RegisterForm_OnLoginButtonClicked()
    {
        _menuState = MenuState.Login;
        _loginForm.Reset();
    }

    private void LoginForm_OnRegisterButtonClicked()
    {
        _menuState = MenuState.Register;
        _registerForm.Reset();
    }

    private void LoginForm_OnLoginButtonClicked(string username)
    {
        _greetingsLabel.Text = $"Hello, {username}";
        _menuState = MenuState.MainMenu;
    }

    public void SetMenuState(MenuState menuState)
    {
        _menuState = menuState;
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        switch (_menuState)
        {
            case MenuState.Login: _loginForm.OnGUI(UIRenderer); break;
            case MenuState.Register: _registerForm.OnGUI(UIRenderer); break;
            case MenuState.MainMenu: DrawMainMenu(UIRenderer); break;
            case MenuState.Create: _createWorldPanel.OnGUI(UIRenderer); break;
            case MenuState.Load: _loadPanel.OnGUI(UIRenderer); break;
        }
    }

    private void DrawMainMenu(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);
        UIRenderer.DrawLabel(_greetingsLabel);

        if (UIRenderer.DrawButton(_playButton))
        {
            _menuState = MenuState.Create;
        }

        if (UIRenderer.DrawButton(_loadButton))
        {
            _menuState = MenuState.Load;
        }

        if (UIRenderer.DrawButton(_quitButton))
        {
            Application app = Application.Instance;
            app.Close();
        }
    }
}
