using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Graphics;
using System.Numerics;

namespace GameV1.UI;

internal class StatsPanel
{
	private Player _player;

	private Vector2 _position;
	private Vector2 _size;

	private float _startHealth;

	public StatsPanel(Player player)
	{
		_player = player;
		_startHealth = _player.Stats.Health;

		var window = Application.Instance.Window;

		_size = new Vector2(400, window.Height);
		_position = new Vector2(window.Width - _size.X, 0);
	}

	private float damage = 50;

	public void OnGUI(IUIRenderer UIRenderer)
	{
		UIRenderer.DrawPanel((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y, "Stats");

        UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + 28, $"Health: {_player.Stats.Health}");
		UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + (28 * 2), $"Movement Points: {_player.Stats.MovementPoints}");

        damage = (int)UIRenderer.DrawSlider(new Raylib_cs.Rectangle(10, 70, 250, 20), string.Empty, $"{damage} Damage", damage, 0, 250, 0);
		if(UIRenderer.DrawButton(10, 92, "Die"))
        {
			_player.TakeDamage((int)damage);
		}

		string text = $"{_player.Stats.Health} Health";
		if(_player.IsDead)
		{
			text = "You fucking died!";
        }
		UIRenderer.DrawLabel((int)_position.X + 4, (int)_position.Y + (28 * 3), text);
        _player.Stats.Health = (int)UIRenderer.DrawSlider(new Raylib_cs.Rectangle((int)_position.X + 10, (int)_position.Y + (28 * 4), 380, 20), string.Empty, string.Empty, _player.Stats.Health, 0, _startHealth, 0);
    }
}
