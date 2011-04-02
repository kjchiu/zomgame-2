using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zomgame.Items;
using Zomgame.Factories;
using Zomgame.Constants;

namespace Zomgame
{
    public class Player
        : Entity
    {
        protected Dictionary<string, Item> equipment;
		protected Dictionary<string, Item> defaultEquipment;

		protected Dictionary<string, Skill> skills;
		protected List<Item> inventory;
		protected Weapon defaultWeapon;

		public Player(Vector2 position, string imgLoc)
			: base(position, imgLoc)
		{
			base.velocity = 1;
			Name = "Player";
			inventory = new List<Item>();
			skills = new Dictionary<string, Skill>();
            equipment = new Dictionary<string, Item>();
			defaultEquipment = new Dictionary<string, Item>();
			defaultEquipment[EquipmentTypes.MELEE_WEAPON] = WeaponFactory.CreateFists();
		}

        public void AddSkill(Skill toAdd)
        {
            skills.Add(toAdd.Name, toAdd);
        }

        public int GetSkillValue(string skillName)
        {
            if (skills[skillName] != null)
            {
                return skills[skillName].Value;
            }
            return 0;
        }

        public void UseSkill(string skillName, int growth)
        {
            if (skills[skillName] != null)
            {
                skills[skillName].GrowBy(growth);
            }
        }

        public Item EquipmentIn(string location)
        {
            Item item;
            equipment.TryGetValue(location, out item);

			if (item == null)
			{
				return defaultEquipment[location];
			}

            return item;  
        }

        public void EquipItem(string location, Item equippedItem)
        {
            equipment[location] = equippedItem;
        }

		public void UnequipItem(Item item)
		{
			if (item is Weapon)
			{
				equipment[EquipmentTypes.MELEE_WEAPON] = defaultWeapon;
			}
		}

		public bool ItemIsEquipped(Item item)
		{
			if (equipment.ContainsValue(item))
			{
				return true;
			}
			return false;
		}

	


		public List<Item> Inventory
		{
			get { return inventory; }
			set { inventory = value; }
		}

		public Dictionary<string, Skill> Skills
		{
			get { return skills; }
		}

		public Dictionary<string, Item> DefaultEquipment
		{
			get { return defaultEquipment; }
		}
    }
}
