using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Factories
{
	static class PropFactory
	{
		public static Prop CreateWoodWall(){
			Prop woodWall = new Prop("wood_wall_bmp");
			woodWall.Passable = false;
			woodWall.Durability = 50;

			return woodWall;
		}

	}
}
