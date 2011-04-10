using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;

namespace Zomgame.Events
{
	class AttackSpaceEvent : BaseEvent
	{
		public Creature attacker;
		public MapBlock space;

		public AttackSpaceEvent(Creature nAttacker, MapBlock nSpace)
		{
			attacker = nAttacker;
			space = nSpace;
		}

		public override void fireEvent()
		{
			if (space.CreatureInBlock != null)
			{
				EventHandler.Instance.AddEvent(EventFactory.CreateAttackEvent(attacker, space.CreatureInBlock));
			} else if (space.PropInBlock != null){
                EventHandler.Instance.AddEvent(EventFactory.CreateAttackPropEvent(attacker, space.PropInBlock));
			}
		}
	}
}
