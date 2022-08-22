using Raylib_cs;

namespace MooseEngine.Core
{
    public static class InputHandler
    {
        private static Dictionary<KeyboardKey, Input> KeyInput = new Dictionary<KeyboardKey, Input>();

        public static void Add(KeyboardKey key, Input input)
        {
            KeyInput.Add(key, input);
        }

        public static Input? Handle()
        {
            foreach (var pair in KeyInput)
            {
                if (Raylib.IsKeyPressed(pair.Key)) { return pair.Value; }
            }

            return null;
        }
    }
}
