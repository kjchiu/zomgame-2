using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;
using System.Diagnostics;

namespace Zomgame
{
	class WanderState : ZombieState
	{
		public WanderState(Zombie aZombie, Player nPlayer)
			: base(aZombie, nPlayer)
		{
			graphic = new Sprite("zombie_wander_bmp");
		}

		public override void DoZombieStuff()
		{
			if (zombie.Location.Coordinates.DistanceTo(player.Location.Coordinates) < 12)
			{
				if (new Random(zombie.ThisID).Next(0, 3) > 0)
				{
					//Zombie is in killin' mode.
					zombie.CurrentState = new HuntState(zombie, player);
				}
			}
			else {
				MoveInRandomDirection();
			}
		}
	}
}
