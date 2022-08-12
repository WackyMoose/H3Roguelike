using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scenes
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
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
