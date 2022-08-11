using MooseEngine.Core;
using MooseEngine.Scene;
using System.Numerics;

namespace H3Roguelike;

internal class TestGame : Game
{
    public TestGame()
    {
    }

    TestEntity testEntity;

    public override void Start()
    {
        //throw new NotImplementedException();
        var spriteCoords = new Vector2(4, 0);

        testEntity = new TestEntity(spriteCoords);
        testEntity.Scale = new Vector2(64, 64);

        testEntity.Position = new Vector2(100, 100);

    }

    public override void Update(float deltaTime)
    {
        //throw new NotImplementedException();
    }

    public override void Render()
    {
        //throw new NotImplementedException();

        testEntity.Render();

    }
}