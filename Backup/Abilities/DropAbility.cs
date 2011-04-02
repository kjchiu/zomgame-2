

using Zomgame.Factories;
using System.Diagnostics;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;

namespace Zomgame.Abilities
{
	class DropAbility : Ability
	{

		public DropAbility()
		{
			ItemAction = new PerformAction(Invoke);
		}

		public override string Name
		{
			get
			{
				return "Drop";
			}
		}

		public void Invoke(Player player)
		{
			MessageBus.Instance.AddMessage(new DropItemMessage(AttachedItem));
			EventHandler.Instance.AddEvent(EventFactory.CreateDropItemEvent(player, AttachedItem));
		}
	}
}
