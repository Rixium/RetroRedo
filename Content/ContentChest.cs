using System.IO;
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
            // Images
            _contentManager.Load<Texture2D>(Path.Combine("Images", "splash"));
            
            // Fonts
            _contentManager.Load<SpriteFont>(Path.Combine("Fonts", "MainFont"));
            _contentManager.Load<SpriteFont>(Path.Combine("Fonts", "TitleFont"));
        }

        public T Get<T>(string name) => _contentManager.Load<T>(name);
    }
}