using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class Menu
{
    private const int PANEL_WIDTH = 300;
    private const int PANEL_HEIGHT = 275;
    private const int PADDING = 25;

    private PanelOptions _panelOptions;
    private ButtonOptions _playButton;
    private ButtonOptions _loadButton;
    private ButtonOptions _quitButton;

    public event Action OnPlayButtonPressed;
    public event Action OnLoadButtonPressed;

    public Menu()
    {
        var window = Application.Instance.Window;

        var panelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var panelPosition = new UIScreenCoords((window.Width - panelSize.X) / 2, (window.Height - panelSize.Y) / 2);
        _panelOptions = new PanelOptions(panelPosition, panelSize, "Main Menu", false);

        var buttonSize = new UIScreenCoords(250, 50);
        var buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, panelPosition.Y + 50);
        _playButton = new ButtonOptions(buttonPosition, buttonSize, "Play");

        buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, panelPosition.Y + 100 + PADDING);
        _loadButton = new ButtonOptions(buttonPosition, buttonSize, "Load");

        buttonPosition = new UIScreenCoords(panelPosition.X + (panelSize.X / 2) - buttonSize.X / 2, panelPosition.Y + 175 + PADDING);
        _quitButton = new ButtonOptions(buttonPosition, buttonSize, "Quit");
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);

        if (UIRenderer.DrawButton(_playButton))
        {
            OnPlayButtonPressed?.Invoke();
        }

        if (UIRenderer.DrawButton(_loadButton))
        {
            OnLoadButtonPressed?.Invoke();
        }

        if (UIRenderer.DrawButton(_quitButton))
        {
            var app = Application.Instance;
            app.Close();
        }
    }
}
