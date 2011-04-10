using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zomgame.Graphics;

namespace Zomgame
{
    public class ZSpriteBatch : Microsoft.Xna.Framework.Graphics.SpriteBatch
    {

        public ZSpriteBatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            
        }

        

        public virtual void DrawLines(IList<Vector2> vertices, int thickness , Color color)
        {
            
            for(int i=0; i < vertices.Count; ++i)
            {
                this.DrawLine(vertices[i], vertices[(i+1) % vertices.Count], thickness, color);
            }
        }

        public virtual void DrawLine(Vector2 a, Vector2 b)
        {
            this.DrawLine(a, b, Color.White);
        }

        public virtual void DrawLine(Vector2 a, Vector2 b, Color color)
        {
            this.DrawLine(a, b, 1, color);
        }

        public virtual void DrawLine(Vector2 a, Vector2 b, int thickness, Color color)
        {
            Vector2 start;
            Vector2 end;
            if (a.X < b.X)
            {
                start = a;
                end = b;
            }
            else
            {
                start = b;
                end = a;
            }
            float angle =(float)Math.Atan2((double)(end.Y - start.Y),
                    (double)(end.X - start.X));


            //this.Draw(GraphicsDispenser.getTexture("pixel_bmp"), start, new Vector2(a.X + (b - a).Length, a.Y)
            this.Draw(GraphicsDispenser.GetTexture("pixel_bmp")
                //, new Rectangle(start.X, start.Y, (int)((end - start).Length()), start.Y)
                , new Rectangle((int)start.X
                    , (int)start.Y
                    , (int)((end - start).Length())
                    , thickness)
                , null
                , color
                , angle
                , Vector2.Zero
                , SpriteEffects.None
                , 0f);
                     
        }

        public virtual void DrawOutline(int x, int y, int width, int height, Color color)
        {
            this.DrawOutline(x, y, width, height, 1, color);
        }

        public virtual void DrawOutline(int x, int y, int width, int height, int thickness, Color color)
        {
            Vector2[] vertices = new Vector2[] { new Vector2(x, y)
                                               , new Vector2(x + width, y)
                                               , new Vector2(x + width, y + height)
                                               , new Vector2(x, y+height)};
            //this.DrawLines(vertices, thickness, color);
            this.DrawLine(vertices[0], vertices[1], thickness, color);
            this.DrawLine(vertices[1], vertices[2], thickness, color);
            this.DrawLine(vertices[3], vertices[0], thickness, color);
            this.DrawLine(new Vector2(x - thickness, y + height - thickness), new Vector2(x + width, y + height - thickness), thickness, color);

        }



        public virtual void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            this.DrawLine(new Vector2(x, y), new Vector2(x + width, y), height, color);
        }

        public virtual  void DrawRectangle(int x, int y, int width, int height, Color color, Color borderColor)
        {
            this.DrawRectangle(x, y, width, height, color, 1, borderColor);
        }

        public virtual void DrawRectangle(int x, int y, int width, int height, Color color, int borderThickness, Color borderColor)
        {
            this.DrawLine(new Vector2(x, y), new Vector2(x + width, y), height, color);
            this.DrawOutline(x, y, width, height, borderThickness, borderColor);
        }

    }
}
