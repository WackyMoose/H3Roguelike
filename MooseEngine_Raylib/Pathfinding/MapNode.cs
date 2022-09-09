using System.Numerics;

namespace MooseEngine.Pathfinding
{
    public class MapNode
    {
        public Vector2 NodePosition { get; set; }
        public float NodeWeight { get; set; }

        public MapNode(Vector2 nodePosition, float nodeWeight)
        {
            NodePosition = nodePosition;
            NodeWeight = nodeWeight;
        }
    }
}
