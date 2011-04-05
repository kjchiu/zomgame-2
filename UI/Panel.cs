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
            get { return Color.Black; }
        }

        public virtual Color BorderColour
        {
            get { return Color.DarkGray; }
        }

        public Panel(int x, int y, int width, int height, Screen screen)
            : base(x, y, width, height)
        {
            margin = 5;
            border = 1;
            Screen = screen;
        }

        public virtual bool HandlesInput
        {
            get { return false; }
        }

        public override void Draw(ZSpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(X, Y, Width, Height, Color.Black, this.border, Color.DarkGray);
            Brush brush = new Brush(spriteBatch, this);
            brush.DrawLine(new Vector2(0, 0), new Vector2(50, 50), Color.Red);
            spriteBatch.DrawLine(new Vector2(0, 0), new Vector2(50, 50), Color.Red);
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
