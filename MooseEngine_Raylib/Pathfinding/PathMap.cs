using System.Numerics;

namespace MooseEngine.Pathfinding
{
    public class PathMap
    {
        public Dictionary<Vector2, MapNode>? Map { get; set; }
        
        public PathMap()
        {
            Map = new Dictionary<Vector2, MapNode>();
        }
    }
}
