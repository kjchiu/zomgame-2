/* 
 * Weapon Factory
 * 
 * Creates equippable weaponry, ranged and melee.
 * 
 */

using Zomgame.Items;

namespace Zomgame.Factories
{
	class WeaponFactory
	{
		public static Weapon CreateFists()
		{
			Weapon fists = new Weapon(1, "fist_icon_bmp");
			fists.Description = "Your fists. Not very effective at all.";
			fists.Name = "Fists";

			return fists;
		}

		public static Weapon CreateSword()
		{
			Weapon sword = new Weapon(10, "sword_bmp");
			sword.Description = "A fairly decent western blade.";
			sword.Name = "Sword";

			return sword;
		}
	}
}
