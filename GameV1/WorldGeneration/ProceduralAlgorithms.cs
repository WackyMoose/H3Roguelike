using MooseEngine.Utilities;
using SimplexNoise;

namespace GameV1.WorldGeneration
{
    internal static class ProceduralAlgorithms
    {
        //TODO Change Vector2 to Coords2D...
        //TODO Add overworld generation...
        //TODO Add WFC to generate castles etc...
        //TODO Use Cellular to generate Dungeons...

        public static Dictionary<Coords2D, float> GenerateOverworld(int width, int height, int tileSize, float worldScale) 
        {
            Dictionary<Coords2D, float> overworld = new Dictionary<Coords2D, float>();

            for (int x = 0; x < width; x++)
			{
                var xCord = x * (int)worldScale;
                for (int y = 0; y < height; y++)
			    {
                    var yCord = y * (int)worldScale;

                    overworld.Add(new Coords2D(xCord, yCord), SimplexNoise.Noise.CalcPixel2D(xCord, yCord, tileSize));
			    }
			}

            return overworld;
        }

        public static HashSet<Coords2D> GenerateForest(int iterations, int walkLength, Coords2D position) 
        {
            HashSet<Coords2D> treePositions = new HashSet<Coords2D>();

            for (int i = 0; i < iterations; i++)
            {
                var path = SimpleRandomWalk(position, walkLength);
                treePositions.UnionWith(path);
            }

            return treePositions;
        }

        private static HashSet<Coords2D> SimpleRandomWalk(Coords2D startPosition, int walkLength) 
        {
            HashSet<Coords2D> path = new HashSet<Coords2D>();

            path.Add(startPosition);
            var prevPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                var dir = Direction2D.GetRandomCardinalDirection() * 32;
                var newPosition = prevPosition + new Coords2D((int)dir.X,(int)dir.Y);
                path.Add(newPosition);
                prevPosition = newPosition;
            }

            return path;
        }

        private static class Direction2D 
        {
            public static List<Coords2D> CardinalDirectionList = new List<Coords2D>
            {
                new Coords2D(0,1),  //Up
                new Coords2D(1,0),  //Right
                new Coords2D(0,-1), //Down
                new Coords2D(-1,0), //Left
            };

            public static Coords2D GetRandomCardinalDirection() 
            {
                Random rnd = new Random();
                return CardinalDirectionList[rnd.Next(CardinalDirectionList.Count)];
            }
        }
    }

    public enum ForestSize
    {
        small,medium,large
    }
}
