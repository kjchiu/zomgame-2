using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Zomgame.Factories;
using Zomgame.Constants;
using Zomgame.Messaging;
using Zomgame.Items;

namespace Zomgame.Events
{
	class AttackEvent : BaseEvent
	{
		Creature attacker, defender;

		public AttackEvent(Creature nAttacker, Creature nDefender)
		{
			attacker = nAttacker;
			defender = nDefender;
		}

		public override void fireEvent()
		{
			attacker.State = Creature.EntityState.IDLE;
			Trace.WriteLine(attacker.Name + " has attacked " + defender.Name);
			//for now, make it simple
            int damage = 10;
            //get damage based on weapon
            if (attacker is Player)
            {
                damage += ((Weapon)(((Player)attacker).EquipmentIn(EquipmentTypes.MELEE_WEAPON))).Damage;
                Trace.WriteLine("Player attacks zombie with " + ((Player)attacker).EquipmentIn(EquipmentTypes.MELEE_WEAPON) +
                    " for " + damage + " damage");
            }

            defender.Health -= damage;
            if (defender.Health <= 0)
            {
                //if it's a gun, make a NoiseEvent
                EventHandler.Instance.AddEvent(EventFactory.CreateKillEntityEvent(defender));
            }
		}
	}
}
