namespace RetroRedo.Data
{
    public class TiledMapObject
    {
        public float X { get; set; }
        public float Y { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public TiledMapProperty[] Properties { get; set; }
    }
}