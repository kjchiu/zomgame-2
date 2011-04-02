using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.UI
{
    class TextReader
    {
        private int width;
        private SpriteFont font;

        public TextReader(SpriteFont _font, int _width)
        {
            font = _font;
            width = _width;
        }

        public IEnumerable<string> Lines(string paragraph)
        {
            int i = 0;
            while (i < paragraph.Length)
            {
                StringBuilder sb = new StringBuilder();
                while (font.MeasureString(sb).X < width && i < paragraph.Length)
                {
                    sb.Append(paragraph[i]);
                    ++i;
                }
                yield return sb.ToString();
            }
            
        }
    }
}
