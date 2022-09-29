namespace MooseEngine.Core
{
    public enum KeyModifier : uint
    {
        KeyPressed,
        KeyReleased,
        KeyDown,
        KeyUp
    }

    public struct KeyStroke
    {
        public Keycode Keycode { get; set; }
        public KeyModifier KeyModifier { get; set; }
    }

    public static class InputHandler
    {
        private static Dictionary<KeyStroke, InputOptions> KeyInputs = new Dictionary<KeyStroke, InputOptions>();

        public static void Add(KeyStroke keyStroke, InputOptions input)
        {
            if (KeyInputs.ContainsKey(keyStroke) == false)
            {
                KeyInputs.Add(keyStroke, input);
            }
        }

        public static IEnumerable<InputOptions>? Handle()
        {
            var input = new List<InputOptions>();
            
            foreach (var keyInput in KeyInputs)
            {
                switch (keyInput.Key.KeyModifier)
                {
                    case KeyModifier.KeyPressed:
                        if (Input.IsKeyPressed(keyInput.Key.Keycode))
                        {
                            input.Add(keyInput.Value);
                        }
                        break;
                    case KeyModifier.KeyReleased:
                        if (Input.IsKeyReleased(keyInput.Key.Keycode))
                        {
                            input.Add(keyInput.Value);
                        }
                        break;
                    case KeyModifier.KeyDown:
                        if (Input.IsKeyDown(keyInput.Key.Keycode))
                        {
                            input.Add(keyInput.Value);
                        }
                        break;
                    case KeyModifier.KeyUp:
                        if (Input.IsKeyUp(keyInput.Key.Keycode))
                        {
                            input.Add(keyInput.Value);
                        }
                        break;
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
