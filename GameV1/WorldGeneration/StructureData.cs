using MooseEngine.Utilities;

namespace GameV1.WorldGeneration
{
    public class StructureData
    {
        public StructureData(Coords2D coords, bool isWalkable)
        {
            SpriteCoords = coords;
            IsWalkable = isWalkable;
        }

        public Coords2D SpriteCoords { get; set; }
        public bool IsWalkable { get; set; }
    }
}
