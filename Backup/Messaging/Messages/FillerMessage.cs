using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame.Messaging.Messages
{
    class FillerMessage : Message
    {
        public FillerMessage()
            : base("hello", Color.Red)
        {
        }
    }
}
