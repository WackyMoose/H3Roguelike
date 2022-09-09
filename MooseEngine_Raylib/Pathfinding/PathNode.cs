using MooseEngine.Scenes;
using System.Numerics;

namespace MooseEngine.Pathfinding
{
    public class PathNode : Entity
    {
        public float G { get; set; }
        public float H { get; set; }
        public float W { get; set; }
        public float F { get { return G + H + W; } }
        public PathNode? Parent { get; set; }

        public PathNode(Vector2 position, PathNode? parent) :base()
        {
            Position = position;
            Parent = parent;
        }

        public override void Initialize() { }

        public override void Update(float deltaTime) { }
    }
}
