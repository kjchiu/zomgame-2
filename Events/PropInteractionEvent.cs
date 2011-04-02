using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Events
{
    class PropInteractionEvent : BaseEvent
    {
        Prop prop;
        Player player;

        public PropInteractionEvent(Prop nProp, Player nPlayer)
        {
            prop = nProp;
            player = nPlayer;
        }

        public override void fireEvent()
        {
            if (prop != null)
            {
                prop.Interact(player);
            }
        }
    }
}
