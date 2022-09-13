using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class StatsPanel
{
    private readonly static Color HEADER_COLOR = new Color(68, 48, 41, 255);
    private readonly static Color BACKGROUND_COLOR = new Color(63, 42, 39, 255);

    private readonly static Color HEALTH_BAR_SLIDER_COLOR = new Color(178, 32, 25, 255);
    private readonly static Color EXPERIENCE_BAR_SLIDER_COLOR = new Color(239, 58, 12, 255);
    private readonly static Color FATIGUE_BAR_SLIDER_COLOR = new Color(57, 87, 28, 255);
    private readonly static Color SLIDER_BACKGROUND_COLOR = new Color(43, 37, 36);

    private readonly static Color TEXT_COLOR = new Color(165, 98, 67, 255);

    public const int WIDTH = 330;

    private Player _player;

    private PanelOptions _panelOptions;
    private ImageOptions _seperatorImageOptions;

    private SeperatorOptions _seperatorOptions;

    // Stats
    private SubImageOptions _portraitOptions;
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
        _panelOptions = new PanelOptions(position, size, "Stats", HEADER_COLOR, TEXT_COLOR, HEADER_COLOR, BACKGROUND_COLOR, false);

        var portait = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\8bitPortraitPack24x24.png");
        var portaitSize = new UIScreenCoords(180, 180);
        var portraitPosition = new UIScreenCoords(1200 + (400 - portaitSize.X - 10), 25);
        _portraitOptions = new SubImageOptions(portraitPosition, portaitSize, new MooseEngine.Utilities.Coords2D(0, 4), 24, portait);

        _player.Stats.Strength = 9999;
        _player.Stats.Agility = 9999;
        _player.Stats.Toughness = 9999;
        _player.Stats.Perception = 9999;
        _player.Stats.Charisma = 9999;

        _player.Stats.Fatigue = 100;

        var strengthLabelPosition = new UIScreenCoords(startPosition.X + 4, 35);
        _strengthLabelOptions = new LabelOptions(strengthLabelPosition, $"STR: {_player.Stats.Strength}", 96, TEXT_COLOR);

        var agilityLabelPosition = new UIScreenCoords(startPosition.X + 4, 70);
        _agilityLabelOptions = new LabelOptions(agilityLabelPosition, $"AGI: {_player.Stats.Agility}", 96, TEXT_COLOR);

        var toughnessLabelPosition = new UIScreenCoords(startPosition.X + 4, 105);
        _toughnessLabelOptions = new LabelOptions(toughnessLabelPosition, $"TOU: {_player.Stats.Toughness}", 96, TEXT_COLOR);

        var perceptionLabelPosition = new UIScreenCoords(startPosition.X + 4, 140);
        _perceptionLabelOptions = new LabelOptions(perceptionLabelPosition, $"PER: {_player.Stats.Perception}", 96, TEXT_COLOR);

        var charismaLabelPosition = new UIScreenCoords(startPosition.X + 4, 175);
        _charismaLabelOptions = new LabelOptions(charismaLabelPosition, $"CHA: {_player.Stats.Charisma}", 96, TEXT_COLOR);

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 215);
        var healthBarSize = new UIScreenCoords(265, 25);
        _healthBarOptions = new SliderOptions(position, healthBarSize, "HP", 24, 0, 0, TextAlignment.Left, SLIDER_BACKGROUND_COLOR, TEXT_COLOR, HEALTH_BAR_SLIDER_COLOR, 0, _player.Stats.Health, false);

        var exp = 1000;

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 245);
        var experienceBarSize = new UIScreenCoords(265, 24);
        _experienceBarOptions = new SliderOptions(position, experienceBarSize, "XP", 24, 0, 0, TextAlignment.Left, SLIDER_BACKGROUND_COLOR, TEXT_COLOR, EXPERIENCE_BAR_SLIDER_COLOR, 0, exp, false);

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 275);
        var fatigueBarSize = new UIScreenCoords(265, 25);
        _fatigueBarOptions = new SliderOptions(position, fatigueBarSize, "FT", 24, 0, 0, TextAlignment.Left, SLIDER_BACKGROUND_COLOR, TEXT_COLOR, FATIGUE_BAR_SLIDER_COLOR, 0, _player.Stats.Fatigue, false);

        var seperatorImage = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources/Textures/Seperator.png");
        var seperatorSize = new UIScreenCoords(size.X - 10, 16);
        var seperatorPosition = new UIScreenCoords(window.Width - size.X + 5, position.Y + 30);
        _seperatorImageOptions = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);
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

        UIRenderer.DrawImage(_seperatorImageOptions);
    }
}
