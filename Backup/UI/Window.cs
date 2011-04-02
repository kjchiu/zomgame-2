using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    public class Window : Widget
    {
        private int margin;
        private int border;

        public Window(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            margin = 5;
            border = 1;
        }


        public override void Draw(ZSpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(X, Y, Width, Height, Color.Black, this.border, Color.DarkGray);
        }
    }
}
