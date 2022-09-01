﻿using MooseEngine.Core;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

internal class RaylibUIRenderer : IUIRenderer
{
    enum GuiState : uint
    {
        STATE_NORMAL = 0,
        STATE_FOCUSED,
        STATE_PRESSED,
        STATE_DISABLED,
    }

    GuiState state;

    public RaylibUIRenderer(IWindow windowData)
    {
        UIRendererOptions = new RaylibUIRendererOptions(24);
        WindowData = windowData;
    }

    public IRaylibUIRendererOptions UIRendererOptions { get; }
    public IWindowData WindowData { get; }

    public void DrawFPS(int x, int y)
    {
        Raylib.DrawFPS(x, y);
    }

    public void DrawText(string text, int x, int y, int fontScale, Color background, Color fontColor)
    {
        var textWidth = Raylib.MeasureText(text, fontScale);

        Raylib.DrawRectangle(x - 4, y, textWidth + 8, fontScale + 4, background);
        Raylib.DrawText(text, x, y, fontScale, fontColor);
    }

    public bool DrawButton(int x, int y, string text, bool enabled = true)
    {
        var bounds = new Rectangle(x, y, 0, 0);

        bool pressed = false;

        // Update control
        //--------------------------------------------------------------------
        if (enabled)
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
        GuiDrawRectangle(bounds, UIRendererOptions.BorderWidth, UIRendererOptions.BorderColor, UIRendererOptions.BaseColor);
        GuiDrawText(text, GetTextBounds(bounds), UIRendererOptions.TextColor);
        //------------------------------------------------------------------

        return pressed;
    }

    public void DrawLabel(int x, int y, string text)
    {
        // Draw control
        //--------------------------------------------------------------------
        var textColor = UIRendererOptions.TextColor;

        var bounds = new Rectangle(x, y, 0.0f, 0.0f);
        GuiDrawText(text, GetTextBounds(bounds), textColor);
        //--------------------------------------------------------------------
    }

    public void DrawLine(int x, int y, int width, int height, string text)
    {
        var lineColor = UIRendererOptions.LineColor;
        var bounds = new Rectangle(x, y, width, height);

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(new Rectangle(bounds.x, bounds.y + bounds.height / 2, bounds.width, 1), 0, Color.Blank, lineColor);
        //--------------------------------------------------------------------
    }

    const int RAYGUI_PANEL_BORDER_WIDTH = 1;
    const int RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT = 24;
    public void DrawPanel(int x, int y, int width, int height, string title, bool enabled = true)
    {
        var statusBarRectangle = new Rectangle(x, y, width, (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT);

        // Text will be drawn as a header bar (if provided)
        if (!string.IsNullOrWhiteSpace(title) && (height < RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2.0f))
        {
            height = RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2;
        }

        if (!string.IsNullOrWhiteSpace(title))
        {
            // Move panel bounds after the header bar
            y += RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT - 1;
            height -= RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT + 1;
        }

        // Draw control
        //--------------------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(title))
        {
            GuiStatusBar(statusBarRectangle, title, enabled);  // Draw panel header as status bar
        }

        var borderColor = enabled ? UIRendererOptions.BorderColor : UIRendererOptions.BorderColor;
        var backgroundColor = enabled ? UIRendererOptions.BackgroundColor : UIRendererOptions.BackgroundColor;

        var panelRectangle = new Rectangle(x, y, width, height);
        GuiDrawRectangle(panelRectangle, RAYGUI_PANEL_BORDER_WIDTH, borderColor, backgroundColor);
        //--------------------------------------------------------------------
    }

    // List View control
    public int DrawListView(Rectangle bounds, string text, ref int scrollIndex, int active, bool enabled = true)
    {
        throw new NotImplementedException();
    }

    // List View control with extended parameters
    public int DrawListViewEx(Rectangle bounds, string[] text, int count, ref int focus, ref int scrollIndex, int active, bool stickToBottom = false, bool enabled = true)
    {
        var options = UIRendererOptions.ListViewOptions;

        int itemFocused = (focus == 0) ? -1 : focus;
        int itemSelected = active;

        var useScrollBar = (options.ItemHeight + options.ItemSpacing) * count > bounds.height;

        var itemBounds = new Rectangle
        {
            x = bounds.x + options.ItemSpacing,
            y = bounds.y + options.ItemSpacing + UIRendererOptions.BorderWidth,
            width = bounds.width - 2 * options.ItemSpacing - UIRendererOptions.BorderWidth,
            height = (float)options.ItemHeight
        };

        if (useScrollBar)
        {
            itemBounds.width -= options.ScrollbarWidth;
        }

        int visibleItems = (int)bounds.height / (options.ItemHeight + options.ItemSpacing);
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
        if (enabled)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            // Check mouse inside list view
            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
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
                    itemBounds.y += options.ItemHeight + options.ItemSpacing;
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
            itemBounds.y = bounds.y + options.ItemSpacing + UIRendererOptions.BorderWidth;
        }
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, UIRendererOptions.BorderWidth, options.BorderNormalColor, UIRendererOptions.BackgroundColor);     // Draw background

