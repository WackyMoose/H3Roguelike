namespace GameV1.SpriteLibraries
{
    public class SpriteLibrary<T>
    {
        public string SpriteSheetPath { get; set; }
        public Dictionary<string, T> Sprites { get; set; }
    }
}
