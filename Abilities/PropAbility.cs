using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Abilities
{
    public class PropAbility
    {
        public delegate void PerformAction(Player player); //the delegate type
		public PerformAction PropAction;
		
		Prop attachedProp;

		//Skill skill -> Soon
		
		public PropAbility() 
		{
		}

		public Prop AttachedProp
		{
            get { return attachedProp; }
            set { attachedProp = value; }
		}

		public virtual string Name
		{
			get { return "NoName"; }
		}
    }
}
