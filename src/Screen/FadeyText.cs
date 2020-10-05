using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroRedo.Screen
{
    public class FadeyText
    {
        private const float FadeSpeed = 0.5f;
        private readonly float _minAlpha;
        private readonly string _text;
        private readonly SpriteFont _font;
        private readonly Vector2 _position;
        private readonly Vector2 _textSize;

        private float _alpha;
        private bool _fadingOut;

        public FadeyText(string text, SpriteFont font, Vector2 position, float minAlpha = 0.25f)
        {
            _text = text;
            _font = font;
            _position = position;
            _minAlpha = minAlpha;
            _textSize = font.MeasureString(text);
            _alpha = minAlpha;
        }

        public void Update(float delta)
        {
            if (_fadingOut)
            {
                _alpha -= FadeSpeed * delta;

                if (_alpha <= _minAlpha)
                {
                    _fadingOut = false;
                }
            }
            else
            {
                _alpha += FadeSpeed * delta;

                if (_alpha >= 1)
                {
                    _fadingOut = true;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text,
                _position - _textSize / 2,
                Color.White * _alpha);
        }
    }
}