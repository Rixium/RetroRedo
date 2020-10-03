using RetroRedo.Commands;
using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public class WaitCommand : ICommand
    {
        public void Do(IEntity entity)
        {
            // Does nothing, just waiting   
        }

        public void Undo(IEntity entity)
        {
            // Same thing
        }
    }
}