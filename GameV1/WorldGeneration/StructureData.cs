using GameV1.Entities;

namespace GameV1.WorldGeneration
{
    public class StructureData
    {
        public Tile[]? Tiles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ObjectEntity[]? Objects { get; set; }
    }
}
