using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zomgame.UI
{
    class Manager
    {
        IEnumerable<Window> windows;
        Stack<Window> modalWindows;

        protected IEnumerable<Window> Windows
        {
            get
            {
                foreach (var window in windows)
                {
                    yield return window;
                }

                foreach (var window in modalWindows)
                {
                    yield return window;
                }
            }
        }

        public Manager()
        {
            windows = new List<Window>();
            modalWindows = new Stack<Window>();
        }

        void Draw(GameTime gameTime, ZSpriteBatch spriteBatch)
        {
            foreach (var window in Windows)
            {
                window.Draw(spriteBatch);
            }
        }

        void Update(GameTime gameTime)
        {
            
        }
    }
}
