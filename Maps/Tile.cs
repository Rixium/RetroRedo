namespace RetroRedo.Maps
{
    public class Tile
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public bool Collidable { get; set; }
        public bool IsWin { get; set; }

        public Tile(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}