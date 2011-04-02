using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Zomgame {
    public class InputHandler {
        private KeyboardState currentKeyState;
		private KeyboardState previousKeyState;
		private MouseState currentMouseState;
		private MouseState previousMouseState;

		private static InputHandler instance = null;

		private InputHandler()
		{
            currentKeyState = Keyboard.GetState();
            previousKeyState = currentKeyState;
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
        }

		public static InputHandler Instance{
			get
			{
				if (instance == null)
				{
					instance = new InputHandler();
				}
				return instance;
			}
		}

        /// <summary>
        /// Determines if a key is pushed, but does not allow holding it down.
        /// </summary>
        /// <param name="key">The key in question.</param>
        /// <returns>Whether or not the Key has been pushed once.</returns>
        public bool IsKeyPushed(Keys key){
            if (!previousKeyState.IsKeyDown(key)) {
                return currentKeyState.IsKeyDown(key);
            }
            return false;
        }

		/// <summary>
		/// Determines if an array of keys is pushed, but does not allow holding them down.
		/// </summary>
		/// <param name="keys">The keys in question. keys[0] is always assumed to be the modifier ie ctrl/alt/shift</param>
		/// <returns>Whether or not the non-modifying key has been pushed once.</returns>
		public bool IsKeyPushed(Keys[] keys)
		{
			if ( IsKeyHeld(keys[0]) && IsKeyPushed(keys[1]) ){
				return true;
			}
			
			return false;
		}

        /// <summary>
        /// Determines if a key is currently being held down.
        /// </summary>
        /// <param name="key">The key is question</param>
        /// <returns>Whether or not the Key is being held</returns>
        public bool IsKeyHeld(Keys key) {
            return Keyboard.GetState().IsKeyDown(key);
        }

        /// <summary>
        /// Updates the keyboard states. Important for the 'previousState' variable.
        /// </summary>
        public void UpdateStates() {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
    }
}
