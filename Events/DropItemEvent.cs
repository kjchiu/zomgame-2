using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;
using Zomgame.Factories;

namespace Zomgame.Events
{
    class DropItemEvent : BaseEvent
    {
        Player picker;
        Item item;

        public DropItemEvent(Player _picker, Item _item)
        {
            picker = _picker;
            item = _item;
        }

        public override void fireEvent()
        {
            if (!picker.Inventory.Contains(item))
            {
                throw new InvalidOperationException("Tried to drop item not in player's inventory.");
            }
            
            Trace.WriteLine(String.Format("{0} is dropping the {1}", picker.Name, item.Name));

            if (picker.ItemIsEquipped(item))
            {
                EventHandler.Instance.AddEvent(EventFactory.CreateUnequipItemEvent(picker, item));
            }
            
            picker.State = Creature.EntityState.IDLE;

            picker.Location.AddObject(item);
            picker.Inventory.Remove(item);
            MessageBus.Instance.AddMessage(new DropItemMessage(item));
        }
    }
}
