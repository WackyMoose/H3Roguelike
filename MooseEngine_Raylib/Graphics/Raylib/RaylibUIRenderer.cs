using MooseEngine.Core;
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

        if (UIRendererOptions.StatusBarTextAlignment == TextAlignment.TEXT_ALIGN_RIGHT)
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
