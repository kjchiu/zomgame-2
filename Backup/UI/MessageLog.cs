using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;

namespace Zomgame.UI
{
    public class MessageLog : TextBox, IMessageListener
    {       
        protected Queue<string> buffer;

        /// <summary>
        /// Current place in the list.  Scrolling up/down modifies this.
        /// </summary>
        //protected int index;
        //protected int next;
        //protected int numVisibleLines;
        protected int margin;
        protected Color color;
        protected int bufferSize;

        /// <summary>
        /// True implies buffer is full, and its time to start rolling over.
        /// </summary>
        protected Boolean filled;
        public static SpriteFont Font;

        public MessageLog(int x, int y, int width, int height)
            : this(x, y, width, height, new Color(0xEB, 0xC0, 0x2C))
        {
        }

        public MessageLog(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, String.Empty)
        {
            MessageBus.Instance.AddListener(this);
            int textHeight = (int)Font.MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!?").Y;
            this.bufferSize = (int)Math.Floor((float)(height - (2 * margin)) / textHeight);
            this.color = color;
            buffer = new Queue<string>();
            base.messages = buffer as IEnumerable<string>;
        }

        /// <summary>
        /// Append message to message buffer.  Overwrites old messages if buffer is full.
        /// </summary>
        /// <param name="message">Message to add to log</param>
        void IMessageListener.HandleMessage(Message message)
        {
            buffer.Enqueue(String.Format("{0}: {1}", DateTime.Now.TimeOfDay, message.Text));
            if (buffer.Count > bufferSize)
            {
                buffer.Dequeue();
            }
        }

        //public override void Draw(ZSpriteBatch spriteBatch)
        //{
        //    //Vector2 anchor = new Vector2(x + margin, y + margin);
        //    //Vector2 lineSpacing = new Vector2(0, Font.LineSpacing);
        //    //spriteBatch.DrawRectangle(x, y, width, height, Color.Black);

        //    //foreach (string msg in buffer)
        //    //{
        //    //    spriteBatch.DrawString(MessageLog.Font, msg, anchor, color);
        //    //    anchor += lineSpacing;
        //    //}
            
        //}

        public IEnumerable<string> Messages
        {
            get { return buffer; }
        }

    }
}
