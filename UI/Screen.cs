using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Zomgame.States;
using System.Threading;

namespace Zomgame.UI
{
    /// <summary>
    /// Responsible for drawing and managing all UI elements related to a specific GameState
    /// </summary>
    public class Screen
    {
        ICollection<Panel> panels;
        ICollection<InteractivePanel> interactivePanels;        
        ICollection<InteractivePanel> dialogs;

        ICollection<Panel> panelPurgeList;
        ICollection<InteractivePanel> dialogPurgeList;

        

        internal GameState GameState
        {
            get;
            private set;
        }
        public IEnumerable<Panel> Panels
        {
            get
            {
                foreach (var panel in panels)
                {
                    yield return panel;
                }

                foreach (var panel in interactivePanels)
                {
                    yield return panel;
                }
            }
        }

        public Screen(GameState state)
        {
            panels = new List<Panel>();
            interactivePanels = new LinkedList<InteractivePanel>();
            panelPurgeList = new LinkedList<Panel>();
            dialogs = new LinkedList<InteractivePanel>();
            dialogPurgeList = new List<InteractivePanel>();
            GameState = state;
        }

        public void Bind(GameState state)
        {
            GameState = state;
        }
        public void Draw(GameTime gameTime, ZSpriteBatch spriteBatch)
        {
            foreach (var panel in Panels)
            {
                panel.Draw(spriteBatch);
            }            
        }

        public void UpdateAsync(GameTime gameTime, InputHandler input)
        {            
            
            ThreadPool.QueueUserWorkItem(
                (state) =>
                {
                    try
                    {
                        Update(gameTime, input);
                    }
                    finally
                    {
                        ((AutoResetEvent)state).Set();
                    }
                }
                , GameState.Semaphore);
        }

        public void Update(GameTime gameTime, InputHandler input)
        {
            foreach (var panel in interactivePanels)
            {
                panel.Update(gameTime, input);
            }
            try
            {
                foreach (var panel in panelPurgeList)
                {
                    panels.Remove(panel);
                    var interactivePanel = panel as InteractivePanel;
                    if (interactivePanels != null) interactivePanels.Remove(interactivePanel);
                }
                foreach (var dialog in dialogPurgeList)
                {
                    dialogs.Remove(dialog);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            finally
            {
                panelPurgeList.Clear();
                dialogPurgeList.Clear();
            }
        }

        public void AddPanel(Panel panel)
        {
            if (panel is InteractivePanel)
            {
                interactivePanels.Add((InteractivePanel)panel);
            }
            else
            {
                panels.Add(panel);
            }
        }

        public void RemovePanel(Panel panel)
        {
            panelPurgeList.Add(panel);
        }

        public void AddDialog(InteractivePanel dialog)
        {
            dialogs.Add(dialog);
        }

        public void RemoveDialog(InteractivePanel dialog)
        {
            
        }
    }
}
