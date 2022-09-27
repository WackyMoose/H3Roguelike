using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class SaveGamesPanel
{
    private const int PANEL_WIDTH = 300;
    private const int PANEL_HEIGHT = 350;

    private PanelOptions _panelOptions;

    private ButtonOptions _backButton;
    private ButtonOptions _loadGameButton;

    public event Action OnBackButtonPressed;

    public SaveGamesPanel()
    {
        var window = Application.Instance.Window;   

        var panelSize = new UIScreenCoords(PANEL_WIDTH, PANEL_HEIGHT);
        var panelPosition = new UIScreenCoords((window.Width - panelSize.X) / 2, (window.Height - panelSize.Y) / 2);
        _panelOptions = new PanelOptions(panelPosition, panelSize, "Save Games");

        var buttonSize = new UIScreenCoords(250, 30);
        var buttonPosition = new UIScreenCoords((panelPosition.X + (panelSize.X / 2)) - buttonSize.X / 2, (panelPosition.Y + panelSize.Y));
        _backButton = new ButtonOptions(buttonPosition, buttonSize, "<-- Back");
    }
    
    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);

        if(UIRenderer.DrawButton(_backButton))
        {
            OnBackButtonPressed?.Invoke();
        }
    }
}
