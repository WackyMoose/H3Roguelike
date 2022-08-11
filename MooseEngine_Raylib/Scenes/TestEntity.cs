using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scenes
{
    public class TestEntity : Entity
    {
        public TestEntity(Vector2 spriteCoords, Color colorTint) 
            : base(spriteCoords, colorTint)
        {
        }

        public TestEntity(Vector2 spriteCoords) : base(spriteCoords)
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
