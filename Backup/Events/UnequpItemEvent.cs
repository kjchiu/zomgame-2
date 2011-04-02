using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Abilities;

namespace Zomgame.Events
{
	class UnequipItemEvent : BaseEvent
	{
		private Player unequipper;
		private Item item;

		public UnequipItemEvent(Player nEquip, Item nItem)
		{
			unequipper = nEquip;
			item = nItem;
		}

		public override void fireEvent()
		{
			//MESSAGE HERE
			unequipper.UnequipItem(item);

			item.RemoveAbility(UnequipItemAbility.StaticName);

			item.AttachAbility(new EquipItemAbility());
			

			unequipper.State = Entity.EntityState.IDLE;
		}
	}
}
