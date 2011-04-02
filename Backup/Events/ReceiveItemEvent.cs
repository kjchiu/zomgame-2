using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Zomgame.Events
{
	class ReceiveItemEvent : BaseEvent
	{
		Player receiver;
		Item item;

		public ReceiveItemEvent(Player nReceiver, Item nItem)
		{
			receiver = nReceiver;
			item = nItem;
		}

		public override void fireEvent()
		{
			//give the player the item
			Trace.WriteLine(receiver.Name + " received the " + item.Name);
			receiver.Inventory.Add(item);
		}

	}
}
