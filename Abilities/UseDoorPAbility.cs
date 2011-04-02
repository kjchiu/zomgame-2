using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Props;

namespace Zomgame.Abilities
{
    class UseDoorPAbility : PropAbility
    {
        public UseDoorPAbility(Prop attached)
        {
            AttachedProp = attached;
            PropAction = new PerformAction(Invoke);
        }

        public void Invoke(Player p)
        {
            //Assuming it's a door
            ((Door)AttachedProp).ToggleOpen();
        }
    }
}
