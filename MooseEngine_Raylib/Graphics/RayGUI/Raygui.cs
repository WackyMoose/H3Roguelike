using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics.RayGUI;

enum GuiState
{
    STATE_NORMAL = 0,
    STATE_FOCUSED,
    STATE_PRESSED,
    STATE_DISABLED,
}

internal unsafe class Raygui
{
    static GuiState guiState = GuiState.STATE_NORMAL;
    static bool guiLocked = false;
    static Font guiFont;
    static float guiAlpha = 1.0f;
    static bool guiStyleLoaded = false;


    // Gui control state
    enum GuiState : uint
    {
        STATE_NORMAL = 0,
        STATE_FOCUSED,
        STATE_PRESSED,
        STATE_DISABLED,
    }

    // Gui control text alignment
    enum GuiTextAlignment : uint
    {
        TEXT_ALIGN_LEFT = 0,
        TEXT_ALIGN_CENTER,
        TEXT_ALIGN_RIGHT,
    }

    // Gui controls
    enum GuiControl : uint
    {
        // Default -> populates to all controls when set
        DEFAULT = 0,
        // Basic controls
        LABEL,          // Used also for: LABELBUTTON
        BUTTON,
        TOGGLE,         // Used also for: TOGGLEGROUP
        SLIDER,         // Used also for: SLIDERBAR
        PROGRESSBAR,
        CHECKBOX,
        COMBOBOX,
        DROPDOWNBOX,
        TEXTBOX,        // Used also for: TEXTBOXMULTI
        VALUEBOX,
        SPINNER,        // Uses: BUTTON, VALUEBOX
        LISTVIEW,
        COLORPICKER,
        SCROLLBAR,
        STATUSBAR
    }

    // Gui base properties for every control
    // NOTE: RAYGUI_MAX_PROPS_BASE properties (by default 16 properties)
    enum GuiControlProperty : uint
    {
        BORDER_COLOR_NORMAL = 0,
        BASE_COLOR_NORMAL,
        TEXT_COLOR_NORMAL,
        BORDER_COLOR_FOCUSED,
        BASE_COLOR_FOCUSED,
        TEXT_COLOR_FOCUSED,
        BORDER_COLOR_PRESSED,
        BASE_COLOR_PRESSED,
        TEXT_COLOR_PRESSED,
        BORDER_COLOR_DISABLED,
        BASE_COLOR_DISABLED,
        TEXT_COLOR_DISABLED,
        BORDER_WIDTH,
        TEXT_PADDING,
        TEXT_ALIGNMENT,
        RESERVED
    }

    // Gui extended properties depend on control
    // NOTE: RAYGUI_MAX_PROPS_EXTENDED properties (by default 8 properties)
    //----------------------------------------------------------------------------------

    // DEFAULT extended properties
    // NOTE: Those properties are common to all controls or global
    enum GuiDefaultProperty : uint
    {
        TEXT_SIZE = 16,             // Text size (glyphs max height)
        TEXT_SPACING,               // Text spacing between glyphs
        LINE_COLOR,                 // Line control color
        BACKGROUND_COLOR,           // Background color
    }

    // Label
    //typedef enum { } GuiLabelProperty;

    // Button/Spinner
    //typedef enum { } GuiButtonProperty;

    // Toggle/ToggleGroup
    enum GuiToggleProperty : uint
    {
        GROUP_PADDING = 16,         // ToggleGroup separation between toggles
    }

    // Slider/SliderBar
    enum GuiSliderProperty : uint
    {
        SLIDER_WIDTH = 16,          // Slider size of internal bar
        SLIDER_PADDING              // Slider/SliderBar internal bar padding
    }

    // ProgressBar
    enum GuiProgressBarProperty : uint
    {
        PROGRESS_PADDING = 16,      // ProgressBar internal padding
    }

    // ScrollBar
    enum GuiScrollBarProperty : uint
    {
        ARROWS_SIZE = 16,
        ARROWS_VISIBLE,
        SCROLL_SLIDER_PADDING,      // (SLIDERBAR, SLIDER_PADDING)
        SCROLL_SLIDER_SIZE,
        SCROLL_PADDING,
        SCROLL_SPEED,
    }

