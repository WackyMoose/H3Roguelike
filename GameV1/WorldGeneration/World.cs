using GameV1.Entities;
using MooseEngine.Utilities;

namespace GameV1.WorldGeneration
{
    public class World
    {
        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }
        public int WorldSeed { get; private set; }
        public Coords2D StartPos { get; private set; }

        public Dictionary<Coords2D, Tile> WorldTiles = new Dictionary<Coords2D, Tile>();

        public World(int worldWidth, int worldHeight, int worldSeed, Coords2D startPos)
        {
            WorldWidth = worldWidth;
            WorldHeight = worldHeight;
            WorldSeed = worldSeed;
            StartPos = startPos;
        }

        public void AddTile(Coords2D coord, Tile newTile) 
        {
            if (WorldTiles.ContainsKey(coord))
            {
                WorldTiles[coord] = newTile;
            }
            else
            {
                WorldTiles.Add(coord, newTile);
            }
        }
    }
}
