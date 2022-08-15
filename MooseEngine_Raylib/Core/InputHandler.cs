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
        public static Command? _key_null;

        public static Command? HandleInput()
        {
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveUp)) { return _key_up; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveDown)) { return _key_down; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveLeft)) { return _key_left; }
            if (Raylib.IsKeyPressed(Keyboard.KeyMoveRight)) { return _key_right; }
            else return null;
        }

        //public static ICommand HandleInput(Entity entity)
        //{
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveUp)) { return _key_up; }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveDown)) { return _key_down; }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveLeft)) { return _key_left; }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveRight)) { return _key_right; }
        //    else return null;
        //}

        //public static void HandleInput(Entity entity)
        //{
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveUp)) { _key_up?.Execute(entity); }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveDown)) { _key_down?.Execute(entity); }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveLeft)) { _key_left?.Execute(entity); }
        //    if (Raylib.IsKeyPressed(Keyboard.KeyMoveRight)) { _key_right?.Execute(entity); }
        //}
    }
}
