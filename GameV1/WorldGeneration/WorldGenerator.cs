using GameV1.Entities;
using GameV1.SpriteLibraries;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.WorldGeneration
{
    public static class WorldGenerator
    {
        private static HashSet<Coords2D> _forest = new HashSet<Coords2D>();
        private static HashSet<Coords2D> _forests = new HashSet<Coords2D>();
        private static Dictionary<Coords2D, float> _overWorld = new Dictionary<Coords2D, float>();
        private static List<List<StructureData>> _castle01Data = new List<List<StructureData>>();
        private static List<List<StructureData>> _castle02Data = new List<List<StructureData>>();
        private static List<List<StructureData>> _startVillageData = new List<List<StructureData>>();

        //TODO We need to get scene out of param, perhaps make GenerateWorld return a map of sort.
        public static bool GenerateWorld(int seed, ref IScene scene)
        {
            var world = new World(201, 201, seed, new Coords2D(51 * Constants.DEFAULT_ENTITY_SIZE, 51 * Constants.DEFAULT_ENTITY_SIZE));

            _overWorld = ProceduralAlgorithms.GeneratePerlinNoiseMap(world.WorldWidth, world.WorldHeight, Constants.DEFAULT_ENTITY_SIZE, world.WorldSeed);

            _castle02Data = StructureCreator.LoadStructure(@"..\..\..\Resources\CSV\Castle02.csv");
            _startVillageData = StructureCreator.LoadStructure(@"..\..\..\Resources\CSV\StarterVillage.csv");

            TileLibrary lib = JsonUtility.LoadFromJson<TileLibrary>(@"..\..\..\Resources\JSON\Tiles.json");

            foreach (var tile in _overWorld)
            {
                //Generate grass with perlin noise..
                if (tile.Value > -0.3 && tile.Value < 0.3)
                {
                    var rand = Randomizer.RandomInt(0, 3);
                    var grassTile = lib.Tiles.ElementAt(rand);

                    Tile grass = grassTile.Value.DeepCopy();
                    grass.Position = new Vector2(tile.Key.X, tile.Key.Y);
                    world.AddTile(tile.Key, grass);
                }
                else
                {
                    Tile grass = new Tile("Empty", true, new Coords2D(1, 1), Color.White);
                    grass.Position = new Vector2(tile.Key.X, tile.Key.Y);
                    world.AddTile(tile.Key, grass);
                }
                ////Generate water with perlin noise..
                //if (tile.Value > 0.8)
                //{

                //    Tile water = new Tile("Water", true, new Coords2D(12, 8), Color.White);
                //    water.Position = new Vector2(tile.Key.X, tile.Key.Y);
                //    world.AddTile(tile.Key, water);
                //    //Console.WriteLine($"Grass Tile at pos {grass.Position.X}:{grass.Position.Y} is {dist} distance from {posB.X}:{posB.Y}");
                //}
            }

            foreach (var tile in _overWorld)
            {
                //Place forests, replacing grass tiles with trees...
                //Place Castles...

                if (tile.Value > 0.3 && tile.Value < 0.305)
                {
                    _forest = ProceduralAlgorithms.GenerateForest(75, 5, tile.Key);

                    foreach (var coord in _forest)
                    {
                        var rand = Randomizer.RandomInt(3, 5);
                        var treeTile = lib.Tiles.ElementAt(rand);

                        Tile tree = treeTile.Value.DeepCopy();
                        tree.Position = new Vector2(coord.X, coord.Y);
                        world.AddTile(coord, tree);
                    }
                }

                //if (tile.Value > 0.1 && tile.Value < 0.101)
                //{
                //    for (int k = 0; k < _castle02Data.Count; k++)
                //    {
                //        for (int i = 0; i < _castle02Data[k].Count; i++)
                //        {
                //            Tile spriteTile = new Tile("Castle", _castle02Data[k][i].IsWalkable, _castle02Data[k][i].SpriteCoords);
                //            spriteTile.Position = new Vector2(tile.Key.X + i * Constants.DEFAULT_ENTITY_SIZE, tile.Key.Y + k * Constants.DEFAULT_ENTITY_SIZE);
                //            world.AddTile(new Coords2D(spriteTile.Position), spriteTile);
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

            //Place Start Village...
            for (int k = 0; k < _startVillageData.Count; k++)
            {
                for (int i = 0; i < _startVillageData[k].Count; i++)
                {
                    Tile spriteTile = new Tile("StartVillage", _startVillageData[k][i].IsWalkable, _startVillageData[k][i].SpriteCoords);
                    spriteTile.Position = new Vector2((world.StartPos.X - (9 * Constants.DEFAULT_ENTITY_SIZE)) + (i * Constants.DEFAULT_ENTITY_SIZE), (world.StartPos.Y - (5 * Constants.DEFAULT_ENTITY_SIZE)) + (k * Constants.DEFAULT_ENTITY_SIZE));
                    world.AddTile(new Coords2D(spriteTile.Position), spriteTile);
                    //Console.WriteLine($"Village tile at: {spriteTile.Position.X}:{spriteTile.Position.Y}");
                }
            }

            scene.AddLayer<Tile>(EntityLayer.WalkableTiles);
            scene.AddLayer<Tile>(EntityLayer.NonWalkableTiles);

            foreach (var tile in world.WorldTiles)
            {
                if (tile.Value.IsWalkable == true)
                {
                    scene.GetLayer((int)EntityLayer.WalkableTiles).ActiveEntities.Add(tile.Key, tile.Value);
                }
                else
                {
                    scene.GetLayer((int)EntityLayer.NonWalkableTiles).ActiveEntities.Add(tile.Key, tile.Value);
                }
            }

            return true;
        }
    }
}
