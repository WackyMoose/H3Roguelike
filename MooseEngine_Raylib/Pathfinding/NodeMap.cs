using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Pathfinding
{
    public class NodeMap<TType> where TType : class, IPathMappable
    {
        public PathMap GenerateMap(IEntityLayer<TType> walkableLayer)
        {
            var map = new PathMap();

            foreach (TType tile in walkableLayer.ActiveEntities.Values)
            {
                var node = new MapNode(tile.Position, tile.PathWeight);
                map.Map.Add(tile.Position, node);
            }

            return map;
        }
    }
}
