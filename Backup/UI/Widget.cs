using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    public abstract class Widget
    {
        /// <summary>
        /// x of top left corner
        /// </summary>
        protected int x;

        /// <summary>
        /// y of top left corner
        /// </summary>
        protected int y;

        protected int height;
        protected int width;

        #region " Properties "
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y 
        { 
            get { return y; }
            set { y = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        #endregion


        public Widget(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
        public abstract void Draw(ZSpriteBatch spriteBatch);
    }
}
