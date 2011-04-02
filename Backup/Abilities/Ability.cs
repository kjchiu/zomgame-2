/*
 * Action
 * 
 * An Action is a verb that can be accomplished by the player. Eat, throw, drop, etc. Actions are tied to the items that use them.
 * Sometimes Skills are tied to actions. If the Action is performed, the user's skill is checked, and goes up.
 * 
 */

using Zomgame.Factories;

namespace Zomgame
{
	public class Ability
	{
		public delegate void PerformAction(Player player); //the delegate type
		public PerformAction ItemAction;
		
		Item attachedItem;

		public Ability() 
		{
		}

		public Item AttachedItem
		{
			get { return attachedItem; }
			set { attachedItem = value; }
		}

		public virtual string Name
		{
			get { return "NoName"; }
		}
	}
}
