using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    public abstract class Widget
    {
        
        #region " Properties "
        /// <summary>
        /// x of top left corner
        /// </summary>
        public int X
        {
            get;
            private set;
        }

        /// <summary>
        /// y of top left corner
        /// </summary>
        public int Y
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
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
