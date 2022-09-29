using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;

namespace GameV1.UI;

internal class StatsPanel
{
    private const int INVENTORY_SIZE = 10;
    private const int EQUIPMENT_SIZE = 5;

    public const int WIDTH = 330;

    /*  Idea for new UI design? 🤔
        private Panel _panel = new Panel("Stats Panel") {
            HeaderText = "Stats/Name",
            HeaderTextColor = HEADER_TEXT_COLOR,
            HeaderColor = HEADER_COLOR,
            BackgroundColor = BACKGROUND_COLOR,
            InnerElements: [
                new SubImageOptions("Portrait") {  
                    Image = "player",
                    Width = 180,
                    Height = 180,
                    X = 10,
                    Y = 25
                },
                new LabelOptions("Strength Label") {  
                    Text = "STR: 9999",
                    X = 10,
                    Y = 25
                },
                new LabelOptions("Agility Label") {  
                    Text = "AGI: 9999",
                    X = 10,
                    Y = 25
                },
                new LabelOptions("Toughness Label") {  
                    Text = "TOU: 9999",
                    X = 10,
                    Y = 25
                },
                new LabelOptions("Perception Label") {  
                    Text = "PER: 9999",
                    X = 10,
                    Y = 25
                },
                new LabelOptions("Charisma Label") {  
                    Text = "CHA: 9999",
                    X = 10,
                    Y = 25
                },
                new SliderOptions("Health Slider") {  
                    Text = "HP",
                    TextAlignment: TextAlignment.Left,
                    MinValue = 0,
                    MaxValue = 100,
                    Value = 50,
                    X = 10,
                    Y = 25
                },
                new SliderOptions("Experience Slider") {  
                    Text = "XP",
                    TextAlignment: TextAlignment.Left,
                    MinValue = 0,
                    MaxValue = 100,
                    Value = 50,
                    X = 10,
                    Y = 25
                },
                new SliderOptions("Fatigue Slider") {  
                    Text = "FT",
                    TextAlignment: TextAlignment.Left,
                    MinValue = 0,
                    MaxValue = 100,
                    Value = 50,
                    X = 10,
                    Y = 25
                }
            ]
        };
     */

    private ICreature _player;

    private PanelOptions _panelOptions;
    private ImageOptions[] _seperatorOptions = new ImageOptions[3];

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
    private ImageOptions[] _equipmentOptions = new ImageOptions[EQUIPMENT_SIZE];

    private SelectorListView _selectorListView;

    public StatsPanel(ICreature player)
    {
        _player = player;

        var window = Application.Instance.Window;

        var size = new UIScreenCoords(WIDTH, window.Height);
        var position = new UIScreenCoords(window.Width - size.X, 0);
        var startPosition = position;
        _panelOptions = new PanelOptions(position, size, "Stats", UIColors.StatsPanel.HEADER_COLOR, UIColors.StatsPanel.HEADER_TEXT_COLOR, UIColors.StatsPanel.BORDER_COLOR, UIColors.StatsPanel.BACKGROUND_COLOR, false);

        var portait = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\8bitPortraitPack24x24.png");
        var portaitSize = new UIScreenCoords(180, 180);
        var portraitPosition = new UIScreenCoords(1200 + (400 - portaitSize.X - 10), 25);
        _portraitOptions = new SubImageOptions(portraitPosition, portaitSize, new MooseEngine.Utilities.Coords2D(0, 4), 24, portait);

        //_player.Stats.Strength = 9999;
        //_player.Stats.Agility = 9999;
        //_player.Stats.Toughness = 9999;
        //_player.Stats.Perception = 9999;
        //_player.Stats.Charisma = 9999;

        //_player.Stats.Fatigue = 100;

        var strengthLabelPosition = new UIScreenCoords(startPosition.X + 4, 35);
        _strengthLabelOptions = new LabelOptions(strengthLabelPosition, $"STR: {_player.Stats.Strength}", 26, UIColors.StatsPanel.TEXT_COLOR);

        var agilityLabelPosition = new UIScreenCoords(startPosition.X + 4, 70);
        _agilityLabelOptions = new LabelOptions(agilityLabelPosition, $"AGI: {_player.Stats.Agility}", 26, UIColors.StatsPanel.TEXT_COLOR);

        var toughnessLabelPosition = new UIScreenCoords(startPosition.X + 4, 105);
        _toughnessLabelOptions = new LabelOptions(toughnessLabelPosition, $"TOU: {_player.Stats.Toughness}", 26, UIColors.StatsPanel.TEXT_COLOR);

        var perceptionLabelPosition = new UIScreenCoords(startPosition.X + 4, 140);
        _perceptionLabelOptions = new LabelOptions(perceptionLabelPosition, $"PER: {_player.Stats.Perception}", 26, UIColors.StatsPanel.TEXT_COLOR);

        var charismaLabelPosition = new UIScreenCoords(startPosition.X + 4, 175);
        _charismaLabelOptions = new LabelOptions(charismaLabelPosition, $"CHA: {_player.Stats.Charisma}", 26, UIColors.StatsPanel.TEXT_COLOR);

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 215);
        var healthBarSize = new UIScreenCoords(265, 25);
        _healthBarOptions = new SliderOptions(position, healthBarSize, "HP", 24, 0, 0, TextAlignment.Left, 0, _player.Stats.Health, false);
        _healthBarOptions.BackgroundColor = UIColors.StatsPanel.SLIDER_BACKGROUND_COLOR;
        _healthBarOptions.NormalColor = UIColors.StatsPanel.HEALTH_BAR_SLIDER_COLOR;
        _healthBarOptions.TextNormalColor = UIColors.StatsPanel.TEXT_COLOR;

