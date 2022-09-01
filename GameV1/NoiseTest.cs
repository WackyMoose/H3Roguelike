﻿using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
    private Npc druid = new Npc("Druid", 100, 1000, new Coords2D(9, 0));
    private Npc ork = new Npc("Ork", 100, 1000, new Coords2D(11, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    public void Initialize()
    {
        sword.MinDamage = 50;
        sword.MaxDamage = 200;
        sword.ArmorPenetrationFlat = 50;
        sword.ArmorPenetrationPercent = 20;

        armor.MinDamageReduction = 20;
        armor.MaxDamageReduction = 120;

        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene.SceneCamera = camera;
        //_scene?.Add(camera);

        // Spawn player
        //player.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        //player.MainHand.Add(sword);
        //player.Chest.Add(armor);
        //_scene?.Add(player);

        //light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        //_scene?.Add(light);

        //townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        //_scene?.Add(townLights);

        //druid.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        //druid.MainHand.Add(sword);
        //druid.Chest.Add(armor);
        //_scene?.Add(druid);

        //ork.Position = new Vector2(60, 32) * Constants.DEFAULT_ENTITY_SIZE;
        //ork.MainHand.Add(sword);
        //ork.Chest.Add(armor);
        //_scene?.Add(ork);

       // WorldGenerator.GenerateWorld(80085,ref tile);

        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        throw new NotImplementedException();
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        //// Reset all Entity Colortint to a cool nighttime blue
        //foreach (var entity in _scene.Tiles)
        //{
        //    entity.Value.ColorTint = new Color(128-64, 128, 128+64, 255);
        //}

        //// Player
        //InputOptions? input = InputHandler.Handle();

        //ICommand command = CommandFactory.Create(input, _scene, player);

        //CommandQueue.Add(command);

        //// Execute Player commands
        //if (!CommandQueue.IsEmpty)
        //{
        //    //Console.WriteLine("Players turn!");
        //    CommandQueue.Execute();

        //    // AI NPC / Monster / Critter controls
        //    //Console.WriteLine("AI's turn!");
        //    AI.Execute(_scene);

        //    // Execute AI commands
        //    CommandQueue.Execute();
        //}

        //// Dynamically updated light sources
        //foreach (var light in _scene.Tiles.OfType<LightSource>())
        //{
        //    light.Illuminate(_scene, _scene.Tiles);
        //}

        //_scene?.UpdateRuntime(deltaTime);
    }
}