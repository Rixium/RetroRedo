using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public interface IComponent
    {
        IEntity Entity { set; }
        void Begin();
        void Update();
    }
}