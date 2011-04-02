using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Abilities;
using Zomgame.Constants;

namespace Zomgame.Items
{
    public class Weapon :Item
    {
        int damage;

        public Weapon(int nDam, string imgLoc)
            : base(imgLoc)
        {
            damage = nDam;
			AttachAbility(new EquipItemAbility());
        }


        public int Damage
        {
            get { return damage; }
            set { if (value > 0) { damage = value; } }
        }
    }
}
