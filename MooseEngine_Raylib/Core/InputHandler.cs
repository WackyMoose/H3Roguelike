using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MooseEngine.Scenes;
using Raylib_cs;

namespace MooseEngine.Core
{
    public static class InputHandler
    {
        public static ICommand? _key_up;
        public static ICommand? _key_down;
        public static ICommand? _key_left;
        public static ICommand? _key_right;

        public static void HandleInput(Entity entity)
        {
            if (Raylib.IsKeyPressed(Keyboard.KeyUp)) { _key_up?.Execute(entity); }
            if (Raylib.IsKeyPressed(Keyboard.KeyDown)) { _key_down?.Execute(entity); }
            if (Raylib.IsKeyPressed(Keyboard.KeyLeft)) { _key_left?.Execute(entity); }
            if (Raylib.IsKeyPressed(Keyboard.KeyRight)) { _key_right?.Execute(entity); }
        }
    }
}
