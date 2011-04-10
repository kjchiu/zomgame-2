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
		public WanderState(Zombie aZombie, Player nPlayer, Map aMap)
			: base(aZombie, nPlayer, aMap)
		{
			graphic = new Sprite("zombie_wander_bmp");
		}

		public override void DoZombieStuff()
		{
			MoveInRandomDirection();
		}
	}
}
