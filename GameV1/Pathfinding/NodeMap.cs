using GameV1.Entities;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;

namespace GameV1.Pathfinding
{
    public class NodeMap
    {
        public PathMap GenerateMap(IEntityLayer<Tile> walkableLayer)
        {
            var map = new PathMap();
            foreach (Tile tile in walkableLayer.Entities.Values)
            {
                var node = new MapNode(tile.Position, tile.PathWeight);
                map.Map.Add(tile.Position, node);
            }

            return map;
        }
    }
}
