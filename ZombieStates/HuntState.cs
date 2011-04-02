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
        public HuntState(Zombie aZombie, Player nPlayer)
			: base(aZombie, nPlayer)
		{
			graphic = new Sprite("zombie_hunt_bmp");
		}

		public override void DoZombieStuff()
		{
			//Trace.WriteLine("ZOMBIE IS IN KILL MODE FUUUUUUCK!");
			if (zombie.Location.Coordinates.DistanceTo(player.Location.Coordinates) > 15)
			{
				//Zombie is in not-killin' mode.
				//probably go into Investigate State
				zombie.CurrentState = new WanderState(zombie, player);
			}
			else
			{
				//A* bitches


			}
		}
	}
}
