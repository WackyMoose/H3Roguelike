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
            if (Input.IsKeyPressed(Keycode.KEY_W)) { return _key_up; }
            if (Input.IsKeyPressed(Keycode.KEY_S)) { return _key_down; }
            if (Input.IsKeyPressed(Keycode.KEY_A)) { return _key_left; }
            if (Input.IsKeyPressed(Keycode.KEY_D)) { return _key_right; }
            else return null;
        }
    }
}
