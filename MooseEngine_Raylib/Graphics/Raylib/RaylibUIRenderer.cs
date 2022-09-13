using MooseEngine.Core;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

internal class RaylibUIRenderer : IUIRenderer
{
    enum GuiState : uint
    {
        STATE_NORMAL = 0,
        STATE_HOVERED,
        STATE_PRESSED,
        STATE_DISABLED,
    }

    GuiState _guiState = GuiState.STATE_NORMAL;

    public RaylibUIRenderer(IWindow windowData)
    {
        WindowData = windowData;
    }

    public void Initialize()
    {
        UIRendererOptions = new RaylibUIRendererOptions(Constants.DEFAULT_FONT_SIZE);
    }

    public IRaylibUIRendererOptions UIRendererOptions { get; internal set; }
    public IWindowData WindowData { get; }

    public void DrawFPS(int x, int y)
    {
        Raylib.DrawFPS(x, y);
    }

    public void DrawLabel(LabelOptions labelOptions)
    {
        // Draw control
        //--------------------------------------------------------------------
        var textBounds = labelOptions.GetTextBounds();
        GuiDrawText(labelOptions.Text, textBounds, GetTextColorByState(_guiState));
        //--------------------------------------------------------------------
    }

    public bool DrawButton(ButtonOptions buttonOptions)
    {
        var state = _guiState;

        var bounds = buttonOptions.GetBounds();
        var textBounds = buttonOptions.GetTextBounds();

        bool pressed = false;

        // Update control
        //--------------------------------------------------------------------
        if (state != GuiState.STATE_DISABLED)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            // Check button state
            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON)) state = GuiState.STATE_PRESSED;
                else state = GuiState.STATE_HOVERED;

                if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON)) pressed = true;
            }
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, buttonOptions.BorderWidth, GetBorderColorByState(state), GetBaseColorByState(state));
        GuiDrawText(buttonOptions.Text, textBounds, GetTextColorByState(state));
        //--------------------------------------------------------------------

        return pressed;
    }

    public float DrawSliderBar(SliderOptions sliderOptions, float value)
    {
        var state = _guiState;
        var sliderRectangle = sliderOptions.GetSliderRectangle(value);
        var bounds = sliderOptions.GetBounds();

        // Update control
        //--------------------------------------------------------------------
        if (sliderOptions.Interactable)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    state = GuiState.STATE_PRESSED;

                    // Get equivalent value and slider position from mousePoint.x
                    value = (int)(((sliderOptions.MaxValue - sliderOptions.MinValue) * (mousePoint.X - (bounds.x / 2.0f)) / bounds.width) + sliderOptions.MinValue);

                    sliderRectangle.width = value;
                }
                else state = GuiState.STATE_HOVERED;
            }
        }
        else
        {
            sliderRectangle.width = value;
        }
        value = sliderOptions.ClampValue(value);

        if (sliderRectangle.width > bounds.width)
        {
            sliderRectangle.width = bounds.width - 2 * sliderOptions.BorderWidth;
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, sliderOptions.BorderWidth, GetBorderColorByState(state), GetBaseColorByState(state));
        //var textColor = sliderOptions.TextNormalColor; 

        // Draw slider internal bar (depends on state)
        if (!sliderOptions.Interactable)
        {
            sliderRectangle.width = MathFunctions.Lerp(sliderOptions.MinValue, bounds.width, value) / sliderOptions.MaxValue;
        }
        GuiDrawRectangle(sliderRectangle, 0, Color.Blank, state == GuiState.STATE_HOVERED ? sliderOptions.TextFocusedColor : sliderOptions.Color);

        if (sliderOptions.TextAlignment != TextAlignment.None)
        {
            var textBounds = sliderOptions.GetTextBounds();
            GuiDrawText(sliderOptions.Text, textBounds, GetTextColorByState(state));
        }
        //--------------------------------------------------------------------

        return value;
    }

    public void DrawPanel(PanelOptions panelOptions)
    {
        var state = _guiState;
        var colors = UIRendererOptions.Colors;

        var bounds = panelOptions.GetBounds();
        var statusBarRectangle = panelOptions.GetStatusBarRectangle();

        // Text will be drawn as a header bar (if provided)
        if (!string.IsNullOrWhiteSpace(panelOptions.Title) && (bounds.height < panelOptions.StatusBarHeight * 2.0f))
        {
            bounds.height = panelOptions.StatusBarHeight * 2;
        }

        if (!string.IsNullOrWhiteSpace(panelOptions.Title))
        {
            // Move panel bounds after the header bar
            bounds.y += panelOptions.StatusBarHeight - 1;
            bounds.height -= panelOptions.StatusBarHeight + 1;
        }

        // Draw control
        //--------------------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(panelOptions.Title))
        {
            GuiStatusBar(statusBarRectangle, panelOptions.Title);  // Draw panel header as status bar
        }

        var lineColor = state == GuiState.STATE_DISABLED ? colors.BorderDisabledColor : colors.LineColor;
        var backgroundColor = state == GuiState.STATE_DISABLED ? colors.DisabledColor : colors.BackgroundColor;

        GuiDrawRectangle(bounds, panelOptions.BorderWidth, lineColor, backgroundColor);
        //--------------------------------------------------------------------
    }

    public void DrawImage(ImageOptions imageOptions)
    {
        var source = imageOptions.GetImageSource();
        var dest = imageOptions.GetImageDestination();
        Raylib.DrawTexturePro(imageOptions.Image, source, dest, Vector2.Zero, 0.0f, Color.White);

        //var size = 192;
        //var dest = new Rectangle(1200 + (400 - size - 10), 25, size, size);
    }

    public int DrawListViewEx(ListViewOptions listViewOptions, IEnumerable<string> items, ref int focus, ref int scrollIndex, int active)
    {
        var count = items.Count();

        var state = _guiState;
        int itemFocused = (focus == 0) ? -1 : focus;
        int itemSelected = active;

        var statusBarRectangle = listViewOptions.GetStatusBarRectangle();
        var bounds = listViewOptions.GetBounds();

        var useScrollBar = (listViewOptions.ItemHeight + listViewOptions.ItemSpacing) * count > bounds.height;

        if (!string.IsNullOrWhiteSpace(listViewOptions.Title) && (bounds.height < listViewOptions.StatusBarHeight * 2.0f))
        {
            bounds.height = listViewOptions.StatusBarHeight * 2;
        }

        if (!string.IsNullOrWhiteSpace(listViewOptions.Title))
        {
            // Move panel bounds after the header bar
            bounds.y += listViewOptions.StatusBarHeight - 1;
            bounds.height -= listViewOptions.StatusBarHeight + 1;
        }

        var itemBounds = listViewOptions.GetItemBounds(bounds);

        if (useScrollBar)
        {
            itemBounds.width -= listViewOptions.ScrollbarWidth;
        }

        int visibleItems = (int)bounds.height / (listViewOptions.ItemHeight + listViewOptions.ItemSpacing);
        if (visibleItems > count)
        {
            visibleItems = count;
        }

        int startIndex = (scrollIndex == 0) ? 0 : scrollIndex;
        if ((startIndex < 0) || (startIndex > (count - visibleItems)))
        {
            startIndex = 0;
        }
        int endIndex = startIndex + visibleItems;

        // Update control
        //--------------------------------------------------------------------
        if (listViewOptions.Interactable)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            // Check mouse inside list view
            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                state = GuiState.STATE_HOVERED;

                // Check focused and selected item
                for (int i = 0; i < visibleItems; i++)
                {
                    if (Raylib.CheckCollisionPointRec(mousePoint, itemBounds))
                    {
                        itemFocused = startIndex + i;
                        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                        {
                            if (itemSelected == (startIndex + i)) itemSelected = -1;
                            else itemSelected = startIndex + i;
                        }
                        break;
                    }

                    // Update item rectangle y position for next item
                    itemBounds.y += listViewOptions.ItemHeight + listViewOptions.ItemSpacing;
                }

                if (useScrollBar)
                {
                    int wheelMove = (int)Raylib.GetMouseWheelMove();
                    startIndex -= wheelMove;

                    if (startIndex < 0)
                    {
                        startIndex = 0;
                    }
                    else if (startIndex > (count - visibleItems))
                    {
                        startIndex = count - visibleItems;
                    }

                    endIndex = startIndex + visibleItems;
                    if (endIndex > count) endIndex = count;
                }
            }
            else itemFocused = -1;

            // Reset item rectangle y to [0]
            itemBounds.y = bounds.y + listViewOptions.ItemSpacing + listViewOptions.BorderWidth;
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(listViewOptions.Title))
        {
            GuiStatusBar(statusBarRectangle, listViewOptions.Title);  // Draw panel header as status bar
        }

        GuiDrawRectangle(bounds, listViewOptions.BorderWidth, GetBorderColorByState(state), listViewOptions.BackgroundColor);     // Draw background

        // Draw visible items
        for (int i = 0; ((i < visibleItems) && (count > 0)); i++)
        {
            var item = items.ElementAt(startIndex + i);

            if (state == GuiState.STATE_DISABLED)
            {
                if ((startIndex + i) == itemSelected)
                {
                    GuiDrawRectangle(itemBounds, listViewOptions.BorderWidth, listViewOptions.BorderDisabledColor, listViewOptions.DisabledColor);
                }

                GuiDrawText(item, GetTextBounds(itemBounds), listViewOptions.TextDisabledColor);
            }
            else
            {
                if ((startIndex + i) == itemSelected)
                {
                    // Draw item selected
                    GuiDrawRectangle(itemBounds, listViewOptions.BorderWidth, listViewOptions.BorderPressedColor, listViewOptions.PressedColor);
                    GuiDrawText(item, GetTextBounds(itemBounds), listViewOptions.TextPressedColor);
                }
                else if ((startIndex + i) == itemFocused)
                {
                    // Draw item focused
                    GuiDrawRectangle(itemBounds, listViewOptions.BorderWidth, listViewOptions.BorderFocusedColor, listViewOptions.FocusedColor);
                    GuiDrawText(item, GetTextBounds(itemBounds), listViewOptions.TextFocusedColor);
                }
                else
                {
                    // Draw item normal
                    GuiDrawText(item, GetTextBounds(itemBounds), listViewOptions.TextNormalColor);
                }
            }

            // Update item rectangle y position for next item
            itemBounds.y += (listViewOptions.ItemHeight + listViewOptions.ItemSpacing);
        }

        if (useScrollBar)
        {
            var scrollBarBounds = new Rectangle
            {
                x = bounds.x + bounds.width - listViewOptions.BorderWidth - listViewOptions.ScrollbarWidth,
                y = bounds.y + listViewOptions.BorderWidth,
                width = (float)listViewOptions.ScrollbarWidth,
                height = bounds.height - 2 * listViewOptions.BorderWidth
            };

            // Calculate percentage of visible items and apply same percentage to scrollbar
            float percentVisible = (float)(endIndex - startIndex) / count;
            float sliderSize = bounds.height * percentVisible;

            int prevSliderSize = listViewOptions.ScrollSliderSize;   // Save default slider size
            int prevScrollSpeed = listViewOptions.ScrollSpeed; // Save default scroll speed
            listViewOptions.ScrollSliderSize = (int)sliderSize; // Change slider size
            listViewOptions.ScrollSpeed = count - visibleItems; // Change scroll speed

            //if (stickToBottom)
            //{
            //    endIndex = GuiScrollBar(scrollBarBounds, endIndex, 0, count - visibleItems);
            //}
            //else
            //{
            //}
            startIndex = GuiScrollBar(scrollBarBounds, startIndex, 0, count - visibleItems);

            listViewOptions.ScrollSpeed = prevScrollSpeed; // Reset scroll speed to default
            listViewOptions.ScrollSliderSize = prevSliderSize; // Reset slider size to default
        }
        //--------------------------------------------------------------------

        if (focus != 0) focus = itemFocused;
        if (scrollIndex >= 0) scrollIndex = startIndex;

        return itemSelected;
    }

    public void DrawSeperator(SeperatorOptions seperatorOptions)
    {
        var bounds = seperatorOptions.GetBounds();

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, 0, Color.Blank, seperatorOptions.LineColor);
        //--------------------------------------------------------------------
    }

    // Scroll bar control (used by GuiScrollPanel())
    private int GuiScrollBar(Rectangle bounds, int value, int minValue, int maxValue)
    {
        var state = _guiState;
        var colors = UIRendererOptions.Colors;
        var scrollbarOptions = UIRendererOptions.ScrollbarOptions;

        // Is the scrollbar horizontal or vertical?
        bool isVertical = (bounds.width > bounds.height) ? false : true;

        // The size (width or height depending on scrollbar type) of the spinner buttons
        int spinnerSize = scrollbarOptions.ArrowsVisible
            ? (isVertical
                ? (int)bounds.width - 2 * scrollbarOptions.BorderWidth
                : (int)bounds.height - 2 * scrollbarOptions.BorderWidth)
            : 0;

        // Arrow buttons [<] [>] [∧] [∨]
        Rectangle arrowUpLeft = new Rectangle(0, 0, 0, 0);
        Rectangle arrowDownRight = new Rectangle(0, 0, 0, 0);

        // Actual area of the scrollbar excluding the arrow buttons
        Rectangle scrollbar = new Rectangle(0, 0, 0, 0);

        // Slider bar that moves     --[///]-----
        Rectangle slider = new Rectangle(0, 0, 0, 0);

        // Normalize value
        if (value > maxValue) value = maxValue;
        if (value < minValue) value = minValue;

        int range = maxValue - minValue;
        int sliderSize = scrollbarOptions.ScrollSliderSize;

        // Calculate rectangles for all of the components
        arrowUpLeft = new Rectangle(
            (float)bounds.x + scrollbarOptions.BorderWidth,
            (float)bounds.y + scrollbarOptions.BorderWidth,
            (float)spinnerSize, (float)spinnerSize);

        if (isVertical)
        {
            arrowDownRight = new Rectangle(
                (float)bounds.x + scrollbarOptions.BorderWidth,
                (float)bounds.y + bounds.height - spinnerSize - scrollbarOptions.BorderWidth,
                (float)spinnerSize, (float)spinnerSize);

            scrollbar = new Rectangle(
                bounds.x + scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding,
                arrowUpLeft.y + arrowUpLeft.height,
                bounds.width - 2 * (scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding),
                bounds.height - arrowUpLeft.height - arrowDownRight.height - 2 * scrollbarOptions.BorderWidth);

            sliderSize = (sliderSize >= scrollbar.height) ? ((int)scrollbar.height - 2) : sliderSize;     // Make sure the slider won't get outside of the scrollbar
            slider = new Rectangle(
                (float)bounds.x + scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding,
                (float)scrollbar.y + (int)(((float)(value - minValue) / range) * (scrollbar.height - sliderSize)),
                (float)bounds.width - 2 * (scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding),
                (float)sliderSize);
        }
        else
        {
            arrowDownRight = new Rectangle(
                (float)bounds.x + bounds.width - spinnerSize - scrollbarOptions.BorderWidth,
                (float)bounds.y + scrollbarOptions.BorderWidth,
                (float)spinnerSize, (float)spinnerSize);

            scrollbar = new Rectangle(
                arrowUpLeft.x + arrowUpLeft.width,
                bounds.y + scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding,
                bounds.width - arrowUpLeft.width - arrowDownRight.width - 2 * scrollbarOptions.BorderWidth,
                bounds.height - 2 * (scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding));

            sliderSize = (sliderSize >= scrollbar.width) ? ((int)scrollbar.width - 2) : sliderSize;       // Make sure the slider won't get outside of the scrollbar
            slider = new Rectangle(
                (float)scrollbar.x + (int)(((float)(value - minValue) / range) * (scrollbar.width - sliderSize)),
                (float)bounds.y + scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding,
                (float)sliderSize,
                (float)bounds.height - 2 * (scrollbarOptions.BorderWidth + scrollbarOptions.ScrollPadding));
        }

        // Update control
        //--------------------------------------------------------------------
        if ((state != GuiState.STATE_DISABLED))
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                state = GuiState.STATE_HOVERED;

                // Handle mouse wheel
                int wheel = (int)Raylib.GetMouseWheelMove();
                if (wheel != 0) value += wheel;

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePoint, arrowUpLeft)) value -= range / scrollbarOptions.ScrollSpeed;
                    else if (Raylib.CheckCollisionPointRec(mousePoint, arrowDownRight)) value += range / scrollbarOptions.ScrollSpeed;

                    state = GuiState.STATE_PRESSED;
                }
                else if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    if (!isVertical)
                    {
                        Rectangle scrollArea = new Rectangle(
                            arrowUpLeft.x + arrowUpLeft.width,
                            arrowUpLeft.y,
                            scrollbar.width,
                            bounds.height - 2 * scrollbarOptions.BorderWidth);
                        if (Raylib.CheckCollisionPointRec(mousePoint, scrollArea))
                        {
                            value = (int)(((float)(mousePoint.X - scrollArea.x - slider.width / 2) * range) / (scrollArea.width - slider.width) + minValue);
                        }
                    }
                    else
                    {
                        Rectangle scrollArea = new Rectangle(
                            arrowUpLeft.x,
                            arrowUpLeft.y + arrowUpLeft.height,
                            bounds.width - 2 * scrollbarOptions.BorderWidth,
                            scrollbar.height);
                        if (Raylib.CheckCollisionPointRec(mousePoint, scrollArea))
                        {
                            value = (int)(((float)(mousePoint.Y - scrollArea.y - slider.height / 2) * range) / (scrollArea.height - slider.height) + minValue);
                        }
                    }
                }
            }

            // Normalize value
            if (value > maxValue) value = maxValue;
            if (value < minValue) value = minValue;
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, scrollbarOptions.BorderWidth, GetBorderColorByState(state), colors.BorderDisabledColor);   // Draw the background

        GuiDrawRectangle(scrollbar, 0, Color.Blank, colors.NormalColor);     // Draw the scrollbar active area background
        GuiDrawRectangle(slider, 0, Color.Blank, GetBorderColorByState(state));         // Draw the slider bar

        // Draw arrows (using icon if available)
        if (scrollbarOptions.ArrowsVisible)
        {
            var r1 = new Rectangle(arrowUpLeft.x, arrowUpLeft.y, isVertical ? bounds.width : bounds.height, isVertical ? bounds.width : bounds.height);
            var r2 = new Rectangle(arrowDownRight.x, arrowDownRight.y, isVertical ? bounds.width : bounds.height, isVertical ? bounds.width : bounds.height);

            GuiDrawText(isVertical ? "^" : "<", r1, GetTextColorByState(state));
            GuiDrawText(isVertical ? "v" : ">", r2, GetTextColorByState(state));
        }
        //--------------------------------------------------------------------

        return value;
    }

    // Status Bar control
    private void GuiStatusBar(Rectangle bounds, string text)
    {
        var state = _guiState;
        var colors = UIRendererOptions.Colors;

        // Draw control
        //--------------------------------------------------------------------
        var borderColor = state == GuiState.STATE_DISABLED ? colors.BorderDisabledColor : colors.BorderNormalColor;
        var baseColor = state == GuiState.STATE_DISABLED ? colors.DisabledColor : colors.NormalColor;
        var textColor = state == GuiState.STATE_DISABLED ? colors.TextDisabledColor : colors.TextNormalColor;

        GuiDrawRectangle(bounds, UIRendererOptions.BorderWidth, borderColor, baseColor);
        GuiDrawText(text, GetTextBounds(bounds), textColor);
        //--------------------------------------------------------------------
    }

    private void GuiDrawText(string text, Rectangle bounds, Color tint)
    {
        var position = new Vector2(bounds.x, bounds.y);
        var font = UIRendererOptions.Font;
        //var font = Raylib.GetFontDefault();

        Raylib.DrawTextEx(font, text, position, UIRendererOptions.FontSize, UIRendererOptions.TextSpacing, tint);
    }

    private void GuiDrawRectangle(Rectangle rec, int borderWidth, Color borderColor, Color color)
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

    private Color GetBaseColorByState(GuiState state)
    {
        var colors = UIRendererOptions.Colors;
        return state switch
        {
            GuiState.STATE_HOVERED => colors.FocusedColor,
            GuiState.STATE_PRESSED => colors.PressedColor,
            GuiState.STATE_DISABLED => colors.DisabledColor,
            _ => colors.NormalColor,
        };
    }

    private Color GetBorderColorByState(GuiState state)
    {
        var colors = UIRendererOptions.Colors;
        return state switch
        {
            GuiState.STATE_HOVERED => colors.BorderFocusedColor,
            GuiState.STATE_PRESSED => colors.BorderPressedColor,
            GuiState.STATE_DISABLED => colors.BorderDisabledColor,
            _ => colors.BorderNormalColor,
        };
    }

    private Color GetTextColorByState(GuiState state)
    {
        var colors = UIRendererOptions.Colors;
        return state switch
        {
            GuiState.STATE_HOVERED => colors.TextFocusedColor,
            GuiState.STATE_PRESSED => colors.TextPressedColor,
            GuiState.STATE_DISABLED => colors.TextDisabledColor,
            _ => colors.TextNormalColor,
        };
    }

    private Rectangle GetTextBounds(Rectangle bounds)
    {
        Rectangle textBounds = bounds;

        textBounds.x = bounds.x + UIRendererOptions.BorderWidth;
        textBounds.y = bounds.y + UIRendererOptions.BorderWidth;
        textBounds.width = bounds.width - 2 * UIRendererOptions.BorderWidth;
        textBounds.height = bounds.height - 2 * UIRendererOptions.BorderWidth;

        if (UIRendererOptions.StatusBarTextAlignment == TextAlignment.Right)
        {
            textBounds.x -= UIRendererOptions.TextPadding;
        }
        else
        {
            textBounds.x += UIRendererOptions.TextPadding;
        }
        textBounds.width -= 2 * UIRendererOptions.TextPadding;

        return textBounds;
    }
}
