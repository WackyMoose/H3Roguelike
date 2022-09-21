using GameV1.Enums;

namespace GameV1.Entities
{
    public class ObjectEntity
    {
        public string? Name { get; set; }
        public ObjectTypes Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