    // CheckBox
    enum GuiCheckBoxProperty : uint
    {
        CHECK_PADDING = 16          // CheckBox internal check padding
    }

    // ComboBox
    enum GuiComboBoxProperty : uint
    {
        COMBO_BUTTON_WIDTH = 16,    // ComboBox right button width
        COMBO_BUTTON_SPACING        // ComboBox button separation
    }

    // DropdownBox
    enum GuiDropdownBoxProperty : uint
    {
        ARROW_PADDING = 16,         // DropdownBox arrow separation from border and items
        DROPDOWN_ITEMS_SPACING      // DropdownBox items separation
    }

    // TextBox/TextBoxMulti/ValueBox/Spinner
    enum GuiTextBoxProperty : uint
    {
        TEXT_INNER_PADDING = 16,    // TextBox/TextBoxMulti/ValueBox/Spinner inner text padding
        TEXT_LINES_SPACING,         // TextBoxMulti lines separation
    }

    // Spinner
    enum GuiSpinnerProperty : uint
    {
        SPIN_BUTTON_WIDTH = 16,     // Spinner left/right buttons width
        SPIN_BUTTON_SPACING,        // Spinner buttons separation
    }

    // ListView
    enum GuiListViewProperty : uint
    {
        LIST_ITEMS_HEIGHT = 16,     // ListView items height
        LIST_ITEMS_SPACING,         // ListView items separation
        SCROLLBAR_WIDTH,            // ListView scrollbar size (usually width)
        SCROLLBAR_SIDE,             // ListView scrollbar side (0-left, 1-right)
    }

    // ColorPicker
    enum GuiColorPickerProperty : uint
    {
        COLOR_SELECTOR_SIZE = 16,
        HUEBAR_WIDTH,               // ColorPicker right hue bar width
        HUEBAR_PADDING,             // ColorPicker right hue bar separation from panel
        HUEBAR_SELECTOR_HEIGHT,     // ColorPicker right hue bar selector height
        HUEBAR_SELECTOR_OVERFLOW    // ColorPicker right hue bar selector overflow
    }

    enum GuiPropertyElement : uint { BORDER = 0, BASE, TEXT, OTHER }

    const int SCROLLBAR_LEFT_SIDE = 0;
    const int SCROLLBAR_RIGHT_SIDE = 1;


    const uint RAYGUI_MAX_CONTROLS = 16;      // Maximum number of standard controls
    const uint RAYGUI_MAX_PROPS_BASE = 16;      // Maximum number of standard properties
    const uint RAYGUI_MAX_PROPS_EXTENDED = 8;     // Maximum number of extended properties

    static uint[] guiStyle = new uint[RAYGUI_MAX_CONTROLS * (RAYGUI_MAX_PROPS_BASE + RAYGUI_MAX_PROPS_EXTENDED)];

    static void GuiEnable() { if (guiState == GuiState.STATE_DISABLED) guiState = GuiState.STATE_NORMAL; }

    // Disable gui global state
    // NOTE: We check for STATE_NORMAL to avoid messing custom global state setups
    static void GuiDisable() { if (guiState == GuiState.STATE_NORMAL) guiState = GuiState.STATE_DISABLED; }

    // Lock gui global state
    static void GuiLock() { guiLocked = true; }

    // Unlock gui global state
    static void GuiUnlock() { guiLocked = false; }

    // Check if gui is locked (global state)
    static bool GuiIsLocked() { return guiLocked; }

    // Set gui controls alpha global state
    static void GuiFade(float alpha)
    {
        if (alpha < 0.0f) alpha = 0.0f;
        else if (alpha > 1.0f) alpha = 1.0f;

        guiAlpha = alpha;
    }

    // Set gui state (global state)
    static void GuiSetState(int state) { guiState = (GuiState)state; }

    // Get gui state (global state)
    static int GuiGetState() { return (int)guiState; }

