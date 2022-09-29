using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class CreateWorldPanel
{
    private const int PANEL_WIDTH = 500;
    private const int PANEL_HEIGHT = 250;

    private string _playerName;
    private bool _playerNameEditMode = false;

    private string _mapSeed;
    private bool _mapSeedEditMode = false;

    private PanelOptions _panelOptions;
    private LabelOptions _titleOptions;

    // Username
    private LabelOptions _playerNameLabelOptions;
    private TextInputFieldOptions _playerNameInputFieldOptions;

    // Password
    private LabelOptions _mapSeedLabelOptions;
    private TextInputFieldOptions _mapSeedInputFieldOptions;

    private ButtonOptions _createButtonOptions;
    private ButtonOptions _backButtonOptions;

    public event Action<string, int> OnCreateButtonClicked;
    public event Action OnBackButtonClicked;

    public CreateWorldPanel()
    {
        var inputFieldSize = new UIScreenCoords(250, 30);
        var window = Application.Instance.Window;

        _playerName = string.Empty;
        _mapSeed = "80085";

        var panelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var panelPosition = new UIScreenCoords((window.Width - panelSize.X) / 2, (window.Height - panelSize.Y) / 2);
        _panelOptions = new PanelOptions(panelPosition, panelSize, string.Empty);

        var loginTitlePosition = new UIScreenCoords(panelPosition);
        _titleOptions = new LabelOptions(loginTitlePosition, "CREATE CHARACTER", 34);

        var usernameLabelPosition = new UIScreenCoords(loginTitlePosition.X + 50, loginTitlePosition.Y + 50);
        _playerNameLabelOptions = new LabelOptions(usernameLabelPosition, "Name: ", 26);

        var usernameInputFieldPosition = new UIScreenCoords(usernameLabelPosition.X + 150, usernameLabelPosition.Y);
        _playerNameInputFieldOptions = new TextInputFieldOptions(usernameInputFieldPosition, inputFieldSize);

        var passwordLabelPosition = new UIScreenCoords(loginTitlePosition.X + 50, loginTitlePosition.Y + 100);
        _mapSeedLabelOptions = new LabelOptions(passwordLabelPosition, "Map Seed: ", 26);

        var passwordInputFieldPosition = new UIScreenCoords(passwordLabelPosition.X + 150, passwordLabelPosition.Y);
        _mapSeedInputFieldOptions = new TextInputFieldOptions(passwordInputFieldPosition, inputFieldSize);

        var createButtonSize = new UIScreenCoords(400, 50);
        var createButtonPosition = new UIScreenCoords((panelPosition.X + (panelSize.X / 2)) - createButtonSize.X / 2, loginTitlePosition.Y + 150);
        _createButtonOptions = new ButtonOptions(createButtonPosition, createButtonSize, "Create for fanden!");
        _createButtonOptions.FontSize = 24;

        var buttonSize = new UIScreenCoords(250, 30);
        var buttonPosition = new UIScreenCoords((panelPosition.X + (panelSize.X / 2)) - buttonSize.X / 2, (panelPosition.Y + panelSize.Y));
        _backButtonOptions = new ButtonOptions(buttonPosition, buttonSize, "<-- Back");
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);
        UIRenderer.DrawLabel(_titleOptions);

        UIRenderer.DrawLabel(_playerNameLabelOptions);
        if (UIRenderer.DrawTextInputField(_playerNameInputFieldOptions, ref _playerName, 32, _playerNameEditMode))
        {
            _playerNameEditMode = !_playerNameEditMode;
        }

        UIRenderer.DrawLabel(_mapSeedLabelOptions);
        if (UIRenderer.DrawTextInputField(_mapSeedInputFieldOptions, ref _mapSeed, 32, _mapSeedEditMode))
        {
            _mapSeedEditMode = !_mapSeedEditMode;
        }

        if (UIRenderer.DrawButton(_createButtonOptions))
        {
            _ = int.TryParse(_mapSeed, out var seed);
            OnCreateButtonClicked?.Invoke(_playerName, seed);
        }

        if (UIRenderer.DrawButton(_backButtonOptions))
        {
            OnBackButtonClicked?.Invoke();
        }
    }
}
