using Microsoft.Xna.Framework.Content;

namespace RetroRedo.Content
{
    public interface IContentChest
    {
        void Load();
        T Get<T>(string name);
        void SetContentManager(ContentManager contentManager);
    }
}