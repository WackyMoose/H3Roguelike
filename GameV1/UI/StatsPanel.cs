using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class StatsPanel
{
    public const int WIDTH = 330;

    private Player _player;

    private PanelOptions _panelOptions;
    private SeperatorOptions _seperatorOptions;

    // Stats
    private ImageOptions _portraitOptions;
    private LabelOptions _strengthLabelOptions;
    private LabelOptions _agilityLabelOptions;
    private LabelOptions _toughnessLabelOptions;
    private LabelOptions _perceptionLabelOptions;
    private LabelOptions _charismaLabelOptions;

    private SliderOptions _healthBarOptions;
    private SliderOptions _experienceBarOptions;
    private SliderOptions _fatigueBarOptions;

    public StatsPanel(Player player)
    {
        _player = player;

        var window = Application.Instance.Window;

        var size = new UIScreenCoords(WIDTH, window.Height);
        var position = new UIScreenCoords(window.Width - size.X, 0);
        var startPosition = position;
        _panelOptions = new PanelOptions(position, size, "Stats");

        var portait = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\8bitPortraitPack24x24.png");
        var portaitSize = new UIScreenCoords(180, 180);
        var portraitPosition = new UIScreenCoords(1200 + (400 - portaitSize.X - 10), 25);
        _portraitOptions = new ImageOptions(portraitPosition, portaitSize, new MooseEngine.Utilities.Coords2D(0, 4), 24, portait);

        _player.Stats.Strength = 9999;
        _player.Stats.Agility = 9999;
        _player.Stats.Toughness = 9999;
        _player.Stats.Perception = 9999;
        _player.Stats.Charisma = 9999;

        _player.Stats.Fatigue = 100;

        var strengthLabelPosition = new UIScreenCoords(startPosition.X + 4, 35);
        _strengthLabelOptions = new LabelOptions(strengthLabelPosition, 96, $"STR: {_player.Stats.Strength}");

        var agilityLabelPosition = new UIScreenCoords(startPosition.X + 4, 70);
        _agilityLabelOptions = new LabelOptions(agilityLabelPosition, 96, $"AGI: {_player.Stats.Agility}");

        var toughnessLabelPosition = new UIScreenCoords(startPosition.X + 4, 105);
        _toughnessLabelOptions = new LabelOptions(toughnessLabelPosition, 96, $"TOU: {_player.Stats.Toughness}");

        var perceptionLabelPosition = new UIScreenCoords(startPosition.X + 4, 140);
        _perceptionLabelOptions = new LabelOptions(perceptionLabelPosition, 96, $"PER: {_player.Stats.Perception}");

        var charismaLabelPosition = new UIScreenCoords(startPosition.X + 4, 175);
        _charismaLabelOptions = new LabelOptions(charismaLabelPosition, 96, $"CHA: {_player.Stats.Charisma}");

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 215);
        var healthBarSize = new UIScreenCoords(270, 25);
        _healthBarOptions = new SliderOptions(position, healthBarSize, 24, "HP", TextAlignment.Left, 0, _player.Stats.Health, Color.Red, false);

        var exp = 1000;

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 245);
        var experienceBarSize = new UIScreenCoords(270, 24);
        _experienceBarOptions = new SliderOptions(position, experienceBarSize, 24, "XP", TextAlignment.Left, 0, exp, Color.Orange, false);

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 275);
        var fatigueBarSize = new UIScreenCoords(270, 25);
        _fatigueBarOptions = new SliderOptions(position, fatigueBarSize, 24, "FT", TextAlignment.Left, 0, _player.Stats.Fatigue, Color.Green, false);

        position = new UIScreenCoords(window.Width - size.X + 5, position.Y + 30);
        _seperatorOptions = new SeperatorOptions(position, size.X - 10);
    }

    public void OnGUI(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawPanel(_panelOptions);

        UIRenderer.DrawImage(_portraitOptions);
        UIRenderer.DrawLabel(_strengthLabelOptions);
        UIRenderer.DrawLabel(_agilityLabelOptions);
        UIRenderer.DrawLabel(_toughnessLabelOptions);
        UIRenderer.DrawLabel(_perceptionLabelOptions);
        UIRenderer.DrawLabel(_charismaLabelOptions);

        UIRenderer.DrawSliderBar(_healthBarOptions, _player.Stats.Health);
        UIRenderer.DrawSliderBar(_experienceBarOptions, 555.0f);
        UIRenderer.DrawSliderBar(_fatigueBarOptions, _player.Stats.Fatigue / 2);

        UIRenderer.DrawSeperator(_seperatorOptions);
    }
}
