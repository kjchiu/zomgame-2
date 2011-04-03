using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Zomgame.UI
{
    /// <summary>
    /// Responsible for drawing and managing all UI elements related to a specific GameState
    /// </summary>
    public class Screen
    {
        ICollection<Window> windows;
        ICollection<InteractiveWindow> interactiveWindows;
        ICollection<Window> purgeList;

        public IEnumerable<Window> Windows
        {
            get
            {
                foreach (var window in windows)
                {
                    yield return window;
                }

                foreach (var window in interactiveWindows)
                {
                    yield return window;
                }
            }
        }

        public Screen()
        {
            windows = new List<Window>();
            interactiveWindows = new LinkedList<InteractiveWindow>();
            purgeList = new LinkedList<Window>();
        }

        public void Draw(GameTime gameTime, ZSpriteBatch spriteBatch)
        {
            foreach (var window in Windows)
            {
                window.Draw(spriteBatch);
            }            
        }

        public void Update(GameTime gameTime, InputHandler input)
        {
            foreach(var window in interactiveWindows)
            {
                window.Update(gameTime, input);
            }
            try
            {
                foreach (var window in purgeList)
                {
                    windows.Remove(window);
                    var interactiveWindow = window as InteractiveWindow;
                    if (interactiveWindows != null) interactiveWindows.Remove(interactiveWindow);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            purgeList.Clear();
        }

        public void AddWindow(Window window)
        {
            if (window is InteractiveWindow)
            {
                interactiveWindows.Add((InteractiveWindow)window);
            }
            else
            {
                windows.Add(window);
            }
        }

        public void RemoveWindow(Window window)
        {
            purgeList.Add(window);
        }
    }
}
