using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Zomgame.Graphics;

namespace Zomgame.UI
{
    public class TextBox : Panel
    {
        protected IEnumerable<string> messages;

        private SpriteFont font;
        private TextReader textReader;
        private Color color;


        public TextBox(int x, int y, int width, int height, Screen screen, string _message)
            : this(x, y, width, height, screen, new string[] { _message })
        {
        }

        public TextBox(int x, int y, int width, int height, Screen screen, IEnumerable<string> _messages)
            : this(x, y, width, height, screen, _messages, GraphicsDispenser.GetFont("Calibri"), Color.Gold)
        {
            
        }

        public TextBox(int x, int y, int width, int height, Screen screen, IEnumerable<string> _messages, SpriteFont _font, Color _color)
            : base(x, y, width, height, screen)
        {
            font = _font;
            textReader = new TextReader(font, width);
            messages = _messages;
            color = _color;
        }

        public override void DrawContent(Brush brush)
        {
            int currY = 0;

            foreach (string msg in messages)
            {
                foreach (string line in textReader.Lines(msg))
                {                    
                    if (currY > Y + Height)
                        return;
                    else
                    {
                        brush.DrawString(font, line, new Vector2(5, currY), color);
                    }
                }
                currY += font.LineSpacing;
            }
        }


    }
}
