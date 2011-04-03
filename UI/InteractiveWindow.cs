using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zomgame.Events;

namespace Zomgame.UI
{
    abstract class InteractiveWindow : Window
    {
        public override bool HandlesInput
        {
            get
            {
                return true;
            }
        }

        public InteractiveWindow(int x, int y, int width, int height, Screen screen)
            : base(x, y, width, height, screen)
        { }
      
    }

    abstract class InteractiveWindow<T> : InteractiveWindow
    {

        public InteractiveWindow(int x, int y, int width, int height, Screen screen)
            : base(x, y, width, height, screen)
        { }

        public override void Update(GameTime gameTime, InputHandler input)
        {
            T arg = ProcessKey(input);
            Update(gameTime, arg);
        }
        public abstract void Update(GameTime gameTime, T arg);

        public abstract T ProcessKey(InputHandler input);
    }
}
