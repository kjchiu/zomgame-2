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
        ICollection<Panel> panels;
        ICollection<InteractivePanel> interactivePanels;
        ICollection<Panel> purgeList;

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

        public Screen()
        {
            panels = new List<Panel>();
            interactivePanels = new LinkedList<InteractivePanel>();
            purgeList = new LinkedList<Panel>();
        }

        public void Draw(GameTime gameTime, ZSpriteBatch spriteBatch)
        {
            foreach (var panel in Panels)
            {
                panel.Draw(spriteBatch);
            }            
        }

        public void Update(GameTime gameTime, InputHandler input)
        {
            foreach(var panel in interactivePanels)
            {
                panel.Update(gameTime, input);
            }
            try
            {
                foreach (var panel in purgeList)
                {
                    panels.Remove(panel);
                    var interactivePanel = panel as InteractivePanel;
                    if (interactivePanels != null) interactivePanels.Remove(interactivePanel);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            purgeList.Clear();
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
            purgeList.Add(panel);
        }
    }
}
