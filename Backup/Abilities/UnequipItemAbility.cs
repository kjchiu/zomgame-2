using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;

namespace Zomgame.Abilities
{
	class UnequipItemAbility : Ability
	{
		public UnequipItemAbility()
		{
			ItemAction = new PerformAction(Invoke);
		}

		public override string Name
		{
			get
			{
				return UnequipItemAbility.StaticName;
			}
		}

		public static string StaticName
		{
			get { return "Unequip"; }
		}

		public void Invoke(Player player)
		{
			EventHandler.Instance.AddEvent(EventFactory.CreateUnequipItemEvent(player, AttachedItem));
		}
	}
}
