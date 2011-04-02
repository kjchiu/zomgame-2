using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Zomgame.UI
{
    public class TextBox : Widget
    {
        protected IEnumerable<string> messages;

        private SpriteFont font;
        private TextReader textReader;
        private Color color;


        public TextBox(int x, int y, int width, int height, string _message)
            : this(x, y, width, height, new string[] { _message })
        {
            {
                int i;
            }
            {
                string i;
            }
        }

        public TextBox(int x, int y, int width, int height, IEnumerable<string> _messages)
            : this(x, y, width, height, _messages, GraphicsDispenser.GetFont("Default"), Color.Gold)
        {
            
        }

        public TextBox(int x, int y, int width, int height, IEnumerable<string> _messages, SpriteFont _font, Color _color)
            : base(x, y, width, height)
        {
            font = _font;
            textReader = new TextReader(font, width);
            messages = _messages;            
        }

        public override void Draw(ZSpriteBatch spriteBatch)
        {
            int currY;
            currY = y;
            spriteBatch.DrawRectangle(X, Y, width, height, Color.Black);
            foreach (string msg in messages)
            {
                foreach (string line in textReader.Lines(msg))
                {
                    currY += font.LineSpacing;
                    if (currY > Y + Height)
                        return;
                    else
                    {
                        spriteBatch.DrawString(font, line, new Vector2(X, currY), color);
                    }
                }
            }
        }


    }
}
