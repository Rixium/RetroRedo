using Microsoft.Xna.Framework.Audio;
using RetroRedo.Commands;
using RetroRedo.Content;
using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public class WaitCommand : ICommand
    {
        public void Do(IEntity entity)
        {
            ContentChest.Get<SoundEffect>("Sounds/skip").Play();
            // Does nothing, just waiting   
        }

        public void Undo(IEntity entity)
        {
            // Same thing
        }
    }
}