        // Draw visible items
        for (int i = 0; ((i < visibleItems) && (text.Length > 0)); i++)
        {
            if (enabled)
            {
                if ((startIndex + i) == itemSelected)
                {
                    GuiDrawRectangle(itemBounds, UIRendererOptions.BorderWidth, options.BorderDisabledColor, options.DisabledColor);
                }

                GuiDrawText(text[startIndex + i], GetTextBounds(itemBounds), options.TextDisabledColor);
            }
            else
            {
                if ((startIndex + i) == itemSelected)
                {
                    // Draw item selected
                    GuiDrawRectangle(itemBounds, UIRendererOptions.BorderWidth, options.BorderPressedColor, options.PressedColor);
                    GuiDrawText(text[startIndex + i], GetTextBounds(itemBounds), options.TextPressedColor);
                }
                else if ((startIndex + i) == itemFocused)
                {
                    // Draw item focused
                    GuiDrawRectangle(itemBounds, UIRendererOptions.BorderWidth, options.BorderFocusedColor, options.FocusedColor);
                    GuiDrawText(text[startIndex + i], GetTextBounds(itemBounds), options.TextFocusedColor);
                }
                else
                {
                    // Draw item normal
                    GuiDrawText(text[startIndex + i], GetTextBounds(itemBounds), options.TextNormalColor);
                }
            }

            // Update item rectangle y position for next item
            itemBounds.y += (options.ItemHeight + options.ItemSpacing);
        }

        if (useScrollBar)
        {
            var scrollBarBounds = new Rectangle
            {
                x = bounds.x + bounds.width - UIRendererOptions.BorderWidth - options.ScrollbarWidth,
                y = bounds.y + UIRendererOptions.BorderWidth,
                width = (float)options.ScrollbarWidth,
                height = bounds.height - 2 * UIRendererOptions.BorderWidth
            };

            // Calculate percentage of visible items and apply same percentage to scrollbar
            float percentVisible = (float)(endIndex - startIndex) / count;
            float sliderSize = bounds.height * percentVisible;

            int prevSliderSize = UIRendererOptions.ScrollbarScrollSliderSize;   // Save default slider size
            int prevScrollSpeed = UIRendererOptions.ScrollbarScrollSpeed; // Save default scroll speed
            UIRendererOptions.ScrollbarScrollSliderSize = (int)sliderSize; // Change slider size
            UIRendererOptions.ScrollbarScrollSpeed = count - visibleItems; // Change scroll speed

            if (stickToBottom)
            {
                endIndex = GuiScrollBar(scrollBarBounds, endIndex, 0, count - visibleItems);
            }
            else
            {
                startIndex = GuiScrollBar(scrollBarBounds, startIndex, 0, count - visibleItems);
            }

            UIRendererOptions.ScrollbarScrollSpeed = prevScrollSpeed; // Reset scroll speed to default
            UIRendererOptions.ScrollbarScrollSliderSize = prevSliderSize; // Reset slider size to default
        }
        //--------------------------------------------------------------------

