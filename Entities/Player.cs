namespace RetroRedo.Entities
{
    public class Player : Entity
    {
        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Entered(IEntity other)
        {
            // Does nothing.
        }
    }
}