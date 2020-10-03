namespace RetroRedo.Entities
{
    public class Player : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}