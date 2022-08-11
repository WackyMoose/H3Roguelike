﻿using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace H3Roguelike;

internal class TestEntity : Entity
{
    public TestEntity(Vector4 spriteCoords) : base(spriteCoords)
    {
    }

    public override void Initialize()
    {
    }

    public override void Update(float deltaTime)
    {
    }
}

internal class TestGame : IGame
{
    private Scene? _scene;

    public void Initialize()
    {
        _scene = new Scene();
        
        var entity = new TestEntity(new Vector4(8 * 5, 0, 8, 8));
        entity.Position = new Vector2(128, 192);

        _scene?.Add(entity);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        _scene?.UpdateRuntime(deltaTime);
    }

    public void Render()
    {
        _scene?.RenderRuntime();
    }
}