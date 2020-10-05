// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace RetroRedo.Data
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TiledMapLayer
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Data { get; set; }
        public TiledMapObject[] Objects { get; set; }
    }
}