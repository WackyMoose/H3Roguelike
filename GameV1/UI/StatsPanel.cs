using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;
using System.Numerics;
//using System.Drawing;

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
    private Raylib_cs.Texture2D _spriteSheet;

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

    private SubImageOptions[] _inventoryItemsOptions = new SubImageOptions[INVENTORY_SIZE];
    private SubImageOptions[] _equippedItemsOptions = new SubImageOptions[EQUIPMENT_SIZE];

    private ListViewOptions _listViewOptions;
    static string[] items =
    {
        "Orc Warloard",
        "> Orc Bruiser",
        "Orc Shaman",
        "Goblin Looter"
    };

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
        var bodyArmorSlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlotChest.png");
        var footWearSlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlotFeet.png");
        var headGearSlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlotHead.png");
        var primaryWeaponSlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlotSword.png");
        var secondaryWeaponSlotTexture = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\InventorySlotShield.png");


        var inventorySlotSize = new UIScreenCoords(52, 52);
        var startingPosition = new UIScreenCoords(window.Width - size.X + 15, position.Y + 55);
        const int padding = 10;

        var inventoryItemSize = inventorySlotSize; // new UIScreenCoords(40, 40);

        // Spritesheet, for rendering inventory content
        _spriteSheet = Raylib_cs.Raylib.LoadTexture(@"..\..\..\Resources\Textures\Tilemap_Modified_MSN.png");

        for (int i = 0; i < _player.Inventory.Inventory.Slots.Count(); i++)
        {
            var inventorySlotPosition = startingPosition;
            inventorySlotPosition.X += (inventorySlotSize.X + padding) * (i % 5);
            inventorySlotPosition.Y = (i > 4) ? startingPosition.Y + inventorySlotSize.Y + padding : startingPosition.Y;

            _inventoryOptions[i] = new ImageOptions(inventorySlotPosition, inventorySlotSize, inventorySlotTexture);

            _inventoryItemsOptions[i] = new SubImageOptions(inventorySlotPosition, inventoryItemSize, new Coords2D(1, 0), 8, _spriteSheet);
        }

        seperatorPosition.Y += 150;
        _seperatorOptions[1] = new ImageOptions(seperatorPosition, seperatorSize, seperatorImage);

        startingPosition.Y += 150;
        for (int i = 0; i < EQUIPMENT_SIZE; i++)
        {
            var equipmentSlotPosition = startingPosition;
            equipmentSlotPosition.X += (inventorySlotSize.X + padding) * (i % 5);
            _equipmentOptions[i] = new ImageOptions(equipmentSlotPosition, inventorySlotSize, inventorySlotTexture);

            _equippedItemsOptions[i] = new SubImageOptions(equipmentSlotPosition, inventoryItemSize, new Coords2D(2000, 0), 8, _spriteSheet);
        }

        // Set equip slots background images
        _equipmentOptions[0].Image = primaryWeaponSlotTexture;
        _equipmentOptions[1].Image = secondaryWeaponSlotTexture;
        _equipmentOptions[2].Image = headGearSlotTexture;
        _equipmentOptions[3].Image = bodyArmorSlotTexture;
        _equipmentOptions[4].Image = footWearSlotTexture;

        UpdateInventory(_player);

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
           UIRenderer.DrawImage(_inventoryItemsOptions[i]);
        }

        // Equipment
        for (int i = 0; i < EQUIPMENT_SIZE; i++)
        {
            UIRenderer.DrawImage(_equipmentOptions[i]);
            UIRenderer.DrawImage(_equippedItemsOptions[i]);
        }

        _selectorListView.OnGUI(UIRenderer);
    }

    public void UpdateInventory(ICreature player)
    {
        var defaultTexture = new Coords2D(0, 16);

        for (int i = 0; i < player.Inventory.Inventory.Slots.Count(); i++)
        {
            Coords2D? inventoryItem = player.Inventory.Inventory.Slots.ElementAt(i).Item?.SpriteCoords;

            if (inventoryItem is not null)
            {
                _inventoryItemsOptions[i].Coords = (Coords2D)inventoryItem;
            }
            else
            {
                _inventoryItemsOptions[i].Coords = defaultTexture;
            }
        }

        Coords2D? primaryWeaponCoords = player.Inventory.PrimaryWeapon.Item?.SpriteCoords;
        Coords2D? secondaryWeaponCoords = player.Inventory.SecondaryWeapon.Item?.SpriteCoords;
        Coords2D? headGearCoords = player.Inventory.HeadGear.Item?.SpriteCoords;
        Coords2D? bodyArmorCoords = player.Inventory.BodyArmor.Item?.SpriteCoords;
        Coords2D? FootWearWeaponCoords = player.Inventory.FootWear.Item?.SpriteCoords;

        _equippedItemsOptions[0].Coords = (Coords2D)(primaryWeaponCoords is not null ? primaryWeaponCoords : defaultTexture);
        _equippedItemsOptions[1].Coords = (Coords2D)(secondaryWeaponCoords is not null ? secondaryWeaponCoords : defaultTexture);
        _equippedItemsOptions[2].Coords = (Coords2D)(headGearCoords is not null ? headGearCoords : defaultTexture);
        _equippedItemsOptions[3].Coords = (Coords2D)(bodyArmorCoords is not null ? bodyArmorCoords : defaultTexture);
        _equippedItemsOptions[4].Coords = (Coords2D)(FootWearWeaponCoords is not null ? FootWearWeaponCoords : defaultTexture);

    }
    
    static int s_Focus = 0;
    static int s_ScrollIndex = 0;
    private SelectorListView _selectorListView;
}
