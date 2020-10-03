namespace RetroRedo.Data
{
    public class TiledMap
    {
        public int MapId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public TiledMapLayer[] Layers { get; set; }
        public TiledMapProperty[] Properties { get; set; }
    }
}