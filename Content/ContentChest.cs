using Microsoft.Xna.Framework.Content;

namespace RetroRedo.Content
{
    public class ContentChest : IContentChest
    {
        private const string RootDirectory = "Content";

        private readonly ContentManager _contentManager;

        public ContentChest(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _contentManager.RootDirectory = RootDirectory;
        }

        public void Load()
        {
        }

        public T Get<T>(string name) => _contentManager.Load<T>(name);
    }
}