    // Set custom gui font
    // NOTE: Font loading/unloading is external to raygui
    static void GuiSetFont(Font font)
    {
        if (font.texture.id > 0)
        {
            // NOTE: If we try to setup a font but default style has not been
            // lazily loaded before, it will be overwritten, so we need to force
            // default style loading first
            if (!guiStyleLoaded) GuiLoadStyleDefault();

            guiFont = font;
            GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, (uint)font.baseSize);
        }
    }

    // Get custom gui font
    static Font GuiGetFont()
    {
        return guiFont;
    }

    // Set control style property value
    static void GuiSetStyle(uint control, uint property, uint value)
    {
        if (!guiStyleLoaded) GuiLoadStyleDefault();
        guiStyle[control * (RAYGUI_MAX_PROPS_BASE + RAYGUI_MAX_PROPS_EXTENDED) + property] = value;

        // Default properties are propagated to all controls
        if ((control == 0) && (property < RAYGUI_MAX_PROPS_BASE))
        {
            for (int i = 1; i < RAYGUI_MAX_CONTROLS; i++) guiStyle[i * (RAYGUI_MAX_PROPS_BASE + RAYGUI_MAX_PROPS_EXTENDED) + property] = value;
        }
    }

    // Get control style property value
    static uint GuiGetStyle(uint control, uint property)
    {
        if (!guiStyleLoaded) GuiLoadStyleDefault();
        return guiStyle[control * (RAYGUI_MAX_PROPS_BASE + RAYGUI_MAX_PROPS_EXTENDED) + property];
    }

    // Label control
    public static void GuiLabel(Rectangle bounds, string text)
    {
        GuiState state = guiState;

        // Draw control
        //--------------------------------------------------------------------
        var textColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.LABEL, (uint)GuiPropertyElement.TEXT + ((uint)state * 3))), guiAlpha));

        GuiDrawText(text, GetTextBounds((uint)GuiControl.LABEL, bounds), (int)GuiGetStyle((uint)GuiControl.LABEL, (uint)GuiControlProperty.TEXT_ALIGNMENT), textColor);
        //--------------------------------------------------------------------
    }

    const int RAYGUI_PANEL_BORDER_WIDTH = 1;
    const int RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT = 24;
    public static void GuiPanel(Rectangle bounds, string text)
    {
        GuiState state = guiState;

        // Text will be drawn as a header bar (if provided)
        Rectangle statusBar = new Rectangle(bounds.x, bounds.y, bounds.width, (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT);
        if (!string.IsNullOrWhiteSpace(text) && (bounds.height < RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2.0f)) bounds.height = RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2.0f;

        if (!string.IsNullOrWhiteSpace(text))
        {
            // Move panel bounds after the header bar
            bounds.y += (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT - 1;
            bounds.height -= (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT + 1;
        }

        // Draw control
        //--------------------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(text)) GuiStatusBar(statusBar, text);  // Draw panel header as status bar

        var borderColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.DEFAULT, (state == GuiState.STATE_DISABLED) ? (uint)GuiControlProperty.BORDER_COLOR_DISABLED : (uint)GuiDefaultProperty.LINE_COLOR)), guiAlpha));
        var bgColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.DEFAULT, (state == GuiState.STATE_DISABLED) ? (uint)GuiControlProperty.BASE_COLOR_DISABLED : (uint)GuiDefaultProperty.BACKGROUND_COLOR)), guiAlpha));

        GuiDrawRectangle(bounds, RAYGUI_PANEL_BORDER_WIDTH, borderColor, bgColor);
        //--------------------------------------------------------------------
    }

    // Status Bar control
    public static void GuiStatusBar(Rectangle bounds, string text)
    {
        GuiState state = guiState;

        // Draw control
        //--------------------------------------------------------------------
        var borderColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.STATUSBAR, (state != GuiState.STATE_DISABLED) ? (uint)GuiControlProperty.BORDER_COLOR_NORMAL : (uint)GuiControlProperty.BORDER_COLOR_DISABLED)), guiAlpha));
        var baseColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.STATUSBAR, (state != GuiState.STATE_DISABLED) ? (uint)GuiControlProperty.BASE_COLOR_NORMAL : (uint)GuiControlProperty.BASE_COLOR_DISABLED)), guiAlpha));

        GuiDrawRectangle(bounds, (int)GuiGetStyle((uint)GuiControl.STATUSBAR, (uint)GuiControlProperty.BORDER_WIDTH), borderColor, baseColor);

        var textColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.STATUSBAR, (state != GuiState.STATE_DISABLED) ? (uint)GuiControlProperty.TEXT_COLOR_NORMAL : (uint)GuiControlProperty.TEXT_COLOR_DISABLED)), guiAlpha));

        GuiDrawText(text, GetTextBounds((uint)GuiControl.STATUSBAR, bounds), (int)GuiGetStyle((uint)GuiControl.STATUSBAR, (uint)GuiControlProperty.TEXT_ALIGNMENT), textColor);
        //--------------------------------------------------------------------
    }

    // Button control, returns true when clicked
    public static bool GuiButton(Rectangle bounds, string text)
    {
        GuiState state = guiState;
        bool pressed = false;

        // Update control
        //--------------------------------------------------------------------
        if ((state != GuiState.STATE_DISABLED) && !guiLocked)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            // Check button state
            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON)) state = GuiState.STATE_PRESSED;
                else state = GuiState.STATE_FOCUSED;

                if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON)) pressed = true;
            }
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        var baseColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.BUTTON, (uint)GuiPropertyElement.BASE + ((uint)state * 3))), guiAlpha));
        var textColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.BUTTON, (uint)GuiPropertyElement.TEXT + ((uint)state * 3))), guiAlpha));
        var btnColor = new Color(Raylib.Fade(Raylib.GetColor(GuiGetStyle((uint)GuiControl.BUTTON, (uint)GuiPropertyElement.BORDER + ((uint)state * 3))), guiAlpha));

        GuiDrawRectangle(bounds, (int)GuiGetStyle((uint)GuiControl.BUTTON, (uint)GuiControlProperty.BORDER_WIDTH), btnColor, baseColor);
        GuiDrawText(text, GetTextBounds((uint)GuiControl.BUTTON, bounds), (int)GuiGetStyle((uint)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_ALIGNMENT), textColor);
        //------------------------------------------------------------------

        return pressed;
    }

    int TEXT_VALIGN_PIXEL_OFFSET(float h) => ((int)h % 2);

    const int ICON_TEXT_PADDING = 4;
    static void GuiDrawText(string text, Rectangle bounds, int alignment, Color tint)
    {
        var position = new Vector2(bounds.x, bounds.y);

        Raylib.DrawTextEx(guiFont, text, position, (float)GuiGetStyle((uint)GuiControl.DEFAULT, (uint)GuiDefaultProperty.TEXT_SIZE), (float)GuiGetStyle((uint)GuiControl.DEFAULT, (uint)GuiDefaultProperty.TEXT_SPACING), tint);
    }

    static void GuiDrawRectangle(Rectangle rec, int borderWidth, Color borderColor, Color color)
    {
        if (color.A > 0)
        {
            // Draw rectangle filled with color
            Raylib.DrawRectangle((int)rec.x, (int)rec.y, (int)rec.width, (int)rec.height, color);
        }

        if (borderWidth > 0)
        {
            // Draw rectangle border lines with color
            Raylib.DrawRectangle((int)rec.x, (int)rec.y, (int)rec.width, borderWidth, borderColor);
            Raylib.DrawRectangle((int)rec.x, (int)rec.y + borderWidth, borderWidth, (int)rec.height - 2 * borderWidth, borderColor);
            Raylib.DrawRectangle((int)rec.x + (int)rec.width - borderWidth, (int)rec.y + borderWidth, borderWidth, (int)rec.height - 2 * borderWidth, borderColor);
            Raylib.DrawRectangle((int)rec.x, (int)rec.y + (int)rec.height - borderWidth, (int)rec.width, borderWidth, borderColor);
        }
    }

    static void GuiLoadStyleDefault()
    {
        // We set this variable first to avoid cyclic function calls
        // when calling GuiSetStyle() and GuiGetStyle()
        guiStyleLoaded = true;

        // Initialize default LIGHT style property values
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_NORMAL, 0x838383ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_NORMAL, 0xc9c9c9ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_NORMAL, 0x686868ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_FOCUSED, 0x5bb2d9ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_FOCUSED, 0xc9effeff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_FOCUSED, 0x6c9bbcff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_PRESSED, 0x0492c7ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_PRESSED, 0x97e8ffff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_PRESSED, 0x368bafff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_COLOR_DISABLED, 0xb5c1c2ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BASE_COLOR_DISABLED, 0xe6e9e9ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_COLOR_DISABLED, 0xaeb7b8ff);
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.BORDER_WIDTH, 1);                       // WARNING: Some controls use other values
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_PADDING, 0);                       // WARNING: Some controls use other values
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_CENTER); // WARNING: Some controls use other values

        // Initialize control-specific property values
        // NOTE: Those properties are in default list but require specific values by control type
        GuiSetStyle((int)GuiControl.LABEL, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_LEFT);
        GuiSetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.BORDER_WIDTH, 2);
        GuiSetStyle((int)GuiControl.SLIDER, (int)GuiControlProperty.TEXT_PADDING, 4);
        GuiSetStyle((int)GuiControl.CHECKBOX, (int)GuiControlProperty.TEXT_PADDING, 4);
        GuiSetStyle((int)GuiControl.CHECKBOX, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_RIGHT);
        GuiSetStyle((int)GuiControl.TEXTBOX, (int)GuiControlProperty.TEXT_PADDING, 4);
        GuiSetStyle((int)GuiControl.TEXTBOX, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_LEFT);
        GuiSetStyle((int)GuiControl.VALUEBOX, (int)GuiControlProperty.TEXT_PADDING, 4);
        GuiSetStyle((int)GuiControl.VALUEBOX, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_LEFT);
        GuiSetStyle((int)GuiControl.SPINNER, (int)GuiControlProperty.TEXT_PADDING, 4);
        GuiSetStyle((int)GuiControl.SPINNER, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_LEFT);
        GuiSetStyle((int)GuiControl.STATUSBAR, (int)GuiControlProperty.TEXT_PADDING, 8);
        GuiSetStyle((int)GuiControl.STATUSBAR, (int)GuiControlProperty.TEXT_ALIGNMENT, (int)GuiTextAlignment.TEXT_ALIGN_LEFT);

        // Initialize extended property values
        // NOTE: By default, extended property values are initialized to 0
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 24);                // DEFAULT, shared by all controls, 10
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SPACING, 1);              // DEFAULT, shared by all controls
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.LINE_COLOR, 0x90abb5ff);       // DEFAULT specific property
        GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.BACKGROUND_COLOR, 0xf5f5f5ff); // DEFAULT specific property
        GuiSetStyle((int)GuiControl.TOGGLE, (int)GuiToggleProperty.GROUP_PADDING, 2);
        GuiSetStyle((int)GuiControl.SLIDER, (int)GuiSliderProperty.SLIDER_WIDTH, 16);
        GuiSetStyle((int)GuiControl.SLIDER, (int)GuiSliderProperty.SLIDER_PADDING, 1);
        GuiSetStyle((int)GuiControl.PROGRESSBAR, (int)GuiProgressBarProperty.PROGRESS_PADDING, 1);
        GuiSetStyle((int)GuiControl.CHECKBOX, (int)GuiCheckBoxProperty.CHECK_PADDING, 1);
        GuiSetStyle((int)GuiControl.COMBOBOX, (int)GuiComboBoxProperty.COMBO_BUTTON_WIDTH, 32);
        GuiSetStyle((int)GuiControl.COMBOBOX, (int)GuiComboBoxProperty.COMBO_BUTTON_SPACING, 2);
        GuiSetStyle((int)GuiControl.DROPDOWNBOX, (int)GuiDropdownBoxProperty.ARROW_PADDING, 16);
        GuiSetStyle((int)GuiControl.DROPDOWNBOX, (int)GuiDropdownBoxProperty.DROPDOWN_ITEMS_SPACING, 2);
        GuiSetStyle((int)GuiControl.TEXTBOX, (int)GuiTextBoxProperty.TEXT_LINES_SPACING, 4);
        GuiSetStyle((int)GuiControl.TEXTBOX, (int)GuiTextBoxProperty.TEXT_INNER_PADDING, 4);
        GuiSetStyle((int)GuiControl.SPINNER, (int)GuiSpinnerProperty.SPIN_BUTTON_WIDTH, 24);
        GuiSetStyle((int)GuiControl.SPINNER, (int)GuiSpinnerProperty.SPIN_BUTTON_SPACING, 2);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiControlProperty.BORDER_WIDTH, 0);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.ARROWS_VISIBLE, 0);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.ARROWS_SIZE, 6);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.SCROLL_SLIDER_PADDING, 0);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.SCROLL_SLIDER_SIZE, 16);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.SCROLL_PADDING, 0);
        GuiSetStyle((int)GuiControl.SCROLLBAR, (int)GuiScrollBarProperty.SCROLL_SPEED, 12);
        GuiSetStyle((int)GuiControl.LISTVIEW, (int)GuiListViewProperty.LIST_ITEMS_HEIGHT, 24);
        GuiSetStyle((int)GuiControl.LISTVIEW, (int)GuiListViewProperty.LIST_ITEMS_SPACING, 2);
        GuiSetStyle((int)GuiControl.LISTVIEW, (int)GuiListViewProperty.SCROLLBAR_WIDTH, 12);
        GuiSetStyle((int)GuiControl.LISTVIEW, (int)GuiListViewProperty.SCROLLBAR_SIDE, SCROLLBAR_RIGHT_SIDE);
        GuiSetStyle((int)GuiControl.COLORPICKER, (int)GuiColorPickerProperty.COLOR_SELECTOR_SIZE, 8);
        GuiSetStyle((int)GuiControl.COLORPICKER, (int)GuiColorPickerProperty.HUEBAR_WIDTH, 16);
        GuiSetStyle((int)GuiControl.COLORPICKER, (int)GuiColorPickerProperty.HUEBAR_PADDING, 8);
        GuiSetStyle((int)GuiControl.COLORPICKER, (int)GuiColorPickerProperty.HUEBAR_SELECTOR_HEIGHT, 8);
        GuiSetStyle((int)GuiControl.COLORPICKER, (int)GuiColorPickerProperty.HUEBAR_SELECTOR_OVERFLOW, 2);

        guiFont = Raylib.GetFontDefault();     // Initialize default font
    }

    static Rectangle GetTextBounds(uint control, Rectangle bounds)
    {
        Rectangle textBounds = bounds;

        textBounds.x = bounds.x + GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
        textBounds.y = bounds.y + GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
        textBounds.width = bounds.width - 2 * GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);
        textBounds.height = bounds.height - 2 * GuiGetStyle(control, (int)GuiControlProperty.BORDER_WIDTH);

        // Consider TEXT_PADDING properly, depends on control type and TEXT_ALIGNMENT
        switch (control)
        {
            case (int)GuiControl.COMBOBOX: bounds.width -= (GuiGetStyle(control, (int)GuiComboBoxProperty.COMBO_BUTTON_WIDTH) + GuiGetStyle(control, (int)GuiComboBoxProperty.COMBO_BUTTON_SPACING)); break;
            case (int)GuiControl.VALUEBOX: break;   // NOTE: ValueBox text value always centered, text padding applies to label
            default:
                {
                    if (GuiGetStyle(control, (int)GuiControlProperty.TEXT_ALIGNMENT) == (int)GuiTextAlignment.TEXT_ALIGN_RIGHT) textBounds.x -= GuiGetStyle(control, (int)GuiControlProperty.TEXT_PADDING);
                    else textBounds.x += GuiGetStyle(control, (int)GuiControlProperty.TEXT_PADDING);
                    textBounds.width -= 2 * GuiGetStyle(control, (int)GuiControlProperty.TEXT_PADDING);
                }
                break;
        }

        return textBounds;
    }

}
