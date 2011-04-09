/*
 * In the HuntState, the zombie knows where the player is, and is making more or less a beeline towards him/her.
 * 
 */

using System.Diagnostics;
using System;
namespace Zomgame
{
	class HuntState : ZombieState
	{
        public HuntState(Zombie aZombie, Player nPlayer, Map aMap)
			: base(aZombie, nPlayer, aMap)
		{
			graphic = new Sprite("zombie_hunt_bmp");
		}

		public override void DoZombieStuff()
		{
			
		}
	}
}
