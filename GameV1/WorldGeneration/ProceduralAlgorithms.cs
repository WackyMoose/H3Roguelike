using System.Numerics;

namespace GameV1.WorldGeneration
{
    internal static class ProceduralAlgorithms
    {
        //TODO Change Vector2 to Coords2D...
        //TODO Add overworld generation...
        //TODO Add WFC to generate castles etc...
        //TODO Use Cellular to generate Dungeons...

        public static HashSet<Vector2> GenerateForest(ForestSize size, Vector2 position) 
        {
            int iterations = 0;
            int walkLength = 0;

            switch (size)
            {
                case ForestSize.small:
                    iterations = 50;
                    walkLength = 6;
                    break;
                case ForestSize.medium:
                    iterations = 75;
                    walkLength = 8;
                    break;
                case ForestSize.large:
                    iterations = 100;
                    walkLength = 10;
                    break;
                default:
                    break;
            }

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
                var newPosition = prevPosition + Direction2D.GetRandomCardinalDirection();
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
