using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Props;

namespace Zomgame.Factories
{
	static class PropFactory
	{
		public static Prop GetWoodWall(){
			Prop woodWall = new Prop("wood_wall_bmp");
			woodWall.Passable = false;
			woodWall.Durability = 50;

			return woodWall;
		}

        public static Door GetWoodDoor()
        {
            Door lDoor = new Door("door_closed_bmp");
            lDoor.Durability = 20;

            return lDoor;
        }

	}
}
