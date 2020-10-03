using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public interface IComponent
    {
        IEntity Entity { get; set; }
        void Begin();
        void Update();
    }
}