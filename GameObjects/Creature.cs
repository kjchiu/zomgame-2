using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Zomgame
{
    public class Creature : GameObject
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT };
        public enum EntityState { IDLE, BUSY };

		private int iHealth;
        private int iStrength;

        protected EntityState iEntityState;
        protected Map.MoveObjectToDel iMove;

        /// <summary>
		/// Actual initialization constructor.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="imgLoc"></param>
		public Creature(string imgLoc) : base(imgLoc)
        {
            iEntityState = EntityState.IDLE;
			Name = "Entity-" + ThisID;
			Health = 10;
            Strength = 10;
        }

        public Map.MoveObjectToDel MoveTo
        {
            set { iMove = value; }
        }

        public int Strength
        {
            get { return iStrength; }
            set { iStrength = value; }
        }

        public EntityState State {
            get { return iEntityState; }
            set { iEntityState = value; }
        }
		
        public int Health
		{
			get { return iHealth; }
			set { iHealth = value; }
		}

		public void TakeDamage(int amount)
		{
			iHealth -= amount;
		}

        public virtual void Attack(Creature aDefender)
        {
        }

        public virtual void Attack(Prop aProp)
        {}

        public virtual void Move(MapBlock aDestination)
        {
        }

 
        public virtual void Die()
        {
            RemoveObject(this);
        }


    }
}
