using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.Messaging
{
    public abstract class Message
    {
        protected string text;
        protected Color color;

#region " Properties "
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
#endregion

#region " Constructor "

        protected Message(string text) : this(text, Color.White) 
        { }            

        protected Message(string text, Color color)
        {
            Text = text;
            Color = color;
        }
#endregion
    }
}
