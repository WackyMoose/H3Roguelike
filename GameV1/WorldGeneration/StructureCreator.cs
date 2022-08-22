using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Console.WriteLine(val);
                        var n = int.Parse(val);
                        var coords = GetSpriteCoords(n);
                        coordsList.Add(coords);
                        Console.WriteLine($"Id: {n} has Coords: {coords.X}:{coords.Y}");

                    }

                    list.Add(coordsList);
                   
                    Console.WriteLine("--------------------------------------------------");
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
