/*
 * Item
 * 
 * An item is anything that can be picked up and placed in the inventory.
 * 
 */

using System.Collections.Generic;
using Zomgame.Abilities;

namespace Zomgame {
    public class Item: GameObject {
        IDictionary<string, Ability> abilities;
        
        public Item() : this("null_bmp") { }

        public Item(string imgLoc) : base(imgLoc) {
            Name = "Item-" + ThisID;
            abilities = new Dictionary<string, Ability>();
            AttachAbility(new DropAbility());
        }

        public void AttachAbility(Ability ability)
        {
            ability.AttachedItem = this;
            abilities.Add(ability.Name, ability);
        }

        public void RemoveAbility(string key)
        {
            abilities.Remove(key);
        }

        public Dictionary<string, Ability> Abilities
        {
            get { return (Dictionary<string, Ability>)abilities; }
        }
    }
}
