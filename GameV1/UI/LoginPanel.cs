using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class LoginPanel
{
    private const int PANEL_WIDTH = 500;
    private const int PANEL_HEIGHT = 250;

    private string _username;
    private bool _usernameEditMode = false;

    private string _password;
    private bool _passwordEditMode = false;

    private PanelOptions _loginPanelOptions;
    private LabelOptions _loginTitleOptions;

    // Username
    private LabelOptions _usernameLabelOptions;
    private TextInputFieldOptions _usernameInputFieldOptions;

    // Password
    private LabelOptions _passwordLabelOptions;
    private TextInputFieldOptions _passwordInputFieldOptions;

    private ButtonOptions _loginButtonOptions;

    public event Action OnLoginButtonPressed;

    public LoginPanel()
    {
        var inputFieldSize = new UIScreenCoords(250, 30);
        var window = Application.Instance.Window;

        _username = string.Empty;
        _password = string.Empty;

        var loginPanelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var loginPanelPosition = new UIScreenCoords((window.Width - loginPanelSize.X) / 2, (window.Height - loginPanelSize.Y) / 2);
        _loginPanelOptions = new PanelOptions(loginPanelPosition, loginPanelSize, string.Empty);

        var loginTitlePosition = new UIScreenCoords(loginPanelPosition);
        _loginTitleOptions = new LabelOptions(loginTitlePosition, "LOGIN", 34);

        var usernameLabelPosition = new UIScreenCoords(loginTitlePosition.X + 50, loginTitlePosition.Y + 50);
        _usernameLabelOptions = new LabelOptions(usernameLabelPosition, "Username: ", 26);

        var usernameInputFieldPosition = new UIScreenCoords(usernameLabelPosition.X + 150, usernameLabelPosition.Y);
        _usernameInputFieldOptions = new TextInputFieldOptions(usernameInputFieldPosition, inputFieldSize);

        var passwordLabelPosition = new UIScreenCoords(loginTitlePosition.X + 50, loginTitlePosition.Y + 100);
        _passwordLabelOptions = new LabelOptions(passwordLabelPosition, "Password: ", 26);

        var passwordInputFieldPosition = new UIScreenCoords(passwordLabelPosition.X + 150, passwordLabelPosition.Y);
        _passwordInputFieldOptions = new TextInputFieldOptions(passwordInputFieldPosition, inputFieldSize);

        var loginButtonSize = new UIScreenCoords(400, 50);
        var loginButtonPosition = new UIScreenCoords((loginPanelPosition.X + (loginPanelSize.X / 2)) - loginButtonSize.X / 2, loginTitlePosition.Y + 150);
        _loginButtonOptions = new ButtonOptions(loginButtonPosition, loginButtonSize, "Login for fanden i helved!");
        _loginButtonOptions.FontSize = 24;
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_loginPanelOptions);
        UIRenderer.DrawLabel(_loginTitleOptions);

        UIRenderer.DrawLabel(_usernameLabelOptions);
        if (UIRenderer.DrawTextInputField(_usernameInputFieldOptions, ref _username, 32, _usernameEditMode))
        {
            _usernameEditMode = !_usernameEditMode;
        }

        UIRenderer.DrawLabel(_passwordLabelOptions);
        if (UIRenderer.DrawTextInputField(_passwordInputFieldOptions, ref _password, 32, _passwordEditMode))
        {
            _passwordEditMode = !_passwordEditMode;
        }

        if (UIRenderer.DrawButton(_loginButtonOptions))
        {
            if (string.IsNullOrWhiteSpace(_username))
            {
                //ConsolePanel.Add($"Username is empty! Please enter a username.");
            }
            if (string.IsNullOrWhiteSpace(_password))
            {
                //ConsolePanel.Add($"Password is empty! Please enter a password.");
            }

            if (!string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
            {
                //ConsolePanel.Add($"{_username}:{_password} logging in...");
            }

            OnLoginButtonPressed?.Invoke();
        }
    }
}
