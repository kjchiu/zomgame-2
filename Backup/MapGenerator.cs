using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;

namespace Zomgame
{
	class MapGenerator
	{
        public static Map map; 

        public static void CreateWoodenBuilding(int xPos, int yPos, int xLength, int yLength)
        {
            for (int y = 0; y <= 1; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    map.AddObjectAt(PropFactory.CreateWoodWall(), xPos + x, yPos + ((yLength - 1) * y));
                }
            }

            for (int x = 0; x <= 1; x++)
            {
                for (int y = 1; y < yLength - 1; y++)
                {
                   map.AddObjectAt(PropFactory.CreateWoodWall(), xPos + ((xLength - 1) * x), yPos + y);
                }
            }

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    map.GetBlockAt(x + xPos, y + yPos).TerrainList[0] = new Terrain("wood_floor_bmp");
                }
            }
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
						map.AddObjectAt(EntityFactory.CreateZombie(p), x, y); 
					}
				}
			}

		}
	}
}
