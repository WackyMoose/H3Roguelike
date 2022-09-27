namespace MooseEngine.Core
{
    public static class InputHandler
    {
        private static Dictionary<Keycode, InputOptions> KeyInput = new Dictionary<Keycode, InputOptions>();

        public static void Add(Keycode key, InputOptions input)
        {
            if (!KeyInput.ContainsKey(key))
            {
                KeyInput.Add(key, input);
            }
        }

        public static IEnumerable<InputOptions>? Handle()
        {
            var input = new List<InputOptions>();

            foreach (var pair in KeyInput)
            {
                if (Input.IsKeyPressed(pair.Key))
                {
                    input.Add(pair.Value);
                }
            }

            if (input.Count > 0)
            {
                return input;
            }
            else
            {
                return null;
            }
        }
    }
}
