using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace RetroRedo.Input
{
    public class InputService : IInputService
    {
        private KeyboardState _lastKeyboardState;

        private readonly IDictionary<Keys, Action> _keyPressed = new Dictionary<Keys, Action>();
        private readonly IDictionary<Keys, Action> _keyReleased = new Dictionary<Keys, Action>();
        private readonly IDictionary<Keys, Action> _keyHeld = new Dictionary<Keys, Action>();

        public void Update()
        {
            var keyboardState = Keyboard.GetState();

            var keysHeld = keyboardState.GetPressedKeys()
                .Intersect(_lastKeyboardState.GetPressedKeys());

            var keysReleased = _lastKeyboardState.GetPressedKeys()
                .Except(keyboardState.GetPressedKeys());

            var keysPressed = keyboardState.GetPressedKeys()
                .Except(_lastKeyboardState.GetPressedKeys());

            var keyPressedActions =
                _keyPressed.Where(x => keysPressed.Contains(x.Key))
                    .Concat(_keyHeld.Where(x => keysHeld.Contains(x.Key)))
                    .Concat(_keyReleased.Where(x => keysReleased.Contains(x.Key)))
                    .Select(x => x.Value);

            foreach (var action in keyPressedActions)
            {
                action?.Invoke();
            }

            _lastKeyboardState = keyboardState;
        }

        public void OnKeyPressed(Keys keys, Action action) => _keyPressed[keys] = action;

        public void OnKeyHeld(Keys keys, Action action) => _keyHeld[keys] = action;

        public void OnKeyReleased(Keys keys, Action action) => _keyReleased[keys] = action;

        public void Reset()
        {
            _keyPressed.Clear();
            _keyHeld.Clear();
            _keyReleased.Clear();
        }
    }
}