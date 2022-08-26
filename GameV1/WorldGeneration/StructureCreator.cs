using MooseEngine.Utilities;

namespace GameV1.WorldGeneration
{
    public static class StructureCreator
    {
        private static int[] walkableIDs = new int[] {-1, 2, 29, 58, 116, 117, 254, 257, 260, 284, 285};

        public static List<List<StructureData>> LoadStructure(string path) 
        {
            using (var reader = new StreamReader(path))
            {
                List<List<StructureData>> list = new List<List<StructureData>>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var coordsList = new List<StructureData>();

                    foreach (var val in values)
                    {
                        var n = int.Parse(val);
                        var coords = GetSpriteCoords(n);
                        var isWalkable = walkableIDs.Contains(n);
                        coordsList.Add(new StructureData(coords,isWalkable));
                    }

                    list.Add(coordsList);
                }

                return list;
            }
        }

        private static Coords2D GetSpriteCoords(int id) 
        {
            if (id == -1)
            {
                return new Coords2D(1,1);
            }
            var row = id / 28;
            var col = id % 28;

            return new Coords2D(col, row);
        }
    }
}
