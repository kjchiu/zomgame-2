using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    public abstract class Panel : Widget, IDisposable
    {
        private int margin;
        private int border;
        protected Screen Screen
        {
            get;
            private set;
        }

        public virtual Color BackgroundColour
        {
            get;
            protected set;
        }

        public virtual Color BorderColour
        {
            get;
            protected set;
        }

        public Panel(int x, int y, int width, int height, Screen screen)
            : this(x, y, width, height, Color.Black, Color.DarkGray, screen)
        { }

        public Panel(int x, int y, int width, int height, Color backgroundColour, Color BorderColour, Screen screen)
            : base(x, y, width, height)
        {
            margin = 5;
            border = 1;
            Screen = screen;
            BackgroundColour = backgroundColour;
            BorderColour = BorderColour;
        }

        public virtual bool HandlesInput
        {
            get { return false; }
        }

        public override void Draw(ZSpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(X, Y, Width, Height, BackgroundColour, this.border, BorderColour);
            Brush brush = new Brush(spriteBatch, this);
            DrawContent(brush);

        }

        public abstract void DrawContent(Brush brush);

        public virtual void Update(GameTime gameTime, InputHandler input)
        {
        }

        public virtual void Close()
        {
            Screen.RemovePanel(this);
        }

        public void Dispose()
        {
            if (Screen != null)
            {
                Screen.RemovePanel(this);
            }
        }
    }
}
