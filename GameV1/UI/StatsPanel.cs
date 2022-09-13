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

    private const int INVENTORY_SIZE = 10;
    private const int ARMOR_SLOTS = 3;
    private const int EQUIPMENT_SLOTS = 2;

    public const int WIDTH = 330;

    private Player _player;

    private PanelOptions _panelOptions;
    private ImageOptions _seperatorImageOptions;

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

    // Inventory 
    private ImageOptions[] _inventoryOptions = new ImageOptions[INVENTORY_SIZE];
    private ImageOptions _equipmentBackgroundOptions;
    private ImageOptions[] _armorOptions = new ImageOptions[ARMOR_SLOTS];
    private ImageOptions[] _equipmentOptions = new ImageOptions[EQUIPMENT_SLOTS];

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

        var seperatorImage = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\Seperator.png");
        var seperatorSize = new UIScreenCoords(size.X - 10, 16);
        var seperatorPosition = new UIScreenCoords(window.Width - size.X + 5, position.Y + 30);
        _seperatorImageOptions = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);

        // Inventory
        var inventorySlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlot.png");
        var inventorySlotSize = new UIScreenCoords(52, 52);
        var startingPosition = new UIScreenCoords(window.Width - size.X + 15, position.Y + 55);
        const int padding = 10;
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            var inventorySlotPosition = startingPosition;
            inventorySlotPosition.X += (inventorySlotSize.X + padding) * (i % 5);
            inventorySlotPosition.Y = (i > 4) ? (startingPosition.Y + inventorySlotSize.Y + padding) : startingPosition.Y;

            _inventoryOptions[i] = new ImageOptions(inventorySlotPosition, inventorySlotSize, inventorySlotTexture);
        }

        var equipmentBackground = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\EquipBackground.png");
        var equipmentBackgroundSize = new UIScreenCoords(224, 326);
        var equipmentBackgroundPosition = new UIScreenCoords(window.Width - ((size.X / 2) + equipmentBackgroundSize.X / 2), startingPosition.Y + 150);
        _equipmentBackgroundOptions = new ImageOptions(equipmentBackgroundPosition, equipmentBackgroundSize, equipmentBackground, new Color(0, 0, 0, 115));

        // Armor slots
        startingPosition = new UIScreenCoords(equipmentBackgroundPosition.X + ((equipmentBackgroundSize.X / 2 - inventorySlotSize.X / 2)), equipmentBackgroundPosition.Y + 25);
        _armorOptions[0] = new ImageOptions(startingPosition, inventorySlotSize, inventorySlotTexture);
        startingPosition.Y += 115;
        _armorOptions[1] = new ImageOptions(startingPosition, inventorySlotSize, inventorySlotTexture);
        startingPosition.Y += 130;
        _armorOptions[2] = new ImageOptions(startingPosition, inventorySlotSize, inventorySlotTexture);

        // Equipment slots
        startingPosition = new UIScreenCoords(equipmentBackgroundPosition.X, equipmentBackgroundPosition.Y);
        for (int i = 0; i < EQUIPMENT_SLOTS; i++)
        {
            var equipmentSlotPosition = startingPosition;
            equipmentSlotPosition.X += (i == 0) ? 0 : (int)(equipmentBackgroundSize.X * 0.77);
            equipmentSlotPosition.Y += equipmentBackground.height / 2;

            _equipmentOptions[i] = new ImageOptions(equipmentSlotPosition, inventorySlotSize, inventorySlotTexture);
        }
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

        // Inventory
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            UIRenderer.DrawImage(_inventoryOptions[i]);
        }
        UIRenderer.DrawImage(_equipmentBackgroundOptions);

        for (int i = 0; i < ARMOR_SLOTS; i++)
        {
            UIRenderer.DrawImage(_armorOptions[i]);
        }

        for (int i = 0; i < EQUIPMENT_SLOTS; i++)
        {
            UIRenderer.DrawImage(_equipmentOptions[i]);
        }
    }
}
