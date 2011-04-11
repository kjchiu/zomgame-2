using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;

namespace Zomgame
{
	static class MapGenerator
	{
        public static Map map; 

        public static void CreateWoodenBuilding(int xPos, int yPos, int xLength, int yLength)
        {
           
        }

		/// <summary>
		/// Puts zombies everywhere.
		/// </summary>
		/// <param name="numOfZombies">Number of zombies you want put.</param>
		/// <param name="p">The player, needed for zombie AI.</param>
		public static void PutZombiesEverywhere(int numOfZombies, Player p)
		{
			bool zomPlaced = false;
			Random r = new Random();
			for (int i = 0; i < Math.Abs(numOfZombies); i++)
			{
				zomPlaced = false;
				while (zomPlaced == false)
				{
					int x = r.Next(map.Width);
					int y = r.Next(map.Height);
					if (map.GetBlockAt(x, y).Passable)
					{
						zomPlaced = true;
					//	map.AddObjectAt(EntityFactory.CreateZombie(p), x, y); 
					}
				}
			}

		}
	}
}
