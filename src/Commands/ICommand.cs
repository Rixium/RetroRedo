using RetroRedo.Entities;

namespace RetroRedo.Commands
{
    public interface ICommand
    {
        void Do(IEntity entity);
        void Undo(IEntity entity);
    }
}