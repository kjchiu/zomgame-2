using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zomgame.Messaging.Messages
{
    class DominatingMessage : Message
    {
        public DominatingMessage(Entity dominator, Entity dominatee, Item item)
            : base(String.Format("{0} is dominating {1} with the {2}", dominator.Name, dominatee.Name, item.Name), Color.Red)
        {
        }
    }
}
