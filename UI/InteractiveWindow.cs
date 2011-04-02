using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zomgame.Events;

namespace Zomgame.UI
{
    abstract class InteractiveWindow<T> : Window
    {
        public override bool HandlesInput
        {
            get
            {
                return true;
            }
        }

        public InteractiveWindow(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

       
        public abstract void Update(GameTime gameTime, T arg);

        public override void Update(GameTime gameTime)
        {
            T arg = ProcessKey(InputHandler.Instance);
            Update(gameTime, arg);
        }

        public abstract T ProcessKey(InputHandler input);
    }
}
