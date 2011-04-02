using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Messaging
{
    interface IMessageListener
    {
        void HandleMessage(Message message);
    }
}
