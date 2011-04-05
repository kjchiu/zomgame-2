using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Zomgame.Factories;

namespace Zomgame.Events
{
	class AttackPropEvent : BaseEvent
	{
		Creature attacker;
		Prop defender;

		public AttackPropEvent(Creature nAttacker, Prop nDefender)
		{
			attacker = nAttacker;
			defender = nDefender;
		}

		public override void fireEvent()
		{
			if (defender != null)
			{
				attacker.State = Creature.EntityState.IDLE;
				Trace.WriteLine(attacker.Name + " has attacked " + defender.Name);
				//for now, make it simple

				defender.Durability -= 10;

				if (defender.Durability <= 0)
				{
					EventHandler.Instance.AddEvent(EventFactory.CreateDestroyPropEvent(defender));
				}
			}

		}
	}
}
