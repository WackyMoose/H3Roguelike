namespace MooseEngine.Core
{
    public static class InputHandler
    {
        private static Dictionary<Keycode, InputOptions> KeyInput = new Dictionary<Keycode, InputOptions>();

        public static void Add(Keycode key, InputOptions input)
        {
            KeyInput.Add(key, input);
        }

        public static InputOptions? Handle()
        {
            foreach (var pair in KeyInput)
            {
                if (Input.IsKeyPressed(pair.Key)) { return pair.Value; }
            }

            return null;
        }
    }
}
