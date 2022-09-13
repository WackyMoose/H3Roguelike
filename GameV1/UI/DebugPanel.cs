using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.UI;

namespace GameV1.UI;

internal class DebugPanel
{
    private ICreature _player;

    private ButtonOptions _consoleButton;
    private SliderOptions _sliderOptions;
    private ButtonOptions _buttonOptions;

    private int damage = 50;
    private static int _index = 0;

    public DebugPanel(int x, int y, ICreature player)
    {
        _player = player;

        var debugSliderPosition = new UIScreenCoords(10, 70);
        var debugSliderSize = new UIScreenCoords(250, 30);
        _sliderOptions = new SliderOptions(debugSliderPosition, debugSliderSize, 24, $"{damage} Damage", TextAlignment.Right, 0, 250);

        var btnPosition = new UIScreenCoords(10, 124);
        var btnSize = new UIScreenCoords(100, 50);
        _buttonOptions = new ButtonOptions(btnPosition, btnSize, "Die");

        btnPosition = new UIScreenCoords(10, 196);
        btnSize = new UIScreenCoords(100, 50);
        _consoleButton = new ButtonOptions(btnPosition, btnSize, "Add");
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        _sliderOptions.Text = $"{damage} Damage";
        damage = (int)UIRenderer.DrawSliderBar(_sliderOptions, damage);
        if (UIRenderer.DrawButton(_buttonOptions))
        {
            _player.TakeDamage(damage);
        }

        if (UIRenderer.DrawButton(_consoleButton))
        {
            ConsolePanel.Add($"Test{++_index}");
        }
    }
}
