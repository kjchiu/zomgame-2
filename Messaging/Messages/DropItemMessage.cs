using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Messaging.Messages
{
    class DropItemMessage : Message
    {

        public DropItemMessage(Item item)
            : base(String.Format("You have dropped {0}", item.Name))
        {
        }
    }
}
