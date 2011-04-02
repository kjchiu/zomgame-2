using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;

namespace Zomgame.Events
{
	class AttackSpaceEvent : BaseEvent
	{
		public Entity attacker;
		public MapBlock space;

		public AttackSpaceEvent(Entity nAttacker, MapBlock nSpace)
		{
			attacker = nAttacker;
			space = nSpace;
		}

		public override void fireEvent()
		{
			if (space.Entities.Count > 0)
			{
				EventHandler.Instance.AddEvent(EventFactory.CreateAttackEvent(attacker, space.Entities[0]));
			} else if (space.Props.Count > 0){
				EventHandler.Instance.AddEvent(EventFactory.CreateAttackPropEvent(attacker, space.Props[0]));
			}
		}
	}
}
