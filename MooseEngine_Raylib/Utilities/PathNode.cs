using MooseEngine.Scenes;
using System.Numerics;

namespace MooseEngine.Utilities
{
    public class PathNode : Entity
    {
        public Coords2D Position;
        public float G;
        public float H;
        public float F;
        public PathNode? Parent;

        public PathNode(Coords2D position, float g, float h, float f, PathNode? parent) : base()
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
}
