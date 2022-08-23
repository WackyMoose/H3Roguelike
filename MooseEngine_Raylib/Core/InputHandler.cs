using Raylib_cs;

namespace MooseEngine.Core
{
    public static class InputHandler
    {
        private static Dictionary<KeyboardKey, InputOptions> KeyInput = new Dictionary<KeyboardKey, InputOptions>();

        public static void Add(KeyboardKey key, InputOptions input)
        {
            KeyInput.Add(key, input);
        }

        public static InputOptions? Handle()
        {
            foreach (var pair in KeyInput)
            {
                if (Raylib.IsKeyPressed(pair.Key)) { return pair.Value; }
            }

            return null;
        }
    }
}
