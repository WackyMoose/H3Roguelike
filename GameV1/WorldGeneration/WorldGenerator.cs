using GameV1.Entities;
using MooseEngine;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GameV1.WorldGeneration
{
    public static class WorldGenerator
    {
        private static HashSet<Coords2D> _forest = new HashSet<Coords2D>();
        private static HashSet<Coords2D> _forests = new HashSet<Coords2D>();
        private static Dictionary<Coords2D, float> _overWorld = new Dictionary<Coords2D, float>();
        private static List<List<Coords2D>> _structure = new List<List<Coords2D>>();
        private static List<List<StructureData>> _startVillage = new List<List<StructureData>>();

        private static Coords2D[] _grassTilesCoords = new Coords2D[3]{new Coords2D(5, 4),
                                                                     new Coords2D(4, 10),
                                                                     new Coords2D(5, 10)};

        private static int[] walkableIds = new int[]{};



        //private static Tile tree01 = new Tile("Tree01", true, new Coords2D());

        //TODO We need to get scene out of param, perhaps make GenerateWorld return a map of sort.
        public static bool GenerateWorld(int seed, ref IScene scene) 
        {
            var world = new World(501,501,seed,new Coords2D(251*Constants.DEFAULT_ENTITY_SIZE,251 * Constants.DEFAULT_ENTITY_SIZE));
            
            _overWorld = ProceduralAlgorithms.GeneratePerlinNoiseMap(world.WorldWidth, world.WorldHeight, Constants.DEFAULT_ENTITY_SIZE, world.WorldSeed);
            _startVillage = StructureCreator.LoadStructure(@"..\..\..\Resources\CSV\StarterVillage.csv");

            foreach (var tile in _overWorld)
            {
                //Console.WriteLine($"Tile: ({tile.Key.X}/{tile.Key.Y}), has value: {tile.Value}");

                //Generate grass with perlin noise..
                if (tile.Value > -0.3 && tile.Value < 0.3)
                {
                    var rand = Randomizer.RandomInt(0, 3);
                    var coord = _grassTilesCoords[rand];

                    Tile grass = new Tile("Grass", true, coord);
                    grass.Position = new Vector2(tile.Key.X, tile.Key.Y);
                    world.AddTile(tile.Key, grass);
                    Console.WriteLine($"Grass Tile at pos {grass.Position.X}:{grass.Position.Y}");
                }
                //Place Start Village...

                //Place forests, replacing grass tiles with trees...
                //Place Castles...

                if (tile.Value > 0.48 && tile.Value < 0.5)
                {
                    _forest = ProceduralAlgorithms.GenerateForest(50, 4, tile.Key);
                    foreach (var tree in _forest)
                    {
                        _forests.Add(tree);
                    }
                }
                //else if (tile.Value == 0)
                //{
                //    for (int k = 0; k < structure.Count; k++)
                //    {
                //        for (int i = 0; i < structure[k].Count; i++)
                //        {
                //            Tile spriteTile = new Tile("Castle", false, structure[k][i]);
                //            spriteTile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
                //            spriteTile.Position = new Vector2(tile.Key.X + i * Constants.DEFAULT_ENTITY_SIZE, tile.Key.Y + k * Constants.DEFAULT_ENTITY_SIZE);
                //            scene?.Add(spriteTile);
                //        }
                //    }
                //}

                //Visualize PerlinNoise, used for debugging...
                //float val = MathFunctions.InverseLerp(-1, 1, tile.Value);
                //int colorVal = (int)MathFunctions.Lerp(0, 255, val);
                //Color tintColor = new Color(colorVal, colorVal, colorVal, 255);
                //Tile perlinTile = new Tile("Perlin", true, new Coords2D(3, 10), tintColor);
                //perlinTile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
                //perlinTile.Position = new Vector2(tile.Key.X, tile.Key.Y);
                //scene?.Add(perlinTile);
            }


            //foreach (var pos in _forests)
            //{
            //    Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            //    tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
            //    tile.Position = new Vector2(pos.X, pos.Y);
            //    scene?.Add(tile);
            //}

            for (int k = 0; k < _startVillage.Count; k++)
            {
                for (int i = 0; i < _startVillage[k].Count; i++)
                {
                    Tile spriteTile = new Tile("StartVillage", _startVillage[k][i].IsWalkable, _startVillage[k][i].SpriteCoords);
                    spriteTile.Position = new Vector2((world.StartPos.X - (9 * Constants.DEFAULT_ENTITY_SIZE)) + (i * Constants.DEFAULT_ENTITY_SIZE), (world.StartPos.Y-(5 * Constants.DEFAULT_ENTITY_SIZE)) + (k * Constants.DEFAULT_ENTITY_SIZE));
                    world.AddTile(new Coords2D(spriteTile.Position), spriteTile);
                    Console.WriteLine($"Village tile at: {spriteTile.Position.X}:{spriteTile.Position.Y}");
                }
            }

            foreach (var tile in world.WorldTiles)
            {
                scene?.Add(tile.Value);
            }

            return true;
        }
    }
}
