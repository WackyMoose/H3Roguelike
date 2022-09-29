using GameV1.Entities;
using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;

namespace GameV1.UI;

internal class GameUI : IUIElement
{
    private ButtonOptions _backToMenuButton;

    private ConsolePanel _consolePanel;
    private StatsPanel _statsPanel;

    public event Action BackToMenuButtonClicked;

    public GameUI(ICreature player)
    {
        var window = Application.Instance.Window;

        var buttonPosition = new UIScreenCoords(10, 10);
        var buttonSize = new UIScreenCoords(200, 30);
        _backToMenuButton = new ButtonOptions(buttonPosition, buttonSize, "Back to Menu");

        var consoleSize = new Coords2D(window.Width - StatsPanel.WIDTH, ConsolePanel.HEIGHT);
        var consolePosition = new Coords2D(((window.Width - StatsPanel.WIDTH) / 2) - (consoleSize.X / 2), window.Height - consoleSize.Y);

        _consolePanel = new ConsolePanel(consolePosition, consoleSize, 4);
        _statsPanel = new StatsPanel(player);

        ConsolePanel.Add("Hello there stranger!");
    }

    public void SetPlayerName(string name)
    {
        _statsPanel.SetPlayerName(name);
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        if(UIRenderer.DrawButton(_backToMenuButton))
        {
            BackToMenuButtonClicked?.Invoke();
        }
        
        _consolePanel.OnGUI(UIRenderer);
        _statsPanel.OnGUI(UIRenderer);
    }
}
