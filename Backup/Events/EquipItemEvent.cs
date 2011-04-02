using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Abilities;
using Zomgame.Factories;

namespace Zomgame.Events
{
	class EquipItemEvent : BaseEvent
	{
		private Player equipper;
		private Item item;
		private string location;

		public EquipItemEvent(Player nEquip, Item nItem, string nLoc)
		{
			equipper = nEquip;
			item = nItem;
			location = nLoc;
		}

		public override void fireEvent()
		{
			Item currentlyEquipped = equipper.EquipmentIn(location);

			if (currentlyEquipped != null && currentlyEquipped != equipper.DefaultEquipment[location])
			{
				//Unequip the previously-equipped item
				EventHandler.Instance.AddEvent(EventFactory.CreateUnequipItemEvent(equipper, currentlyEquipped));
			}

			item.RemoveAbility(EquipItemAbility.StaticName);
			item.AttachAbility(new UnequipItemAbility());

			equipper.EquipItem(location, item);

			//MESSAGE HERE
			
			equipper.State = Entity.EntityState.IDLE;

			
		}
	}
}
