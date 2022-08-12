using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Categories
{
    public class MaterialCategory
    {
        public string Name { get; }

        private MaterialCategory(string name) { Name = name; }

        public static MaterialCategory Metal { get { return new MaterialCategory("metal"); } }
        public static MaterialCategory Rock { get { return new MaterialCategory("rock"); } }
        public static MaterialCategory Organic { get { return new MaterialCategory("organic"); } }
        public static MaterialCategory None { get { return new MaterialCategory("none"); } }
    }
}
