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
        public static Command? _key_up;
        public static Command? _key_down;
        public static Command? _key_left;
        public static Command? _key_right;

        public static Command? HandleInput()
        {
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveUp)) { return _key_up; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveDown)) { return _key_down; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveLeft)) { return _key_left; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveRight)) { return _key_right; }
            else return null;
        }
    }
}
