using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI.Options;
using System.Numerics;

namespace GameV1.UI;

internal class StatsPanel
{
    private Player _player;

    private Vector2 _position;
    private Vector2 _size;

    private float _startHealth;
    private SliderOptions _sliderOptions;
    private SliderOptions _healthBarOptions;

    public StatsPanel(Player player)
    {
        _player = player;
        _startHealth = _player.Stats.Health;

        var window = Application.Instance.Window;

        _size = new Vector2(400, window.Height);
        _position = new Vector2(window.Width - _size.X, 0);

        var sliderPosition = new UIScreenCoords(10, 70);
        var sliderSize = new UIScreenCoords(250, 30);
        _sliderOptions = new SliderOptions(sliderPosition, sliderSize, 24, $"{damage} Damage", TextAlignmentSlider.Right, 0, 250);

        sliderPosition = new UIScreenCoords((int)_position.X + 55, (int)_position.Y + (28 * 4));
        sliderSize = new UIScreenCoords(330, 30);
        _healthBarOptions = new SliderOptions(sliderPosition, sliderSize, 24, "HP", TextAlignmentSlider.Left, 0, _player.Stats.Health, false);
        _healthBarOptions.Value = _player.Stats.Health;
    }

    private float damage = 50;

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y, "Stats");

        UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + 28, $"Health: {_player.Stats.Health}");
        UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + (28 * 2), $"Movement Points: {_player.Stats.MovementPoints}");

        _sliderOptions.Text = $"{damage} Damage";
        damage = UIRenderer.DrawSliderBar(_sliderOptions);
        //damage = (int)UIRenderer.DrawSlider(new Raylib_cs.Rectangle(10, 92, 250, 20), string.Empty, $"{damage} Damage", damage, 0, 250, 0);
        if (UIRenderer.DrawButton(10, 124, "Die"))
        {
            _player.TakeDamage((int)damage);
        }

        string text = $"{_player.Stats.Health} Health";
        if (_player.IsDead)
        {
            text = "You fucking died!";
        }
        UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + (28 * 3), text);

        _healthBarOptions.Value = _player.Stats.Health;
        _player.Stats.Health = (int)UIRenderer.DrawSliderBar(_healthBarOptions);
        //_player.Stats.Health = (int)UIRenderer.DrawSlider(new Raylib_cs.Rectangle((int)_position.X + 10, (int)_position.Y + (28 * 7), 380, 20), string.Empty, string.Empty, _player.Stats.Health, 0, _startHealth, 0);
    }
}
