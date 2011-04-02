using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;
using System.Diagnostics;

namespace Zomgame.Events
{
	class PickupItemEvent : BaseEvent
	{
		Player picker;
		Item item;

		public PickupItemEvent(Player nPicker, Item nItem)
		{
			picker = nPicker;
			item = nItem;
		}

		public override void fireEvent()
		{
			//Check to see if the player can pick up the item

			//if so, then create a ReceiveItemEvent
			Trace.WriteLine(picker.Name + " is picking up the " + item.Name);
			EventHandler.Instance.AddEvent(EventFactory.CreateReceiveItemEvent(picker, item));
			//then remove it from the mapBlock
			item.Location.RemoveObject(item);
            
			//else...don't. Create an error message or something.
		}
	}
}
