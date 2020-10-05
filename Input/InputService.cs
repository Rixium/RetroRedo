using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace RetroRedo.Input
{
    public class InputService
    {
        private KeyboardState _lastKeyboardState;

        private readonly IDictionary<Keys, Action> _keyPressed = new Dictionary<Keys, Action>();

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            
            var keysPressed = keyboardState.GetPressedKeys()
                .Except(_lastKeyboardState.GetPressedKeys());

            var keyPressedActions =
                _keyPressed.Where(x => keysPressed.Contains(x.Key))
                    .Select(x => x.Value).ToImmutableList();

            foreach (var action in keyPressedActions)
            {
                action?.Invoke();
            }

            _lastKeyboardState = keyboardState;
        }

        public void OnKeyPressed(Keys keys, Action action) => _keyPressed[keys] = action;
        
        public void Reset()
        {
            _keyPressed.Clear();
        }
    }
}