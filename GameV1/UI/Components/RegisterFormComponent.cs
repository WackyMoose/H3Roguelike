﻿using GameV1.Http;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI.Components;

internal class RegisterFormComponent : IUIElement
{
    private const int PANEL_WIDTH = 500;
    private const int PANEL_HEIGHT = 250;

    private string _username;
    private bool _usernameEditMode = false;

    private string _password;
    private bool _passwordEditMode = false;

    private bool _providedNoCredentials = false;

    private PanelOptions _panelOptions;
    private LabelOptions _titleOptions;

    // Username
    private LabelOptions _usernameLabelOptions;
    private TextInputFieldOptions _usernameInputFieldOptions;

    // Password
    private LabelOptions _passwordLabelOptions;
    private TextInputFieldOptions _passwordInputFieldOptions;

    private LabelOptions _wrongCredentialsOptions;

    private ButtonOptions _loginButtonOptions;
    private ButtonOptions _registerButtonOptions;

    public event Action? OnLoginButtonClicked;
    public event Action? OnRegisterButtonClicked;

    private HttpRequester _httpRequester;

    public RegisterFormComponent()
    {
        var inputFieldSize = new UIScreenCoords(250, 30);
        var buttonSize = new UIScreenCoords(175, 30);
        var window = Application.Instance.Window;

        _username = string.Empty;
        _password = string.Empty;

        var panelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var panelPosition = new UIScreenCoords((window.Width - panelSize.X) / 2, (window.Height - panelSize.Y) / 2);
        _panelOptions = new PanelOptions(panelPosition, panelSize, string.Empty);

        var loginTitlePosition = new UIScreenCoords((panelPosition.X + (panelSize.X / 2)) - 75, panelPosition.Y);
        _titleOptions = new LabelOptions(loginTitlePosition, "REGISTER", 34);

        var usernameLabelPosition = new UIScreenCoords(panelPosition.X + 50, panelPosition.Y + 50);
        _usernameLabelOptions = new LabelOptions(usernameLabelPosition, "Username: ", 26);

        var usernameInputFieldPosition = new UIScreenCoords(usernameLabelPosition.X + 150, usernameLabelPosition.Y);
        _usernameInputFieldOptions = new TextInputFieldOptions(usernameInputFieldPosition, inputFieldSize);

        var passwordLabelPosition = new UIScreenCoords(panelPosition.X + 50, panelPosition.Y + 100);
        _passwordLabelOptions = new LabelOptions(passwordLabelPosition, "Password: ", 26);

        var passwordInputFieldPosition = new UIScreenCoords(passwordLabelPosition.X + 150, passwordLabelPosition.Y);
        _passwordInputFieldOptions = new TextInputFieldOptions(passwordInputFieldPosition, inputFieldSize);

        _wrongCredentialsOptions = new LabelOptions(new UIScreenCoords(panelPosition.X + 50, passwordLabelPosition.Y + 40), "Wrong credentials!", 26);
        _wrongCredentialsOptions.TextNormalColor = Color.Red;

        var registerButtonPosition = new UIScreenCoords((panelPosition.X + panelSize.X / 2) + 25, loginTitlePosition.Y + 190);
        _registerButtonOptions = new ButtonOptions(registerButtonPosition, buttonSize, "Register");
        _registerButtonOptions.FontSize = 24;

        var loginButtonPosition = new UIScreenCoords((panelPosition.X + panelSize.X / 2) - 200, loginTitlePosition.Y + 190);
        _loginButtonOptions = new ButtonOptions(loginButtonPosition, buttonSize, "Login");
        _loginButtonOptions.FontSize = 24;

        _httpRequester = new HttpRequester("http://api.cavemangames.dk/api/Auth/");
    }

    public void Reset()
    {
        _providedNoCredentials = false;
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);
        UIRenderer.DrawLabel(_titleOptions);

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

        if (_providedNoCredentials)
        {
            UIRenderer.DrawLabel(_wrongCredentialsOptions);
        }

        if (UIRenderer.DrawButton(_loginButtonOptions))
        {
            OnLoginButtonClicked?.Invoke();
        }

        if (UIRenderer.DrawButton(_registerButtonOptions))
        {
            _providedNoCredentials = string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password);
            if (_providedNoCredentials)
            {
                _wrongCredentialsOptions.Text = "Please provide credentials!";
            }

            // TODO: Go to register user, and log in...
            _httpRequester.Post("register", new { Username = _username, Password = _password });

            if (!_providedNoCredentials)
            {
                OnRegisterButtonClicked?.Invoke();
            }
        }
    }
}
