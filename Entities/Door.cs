namespace RetroRedo.Entities
{
    public class Door : Entity
    {
        public int DoorId { get; }

        public Door(int tileX, int tileY, int doorId)
        {
            DoorId = doorId;
            X = tileX;
            Y = tileY;
        }

        public override void Entered(IEntity other)
        {
            // Does nothing.
        }

        public void Toggle() => Blocking = !Blocking;

        public void Close() => Blocking = true;
    }
}