﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroRedo.Content;
using RetroRedo.Window;

namespace RetroRedo.Screen
{
    public class ScreenService : IScreenService
    {
        
        private IScreen _currentScreen = new BlankScreen();

        private bool _transitioningOut;
        private bool _transitioningIn;
        private float _currentTransitionAlpha;

        private IScreen CurrentScreen
        {
            get => _currentScreen;
            set
            {
                if (value != null)
                {
                    _currentScreen = value;
                }
            }
        }

        private float TransitionSpeed { get; } = 10f;
        private IScreen NextScreen { get; set; }

        public void SetNextScreen(IScreen screen)
        {
            NextScreen = screen;
            NextScreen.RequestScreenChange = OnScreenChangeRequest;
        }

        private void OnScreenChangeRequest(IScreen screen)
        {
            SetNextScreen(screen);
            _transitioningOut = false;
            _transitioningIn = false;
        }

        public void UpdateScreen(float delta)
        {
            if (_transitioningOut)
            {
                _currentTransitionAlpha += delta * TransitionSpeed;
                
                if (!(_currentTransitionAlpha >= 1)) 
                    return;
                
                _transitioningOut = false;
                _currentScreen?.FadedOut();
                GoToNextScreen();
                _transitioningIn = true;
            }
            else if (_transitioningIn)
            {
                _currentTransitionAlpha -= delta * TransitionSpeed;
                
                if (!(_currentTransitionAlpha <= 0)) 
                    return;
                
                _transitioningIn = false;
                _transitioningOut = false;
            }
            else
            {
                if (CurrentScreen.Ended)
                {
                    _transitioningOut = true;
                }
                else
                {
                    CurrentScreen.Update(delta);
                }
            }
        }


        private void GoToNextScreen()
        {
            CurrentScreen = NextScreen;
            CurrentScreen.Begin();
            NextScreen = null;
        }

        public void RenderScreen(SpriteBatch spriteBatch)
        {
            CurrentScreen.Render(spriteBatch);

            if (!_transitioningIn && !_transitioningOut) 
                return;
            
            spriteBatch.Begin();
            spriteBatch.Draw(ContentChest.Get<Texture2D>("Images/pixel"),
                new Rectangle(0, 0, WindowSettings.WindowWidth, WindowSettings.WindowHeight),
                Color.Black * _currentTransitionAlpha);
            spriteBatch.End();
        }
    }
}