        if (focus != 0) focus = itemFocused;

        if (scrollIndex >= 0)
        {
            scrollIndex = startIndex;
        }
        if (stickToBottom)
        {
            scrollIndex = endIndex;
        }

        return itemSelected;
    }

    // Scroll Panel control
    public void DrawScrollPanel(Rectangle bounds, string text, Rectangle content, ref Vector2 scroll, bool enabled = true)
    {
        var listViewOptions = UIRendererOptions.ListViewOptions;

        Vector2 scrollPos = new Vector2(0.0f, 0.0f);

        if (scroll != default)
        {
            scrollPos = scroll;
        }

        // Text will be drawn as a header bar (if provided)
        Rectangle statusBar = new Rectangle(bounds.x, bounds.y, bounds.width, (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT);
        if (bounds.height < RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2.0f)
        {
            bounds.height = RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT * 2.0f;
        }

        if (!string.IsNullOrWhiteSpace(text))
        {
            // Move panel bounds after the header bar
            bounds.y += (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT - 1;
            bounds.height -= (float)RAYGUI_WINDOWBOX_STATUSBAR_HEIGHT + 1;
        }

        bool hasHorizontalScrollBar = (content.width > bounds.width - 2 * UIRendererOptions.BorderWidth) ? true : false;
        bool hasVerticalScrollBar = (content.height > bounds.height - 2 * UIRendererOptions.BorderWidth) ? true : false;

        // Recheck to account for the other scrollbar being visible
        if (!hasHorizontalScrollBar) hasHorizontalScrollBar = (hasVerticalScrollBar && (content.width > (bounds.width - 2 * UIRendererOptions.BorderWidth - listViewOptions.ScrollbarWidth))) ? true : false;
        if (!hasVerticalScrollBar) hasVerticalScrollBar = (hasHorizontalScrollBar && (content.height > (bounds.height - 2 * UIRendererOptions.BorderWidth - listViewOptions.ScrollbarWidth))) ? true : false;

        int horizontalScrollBarWidth = hasHorizontalScrollBar ? listViewOptions.ScrollbarWidth : 0;
        int verticalScrollBarWidth = hasVerticalScrollBar ? listViewOptions.ScrollbarWidth : 0;

        Rectangle horizontalScrollBar = new Rectangle((float)(listViewOptions.ScrollbarSide == ScrollbarSide.Left
            ? (float)bounds.x + verticalScrollBarWidth
            : (float)bounds.x) + UIRendererOptions.BorderWidth, (float)bounds.y + bounds.height - horizontalScrollBarWidth - UIRendererOptions.BorderWidth, (float)bounds.width - verticalScrollBarWidth - 2 * UIRendererOptions.BorderWidth, (float)horizontalScrollBarWidth);

        Rectangle verticalScrollBar = new Rectangle((float)(listViewOptions.ScrollbarSide == ScrollbarSide.Left
            ? (float)bounds.x + UIRendererOptions.BorderWidth
            : (float)bounds.x + bounds.width - verticalScrollBarWidth - UIRendererOptions.BorderWidth), (float)bounds.y + UIRendererOptions.BorderWidth, (float)verticalScrollBarWidth, (float)bounds.height - horizontalScrollBarWidth - 2 * UIRendererOptions.BorderWidth);

        // Calculate view area (area without the scrollbars)
        Rectangle view = listViewOptions.ScrollbarSide == ScrollbarSide.Left
            ? new Rectangle(bounds.x + verticalScrollBarWidth + UIRendererOptions.BorderWidth, bounds.y + UIRendererOptions.BorderWidth, bounds.width - 2 * UIRendererOptions.BorderWidth - verticalScrollBarWidth, bounds.height - 2 * UIRendererOptions.BorderWidth - horizontalScrollBarWidth)
            : new Rectangle(bounds.x + UIRendererOptions.BorderWidth, bounds.y + UIRendererOptions.BorderWidth, bounds.width - 2 * UIRendererOptions.BorderWidth - verticalScrollBarWidth, bounds.height - 2 * UIRendererOptions.BorderWidth - horizontalScrollBarWidth);

        // Clip view area to the actual content size
        if (view.width > content.width) view.width = content.width;
        if (view.height > content.height) view.height = content.height;

        float horizontalMin = hasHorizontalScrollBar ? (listViewOptions.ScrollbarSide == ScrollbarSide.Left
            ? (float)-verticalScrollBarWidth : 0) - (float)UIRendererOptions.BorderWidth
            : (listViewOptions.ScrollbarSide == ScrollbarSide.Left
                ? (float)-verticalScrollBarWidth
                : 0) - (float)UIRendererOptions.BorderWidth;

        float horizontalMax = hasHorizontalScrollBar
            ? content.width - bounds.width + (float)verticalScrollBarWidth + UIRendererOptions.BorderWidth - (listViewOptions.ScrollbarSide == ScrollbarSide.Left
                ? (float)verticalScrollBarWidth
                : 0)
            : (float)-UIRendererOptions.BorderWidth;
        float verticalMin = hasVerticalScrollBar ? 0 : -1;
        float verticalMax = hasVerticalScrollBar ? content.height - bounds.height + (float)horizontalScrollBarWidth + (float)UIRendererOptions.BorderWidth : (float)-UIRendererOptions.BorderWidth;

        // Update control
        //--------------------------------------------------------------------
        if (enabled)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            // Check button state
            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    state = GuiState.STATE_PRESSED;
                }
                else
                {
                    state = GuiState.STATE_FOCUSED;
                }

#if SUPPORT_SCROLLBAR_KEY_INPUT
            if (hasHorizontalScrollBar)
            {
                if (IsKeyDown(KEY_RIGHT)) scrollPos.x -= GuiGetStyle(SCROLLBAR, SCROLL_SPEED);
                if (IsKeyDown(KEY_LEFT)) scrollPos.x += GuiGetStyle(SCROLLBAR, SCROLL_SPEED);
            }

            if (hasVerticalScrollBar)
            {
                if (IsKeyDown(KEY_DOWN)) scrollPos.y -= GuiGetStyle(SCROLLBAR, SCROLL_SPEED);
                if (IsKeyDown(KEY_UP)) scrollPos.y += GuiGetStyle(SCROLLBAR, SCROLL_SPEED);
            }
#endif
                float wheelMove = Raylib.GetMouseWheelMove();

                // Horizontal scroll (Shift + Mouse wheel)
                if (hasHorizontalScrollBar && (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SHIFT))) scrollPos.X += wheelMove * 20;
                else scrollPos.Y += wheelMove * 20; // Vertical scroll
            }
        }

        // Normalize scroll values
        if (scrollPos.X > -horizontalMin) scrollPos.X = -horizontalMin;
        if (scrollPos.X < -horizontalMax) scrollPos.X = -horizontalMax;
        if (scrollPos.Y > -verticalMin) scrollPos.Y = -verticalMin;
        if (scrollPos.Y < -verticalMax) scrollPos.Y = -verticalMax;
        //--------------------------------------------------------------------

        // Draw control
        //--------------------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(text)) GuiStatusBar(statusBar, text);  // Draw panel header as status bar

        GuiDrawRectangle(bounds, 0, Color.Blank, UIRendererOptions.BackgroundColor);        // Draw background

        // Save size of the scrollbar slider
        int slider = UIRendererOptions.ScrollbarScrollSliderSize;

        // Draw horizontal scrollbar if visible
        if (hasHorizontalScrollBar)
        {
            // Change scrollbar slider size to show the diff in size between the content width and the widget width
            UIRendererOptions.ScrollbarScrollSliderSize = (int)(((bounds.width - 2 * UIRendererOptions.BorderWidth - verticalScrollBarWidth) / (int)content.width) * ((int)bounds.width - 2 * UIRendererOptions.BorderWidth - verticalScrollBarWidth));
            scrollPos.X = (float)-GuiScrollBar(horizontalScrollBar, (int)-scrollPos.X, (int)horizontalMin, (int)horizontalMax);
        }
        else scrollPos.X = 0.0f;

        // Draw vertical scrollbar if visible
        if (hasVerticalScrollBar)
        {
            // Change scrollbar slider size to show the diff in size between the content height and the widget height
            UIRendererOptions.ScrollbarScrollSliderSize = (int)(((bounds.height - 2 * UIRendererOptions.BorderWidth - horizontalScrollBarWidth) / (int)content.height) * ((int)bounds.height - 2 * UIRendererOptions.BorderWidth - horizontalScrollBarWidth));
            scrollPos.Y = (float)-GuiScrollBar(verticalScrollBar, (int)-scrollPos.Y, (int)verticalMin, (int)verticalMax);
        }
        else scrollPos.Y = 0.0f;

        // Draw detail corner rectangle if both scroll bars are visible
        if (hasHorizontalScrollBar && hasVerticalScrollBar)
        {
            Rectangle corner = new Rectangle((listViewOptions.ScrollbarSide == ScrollbarSide.Left)
                    ? (bounds.x + UIRendererOptions.BorderWidth + 2)
                    : (horizontalScrollBar.x + horizontalScrollBar.width + 2), verticalScrollBar.y + verticalScrollBar.height + 2, (float)horizontalScrollBarWidth - 4, (float)verticalScrollBarWidth - 4);
            GuiDrawRectangle(corner, 0, Color.Blank, listViewOptions.TextNormalColor);
        }

        // Draw scrollbar lines depending on current state
        GuiDrawRectangle(bounds, UIRendererOptions.BorderWidth, listViewOptions.BorderNormalColor, Color.Blank);

        // Set scrollbar slider size back to the way it was before

        UIRendererOptions.ScrollbarScrollSliderSize = slider;
        //--------------------------------------------------------------------

        if (scroll != default)
        {
            scroll = scrollPos;
        }

        //return view;
    }

    // Scroll bar control (used by GuiScrollPanel())
    private int GuiScrollBar(Rectangle bounds, int value, int minValue, int maxValue)
    {
        // Is the scrollbar horizontal or vertical?
        bool isVertical = (bounds.width > bounds.height) ? false : true;

        // The size (width or height depending on scrollbar type) of the spinner buttons
        int spinnerSize = UIRendererOptions.ScrollbarArrowsVisible
            ? (isVertical
                ? (int)bounds.width - 2 * UIRendererOptions.ScrollbarBorderWidth
                : (int)bounds.height - 2 * UIRendererOptions.ScrollbarBorderWidth)
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
        int sliderSize = UIRendererOptions.ScrollbarScrollSliderSize;

        // Calculate rectangles for all of the components
        arrowUpLeft = new Rectangle(
            (float)bounds.x + UIRendererOptions.ScrollbarBorderWidth,
            (float)bounds.y + UIRendererOptions.ScrollbarBorderWidth,
            (float)spinnerSize, (float)spinnerSize);

        if (isVertical)
        {
            arrowDownRight = new Rectangle(
                (float)bounds.x + UIRendererOptions.ScrollbarBorderWidth,
                (float)bounds.y + bounds.height - spinnerSize - UIRendererOptions.ScrollbarBorderWidth,
                (float)spinnerSize, (float)spinnerSize);

            scrollbar = new Rectangle(
                bounds.x + UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding,
                arrowUpLeft.y + arrowUpLeft.height,
                bounds.width - 2 * (UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding),
                bounds.height - arrowUpLeft.height - arrowDownRight.height - 2 * UIRendererOptions.ScrollbarBorderWidth);

            sliderSize = (sliderSize >= scrollbar.height) ? ((int)scrollbar.height - 2) : sliderSize;     // Make sure the slider won't get outside of the scrollbar
            slider = new Rectangle(
                (float)bounds.x + UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding,
                (float)scrollbar.y + (int)(((float)(value - minValue) / range) * (scrollbar.height - sliderSize)),
                (float)bounds.width - 2 * (UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding),
                (float)sliderSize);
        }
        else
        {
            arrowDownRight = new Rectangle(
                (float)bounds.x + bounds.width - spinnerSize - UIRendererOptions.ScrollbarBorderWidth,
                (float)bounds.y + UIRendererOptions.ScrollbarBorderWidth,
                (float)spinnerSize, (float)spinnerSize);

            scrollbar = new Rectangle(
                arrowUpLeft.x + arrowUpLeft.width,
                bounds.y + UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding,
                bounds.width - arrowUpLeft.width - arrowDownRight.width - 2 * UIRendererOptions.ScrollbarBorderWidth,
                bounds.height - 2 * (UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding));

            sliderSize = (sliderSize >= scrollbar.width) ? ((int)scrollbar.width - 2) : sliderSize;       // Make sure the slider won't get outside of the scrollbar
            slider = new Rectangle(
                (float)scrollbar.x + (int)(((float)(value - minValue) / range) * (scrollbar.width - sliderSize)),
                (float)bounds.y + UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding,
                (float)sliderSize,
                (float)bounds.height - 2 * (UIRendererOptions.ScrollbarBorderWidth + UIRendererOptions.ScrollbarScrollPadding));
        }

        // Update control
        //--------------------------------------------------------------------
        if ((state != GuiState.STATE_DISABLED))
        {
            Vector2 mousePoint = Raylib.GetMousePosition();

            if (Raylib.CheckCollisionPointRec(mousePoint, bounds))
            {
                state = GuiState.STATE_FOCUSED;

                // Handle mouse wheel
                int wheel = (int)Raylib.GetMouseWheelMove();
                if (wheel != 0) value += wheel;

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePoint, arrowUpLeft)) value -= range / UIRendererOptions.ScrollbarScrollSpeed;
                    else if (Raylib.CheckCollisionPointRec(mousePoint, arrowDownRight)) value += range / UIRendererOptions.ScrollbarScrollSpeed;

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
                            bounds.height - 2 * UIRendererOptions.ScrollbarBorderWidth);
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
                            bounds.width - 2 * UIRendererOptions.ScrollbarBorderWidth,
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
        GuiDrawRectangle(bounds, UIRendererOptions.ScrollbarBorderWidth, UIRendererOptions.BorderColor, UIRendererOptions.BorderDisabledColor);   // Draw the background

        GuiDrawRectangle(scrollbar, 0, Color.Blank, UIRendererOptions.BaseColor);     // Draw the scrollbar active area background
        GuiDrawRectangle(slider, 0, Color.Blank, UIRendererOptions.BorderColor);         // Draw the slider bar

        // Draw arrows (using icon if available)
        if (UIRendererOptions.ScrollbarArrowsVisible)
        {
            var r1 = new Rectangle(arrowUpLeft.x, arrowUpLeft.y, isVertical ? bounds.width : bounds.height, isVertical ? bounds.width : bounds.height);
            var r2 = new Rectangle(arrowDownRight.x, arrowDownRight.y, isVertical ? bounds.width : bounds.height, isVertical ? bounds.width : bounds.height);

            GuiDrawText(isVertical ? "^" : "<", r1, UIRendererOptions.DropdownBorderColor);
            GuiDrawText(isVertical ? "v" : ">", r2, UIRendererOptions.DropdownBorderColor);
        }
        //--------------------------------------------------------------------

        return value;
    }

    // Status Bar control
    private void GuiStatusBar(Rectangle bounds, string text, bool enabled = true)
    {
        var borderColor = enabled ? UIRendererOptions.BorderColor : UIRendererOptions.BorderColor;
        var baseColor = enabled ? UIRendererOptions.BaseColor : UIRendererOptions.BaseColor;
        var textColor = enabled ? UIRendererOptions.TextColor : UIRendererOptions.TextColor;

        // Draw control
        //--------------------------------------------------------------------
        GuiDrawRectangle(bounds, UIRendererOptions.BorderWidth, borderColor, baseColor);
        GuiDrawText(text, GetTextBounds(bounds), textColor);
        //--------------------------------------------------------------------
    }

    private void GuiDrawText(string text, Rectangle bounds, Color tint)
    {
        var position = new Vector2(bounds.x, bounds.y);
        var font = Raylib.GetFontDefault();

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
