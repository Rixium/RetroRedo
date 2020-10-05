using Microsoft.Xna.Framework.Audio;
using RetroRedo.Content;
using RetroRedo.Entities;

namespace RetroRedo.Commands
{
    public class WaitCommand : ICommand
    {
        public void Do(IEntity entity)
        {
            ContentChest.Get<SoundEffect>("Sounds/skip").Play();
            entity.Wait();
        }

        public void Undo(IEntity entity)
        {
            // Same thing
        }
    }
}