 using Raylib_cs;

namespace MooseEngine.Core
{
    public static class Keyboard
    {

        public static Dictionary<KeyboardKey, Command>? Key = new Dictionary<KeyboardKey, Command>();

        public static KeyboardKey KeyIdle { get; set; }
        public static KeyboardKey KeyMoveUp { get; set; }
        public static KeyboardKey KeyMoveDown { get; set; }
        public static KeyboardKey KeyMoveLeft { get; set; }
        public static KeyboardKey KeyMoveRight { get; set; }
        public static KeyboardKey KeyInteract { get; set; }
        public static KeyboardKey KeyInventory { get; set; }
        public static KeyboardKey KeyCharacter { get; set; }
        public static KeyboardKey KeyMenu { get; set; }
        public static KeyboardKey KeyQuickSlot1 { get; set; }
        public static KeyboardKey KeyQuickSlot2 { get; set; }
        public static KeyboardKey KeyQuickSlot3 { get; set; }
        public static KeyboardKey KeyQuickSlot4 { get; set; }
    }
}
