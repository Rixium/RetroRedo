using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public interface IComponent
    {
        IEntity Entity { get; set; }
        void Update();
    }
}