using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;
using Zomgame.Items;
using Zomgame.Constants;

namespace Zomgame.Abilities
{
	class EquipItemAbility : Ability
	{
		public EquipItemAbility()
		{
			ItemAction = new PerformAction(Invoke);
		}

		public override string Name
		{
			get
			{
				return EquipItemAbility.StaticName;
			}
		}

		public static string StaticName
		{
			get
			{
				return "Equip";
			}
		}

		public void Invoke(Player player)
		{
			if (AttachedItem is Weapon)
			{
				EventHandler.Instance.AddEvent(EventFactory.CreateEquipItemEvent(player, AttachedItem, EquipmentTypes.MELEE_WEAPON));
			}
		}
	}
}
