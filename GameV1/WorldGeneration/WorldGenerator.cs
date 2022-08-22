using GameV1.Entities;
using MooseEngine;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
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
        private static List<List<Coords2D>> structure = new List<List<Coords2D>>();
        
        //TODO We need to get scene out of param, perhaps make GenerateWorld return a map of sort.
        public static bool GenerateWorld(int seed, ref Scene scene) 
        {
            _overWorld = ProceduralAlgorithms.GenerateOverworld(100, 100, 8, Constants.DEFAULT_ENTITY_SIZE,seed);

            structure = StructureCreator.LoadStructure(@"..\..\..\Resources\CastleCSV\Castle01.csv");

            foreach (var tile in _overWorld)
            {
                Console.WriteLine($"Tile: ({tile.Key.X}/{tile.Key.Y}), has value: {tile.Value}");

                //float val = MathFunctions.InverseLerp(-1,1,tile.Value);
                //int colorVal = (int)MathFunctions.Lerp(0, 255, val);
                //Color tintColor = new Color(colorVal, colorVal, colorVal, 255);
                //Tile perlinTile = new Tile("Perlin", true, new Coords2D(3, 10),tintColor);
                //perlinTile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
                //perlinTile.Position = new Vector2(tile.Key.X, tile.Key.Y);
                //_scene?.Add(perlinTile);

                if (tile.Value > 0.48 && tile.Value < 0.5)
                {
                    _forest = ProceduralAlgorithms.GenerateForest(50, 4, tile.Key);
                    foreach (var tree in _forest)
                    {
                        _forests.Add(tree);
                    }
                }
                else if (tile.Value == 0)
                {
                    for (int k = 0; k < structure.Count; k++)
                    {
                        for (int i = 0; i < structure[k].Count; i++)
                        {
                            Tile spriteTile = new Tile("Castle", false, structure[k][i]);
                            spriteTile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
                            spriteTile.Position = new Vector2(tile.Key.X + i * Constants.DEFAULT_ENTITY_SIZE, tile.Key.Y + k * Constants.DEFAULT_ENTITY_SIZE);
                            scene?.Add(spriteTile);
                        }
                    }
                }
            }

            foreach (var pos in _forests)
            {
                Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
                tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
                tile.Position = new Vector2(pos.X, pos.Y);
                scene?.Add(tile);
            }

            Console.WriteLine($"We have {_forests.Count} forest tiles");

            return true;
        }
    }
}
