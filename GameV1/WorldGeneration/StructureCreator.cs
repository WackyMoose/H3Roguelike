using MooseEngine.Utilities;

namespace GameV1.WorldGeneration
{
    public static class StructureCreator
    {
        public static List<List<Coords2D>> LoadStructure(string path) 
        {
            using (var reader = new StreamReader(path))
            {
                List<List<Coords2D>> list = new List<List<Coords2D>>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var coordsList = new List<Coords2D>();

                    foreach (var val in values)
                    {
                        var n = int.Parse(val);
                        var coords = GetSpriteCoords(n);
                        coordsList.Add(coords);
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
