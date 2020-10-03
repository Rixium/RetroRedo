using RetroRedo.Entities;

namespace RetroRedo.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly int _xChange;
        private readonly int _yChange;

        public MoveCommand(int xChange, int yChange)
        {
            _xChange = xChange;
            _yChange = yChange;
        }

        public void Do(IEntity entity)
        {
            entity.X += _xChange;
            entity.Y += _yChange;
        }

        public void Undo(IEntity entity)
        {
            entity.X -= _xChange;
            entity.Y -= _yChange;
        }
    }
}