using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Content
{
    public static class ContentChest
    {
        private static ContentManager _contentManager;

        public static void SetContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _contentManager.RootDirectory = "Content";
        }

        public static void Load()
        {
            // Images
            _contentManager.Load<Texture2D>(Path.Combine("Images", "splash"));
            _contentManager.Load<Texture2D>(Path.Combine("Images", "pixel"));
            _contentManager.Load<Texture2D>(Path.Combine("Images", "tiles_1"));

            // Fonts
            _contentManager.Load<SpriteFont>(Path.Combine("Fonts", "MainFont"));
            _contentManager.Load<SpriteFont>(Path.Combine("Fonts", "TitleFont"));
            
            // Sounds
            _contentManager.Load<SoundEffect>(Path.Combine("Sounds", "Walk"));
        }

        public static T Get<T>(string name) => _contentManager.Load<T>(name);
    }
}