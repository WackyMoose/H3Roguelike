using System.Numerics;
using SimplexNoise;

namespace GameV1.WorldGeneration
{
    internal static class ProceduralAlgorithms
    {
        //TODO Change Vector2 to Coords2D...
        //TODO Add overworld generation...
        //TODO Add WFC to generate castles etc...
        //TODO Use Cellular to generate Dungeons...

        private static void GenerateOverworld(int width, int height, int tileSize) 
        {
            Dictionary<Vector2, float> overworld = new Dictionary<Vector2, float>();

            for (int x = 0; x < width; x++)
			{
                for (int y = 0; y < height; y++)
			    {
                    overworld.Add(new Vector2(x,y), Noise.CalcPixel2D(x, y, tileSize));
			    }
			}

        }

        public static HashSet<Vector2> GenerateForest(int iterations, int walkLength, Vector2 position) 
        {
            HashSet<Vector2> treePositions = new HashSet<Vector2>();

            for (int i = 0; i < iterations; i++)
            {
                var path = SimpleRandomWalk(position, walkLength);
                treePositions.UnionWith(path);
            }

            return treePositions;
        }

        private static HashSet<Vector2> SimpleRandomWalk(Vector2 startPosition, int walkLength) 
        {
            HashSet<Vector2> path = new HashSet<Vector2>();

            path.Add(startPosition);
            var prevPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                var newPosition = prevPosition + Direction2D.GetRandomCardinalDirection() * 64;
                path.Add(newPosition);
                prevPosition = newPosition;
            }

            return path;
        }

        private static class Direction2D 
        {
            public static List<Vector2> CardinalDirectionList = new List<Vector2>
            {
                new Vector2(0,1),  //Up
                new Vector2(1,0),  //Right
                new Vector2(0,-1), //Down
                new Vector2(-1,0), //Left
            };

            public static Vector2 GetRandomCardinalDirection() 
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