        var exp = 1000;

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 245);
        var experienceBarSize = new UIScreenCoords(265, 25);
        _experienceBarOptions = new SliderOptions(position, experienceBarSize, "XP", 24, 0, 0, TextAlignment.Left, 0, exp, false);
        _experienceBarOptions.BackgroundColor = UIColors.StatsPanel.SLIDER_BACKGROUND_COLOR;
        _experienceBarOptions.NormalColor = UIColors.StatsPanel.EXPERIENCE_BAR_SLIDER_COLOR;
        _experienceBarOptions.TextNormalColor = UIColors.StatsPanel.TEXT_COLOR;

        position = new UIScreenCoords(startPosition.X + 55, startPosition.Y + 275);
        var fatigueBarSize = new UIScreenCoords(265, 25);
        _fatigueBarOptions = new SliderOptions(position, fatigueBarSize, "FT", 24, 0, 0, TextAlignment.Left, 0, _player.Stats.Fatigue, false);
        _fatigueBarOptions.BackgroundColor = UIColors.StatsPanel.SLIDER_BACKGROUND_COLOR;
        _fatigueBarOptions.NormalColor = UIColors.StatsPanel.FATIGUE_BAR_SLIDER_COLOR;
        _fatigueBarOptions.TextNormalColor = UIColors.StatsPanel.TEXT_COLOR;

        var seperatorImage = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\Seperator.png");
        var seperatorSize = new UIScreenCoords(size.X - 10, 16);
        var seperatorPosition = new UIScreenCoords(window.Width - size.X + 5, position.Y + 30);
        _seperatorOptions[0] = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);

        // Inventory
        var inventorySlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlot.png");
        var inventorySlotSize = new UIScreenCoords(52, 52);
        var startingPosition = new UIScreenCoords(window.Width - size.X + 15, position.Y + 55);
        const int padding = 10;
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            var inventorySlotPosition = startingPosition;
            inventorySlotPosition.X += (inventorySlotSize.X + padding) * (i % 5);
            inventorySlotPosition.Y = (i > 4) ? startingPosition.Y + inventorySlotSize.Y + padding : startingPosition.Y;

            _inventoryOptions[i] = new ImageOptions(inventorySlotPosition, inventorySlotSize, inventorySlotTexture);
        }

        seperatorPosition.Y += 150;
        _seperatorOptions[1] = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);

        startingPosition.Y += 150;
        for (int i = 0; i < EQUIPMENT_SIZE; i++)
        {
            var equipmentSlotPosition = startingPosition;
            equipmentSlotPosition.X += (inventorySlotSize.X + padding) * (i % 5);
            _equipmentOptions[i] = new ImageOptions(equipmentSlotPosition, inventorySlotSize, inventorySlotTexture);
        }

        seperatorPosition.Y += 90;
        _seperatorOptions[2] = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);

        var listViewPosition = new UIScreenCoords(window.Width - size.X + 10, seperatorPosition.Y + 15);
        var listViewSize = new UIScreenCoords(size.X - 10, 200);
        _selectorListView = new SelectorListView(listViewPosition, listViewSize);
    }

    public void SetPlayerName(string name)
    {
        _panelOptions.Text = name;
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

        for (int i = 0; i < _seperatorOptions.Length; i++)
        {
            UIRenderer.DrawImage(_seperatorOptions[i]);
        }

        // Inventory
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            UIRenderer.DrawImage(_inventoryOptions[i]);
        }

        // Equipment
        for (int i = 0; i < EQUIPMENT_SIZE; i++)
        {
            UIRenderer.DrawImage(_equipmentOptions[i]);
        }

        _selectorListView.OnGUI(UIRenderer);
    }

}
