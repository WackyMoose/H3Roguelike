using MooseEngine.Scenes;
using System.Numerics;

namespace MooseEngine.Utilities
{
    public class PathNode : Entity
    {
        public float G { get; set; }
        public float H { get; set; }
        public float F { get; set; }
        public PathNode? Parent { get; set; }
        
        public PathNode(Vector2 position, PathNode? parent)
        {
            Position = position;
            Parent = parent;
        }
        public PathNode(Vector2 position, float g, float h, float f, PathNode parent) : base()
        {
            Position = position;
            G = g;
            H = h;
            F = f;
            Parent = parent;
        }

        public override void Initialize(){}

        public override void Update(float deltaTime){}
    }

    public enum PathNodeState
    {
        Open,Closed
    }
}
