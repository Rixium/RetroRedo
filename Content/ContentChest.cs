using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Content
{
    public class ContentChest : IContentChest
    {
        private const string RootDirectory = "Content";
        private ContentManager _contentManager;

        public void SetContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _contentManager.RootDirectory = "Content";
        }
        
        public void Load()
        {
            _contentManager.Load<Texture2D>("Images/splash");
        }

        public T Get<T>(string name) => _contentManager.Load<T>(name);
    }
}