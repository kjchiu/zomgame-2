using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zomgame.Items;
using Zomgame.Factories;
using Zomgame.Constants;
using System.Diagnostics;
using Zomgame.GameObjects;

namespace Zomgame
{
    public class Player
        : Human
    {
        protected Dictionary<string, Item> equipment;
		protected Dictionary<string, Item> defaultEquipment;

		protected Dictionary<string, Skill> skills;
		
		protected Weapon defaultWeapon;

		public Player(string imgLoc)
			: base(imgLoc)
		{
			Name = "Player";
			iInventory = new List<Item>();
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

		public Dictionary<string, Skill> Skills
		{
			get { return skills; }
		}

		public Dictionary<string, Item> DefaultEquipment
		{
			get { return defaultEquipment; }
		}

        public override void Attack(Creature aEnemy)
        {

            Trace.WriteLine("Player has attacked " + aEnemy.Name);
            //for now, make it simple
            int damage = Strength;
            //get damage based on weapon
           damage += ((Weapon)(((Player)this).EquipmentIn(EquipmentTypes.MELEE_WEAPON))).Damage;
           Trace.WriteLine("Player attacks zombie with " + EquipmentIn(EquipmentTypes.MELEE_WEAPON).Name +
                    " for " + damage + " damage");
           aEnemy.Health -= damage;
            if (aEnemy.Health <= 0)
            {
                aEnemy.Die();
                //if it's a gun, make a NoiseEvent
                //EventHandler.Instance.AddEvent(EventFactory.CreateKillEntityEvent(aEnemy));
            }

        }

        public override void Move(MapBlock aDestination)
        {
            // check for objects here, etc.
            //
            if (aDestination.Passable)
            {
                iMove(this, aDestination.Coordinates);
                return;
            }
            if (aDestination.CreatureInBlock is Zombie)
            {
                Attack(aDestination.CreatureInBlock);
            }
   
        }

        public override void Die()
        {
            // Game over man. Stat screen, eulogy, etc.
            //
            Trace.WriteLine("The player has technically died. Restoring health now.");
            Health = 10;
            //base.Die();
        }
    }
}
