using MooseEngine.Utility;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scene
{
    public class TestEntity : Entity
    {
        public TestEntity(Coords2D spriteCoords, Color colorTint) : base(spriteCoords, colorTint)
        {
        }

        public TestEntity(Coords2D spriteCoords) : base(spriteCoords)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
