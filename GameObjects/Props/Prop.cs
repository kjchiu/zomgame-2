/*
 * Prop
 * 
 * A prop is anything that can be interacted with that can't be picked up.
 * 
 */

using System.Collections.Generic;
using Zomgame.Abilities;

namespace Zomgame {
    public class Prop: GameObject {
        private int durability; //health of the prop
		private bool passable; //whether the prop can be walked through or not
		private bool seeThrough; //whether you can see through the prop

        private PropAbility interaction;

        public Prop():this("null_bmp") { }

        public Prop(string imgLoc) : base(imgLoc) {
			Name = "Prop-" + ThisID;
			durability = 100;
			passable = false;
			seeThrough = false;
		}

        public int Durability {
            get { return durability; }
            set { durability = value; }
        }

		public virtual bool Passable
		{
			get { return passable; }
			set { passable = value; }
		}

		public virtual bool SeeThrough
		{
			get { return seeThrough; }
			set { seeThrough = value; }
		}

        public PropAbility Interaction
        {
            get { return interaction; }
            set { interaction = value; }
        }

        public bool IsInteractive
        {
            get
            {
                return (interaction != null);
            }
        }

        public void Interact(Player p)
        {
            if (interaction != null)
            {
                interaction.PropAction(p);
            }
        }
    }
}
