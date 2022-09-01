﻿using MooseEngine.Core;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

public interface IUIRenderer
{
    IWindowData WindowData { get; }

    void DrawFPS(int x, int y);
    void DrawText(string text, int x, int y, int fontScale, Color background, Color fontColor);

    bool DrawButton(int x, int y, string text, bool enabled = true);
    void DrawLabel(int x, int y, string text);
    void DrawLine(int x, int y, int width, int height, string text);
    void DrawPanel(int x, int y, int width, int height, string title, bool enabled = true);
    int DrawListView(Rectangle bounds, string text, ref int scrollIndex, int active, bool enabled = true);
    int DrawListViewEx(Rectangle bounds, string[] text, int count, ref int focus, ref int scrollIndex, int active, bool stickToBottom = false, bool enabled = true);
    void DrawScrollPanel(Rectangle bounds, string text, Rectangle content, ref Vector2 scroll, bool enabled = true);
}
