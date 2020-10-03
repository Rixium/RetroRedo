namespace RetroRedo.Data
{
    public class Map
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public MapLayer[] Layers { get; set; }
        public MapProperty[] Properties { get; set; }
    }

    public class MapLayer
    {
        public int[] Data { get; set; }
    }

    public class MapProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    
}