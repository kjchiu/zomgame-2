using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    public class Brush : ZSpriteBatch
    {
        private ZSpriteBatch spriteBatch;

        protected int X
        {
            get;
            private set;
        }

        protected int Y
        {
            get;
            private set;
        }

        protected Vector2 Offset
        {
            get;
            private set;
        }

        public Brush(ZSpriteBatch spriteBatch, Panel panel)
            : base(spriteBatch.GraphicsDevice)
        {
            this.spriteBatch = spriteBatch;
            X = panel.X;
            Y = panel.Y;
            Offset = new Vector2(X, Y);
        }

        public override void DrawLine(Microsoft.Xna.Framework.Vector2 a, Microsoft.Xna.Framework.Vector2 b)
        {
            spriteBatch.DrawLine(Offset + a, Offset + b);
        }
        public override void DrawLine(Vector2 a, Vector2 b, Color color)
        {
            spriteBatch.DrawLine(Offset + a, Offset + b, color);
        }
        public override void DrawLine(Vector2 a, Vector2 b, int thickness, Color color)
        {
            spriteBatch.DrawLine(Offset + a, Offset + b, thickness, color);
        }

        public override void DrawLines(IList<Vector2> vertices, int thickness, Color color)
        {
            spriteBatch.DrawLines(vertices.Select((vertex) => Offset + vertex).ToList(), thickness, color);
        }

        public override void DrawOutline(int x, int y, int width, int height, Color color)
        {
            spriteBatch.DrawOutline(X + x, Y + y, width, height, color);
        }

        public override void DrawOutline(int x, int y, int width, int height, int thickness, Color color)
        {
            spriteBatch.DrawOutline(X + x, Y + y, width, height, thickness, color);
        }
        public override void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            spriteBatch.DrawRectangle(X + x, Y + y, width, height, color);
        }

        public override void DrawRectangle(int x, int y, int width, int height, Color color, Color borderColor)
        {
            spriteBatch.DrawRectangle(X + x, Y + y, width, height, color, borderColor);
        }

        public override void DrawRectangle(int x, int y, int width, int height, Color color, int borderThickness, Color borderColor)
        {
            spriteBatch.DrawRectangle(X + x, Y + y, width, height, color, borderThickness, borderColor);
        }

        public new void DrawString(SpriteFont font, string line, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, line, Offset + position, color);
        }
    }
}
