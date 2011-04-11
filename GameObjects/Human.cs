using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.GameObjects
{
    public abstract class Human : Creature
    {
        protected List<Item> iInventory;

        public Human(String aImgLoc)
            : base (aImgLoc)
        {}

        public void PickUpItem(Item aItem)
        {
            // Check for bulk, weight, etc
            //

            // Place in inventory
            //
            iInventory.Add(aItem);

            
        }

        public void DropItem(Item aItem)
        {
            iInventory.Remove(aItem);
            Location.AddObject(aItem);
        }

        public List<Item> Inventory
        {
            get { return iInventory; }
            set { iInventory = value; }
        }

    }
}
