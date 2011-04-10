/*
 * State used to depict what a zombie is up to.
 * 
 */

using System.Collections.Generic;
using System;
using Zomgame.Factories;
using System.Diagnostics;
namespace Zomgame
{
	public abstract class ZombieState
	{
		public Zombie zombie;
		public Player player; // needed to determine stuff like if the zombie sees the player and stuff 
		public Sprite graphic;
        protected Random rand;
        protected Map iMap;

		public ZombieState(Zombie aZombie, Player nPlayer, Map aMap){
			zombie = aZombie;
			player = nPlayer;
            rand = new Random(zombie.ThisID);
            iMap = aMap;
		}

		public void MoveInRandomDirection()
		{

            int lXDiff = rand.Next(-1, 1);
            int lYDiff = rand.Next(-1, 1);

            zombie.Move(iMap.GetBlockAt(zombie.Location.Coordinates[lXDiff, lYDiff]));
           
		}

		public List<PathBlock> AStarPath(MapBlock from, MapBlock to, bool willBreakThroughStuff)
		{
			if (!to.Passable)
			{
				//int closestH = 1000;
				foreach (MapBlock mb in to.SurroundingBlocks)
				{
					PathBlock p = new PathBlock(mb, null);
					p.CalculateH(from);
				}
			}
			List<PathBlock> pathToTake = new List<PathBlock>(); //final result
			List<PathBlock> openList = new List<PathBlock>(); //possible paths to check
			List<PathBlock> closedList = new List<PathBlock>(); //all spaces that have been checked

			PathBlock first = new PathBlock(from, null);
			first.G = 0;
			first.CalculateH(to);

			openList.Add(new PathBlock(from, null));

			if (from.Equals(to))
			{
				return pathToTake;
			}

			while (openList.Count > 0)
			{
				PathBlock blockToCheck = openList[0];

				if (blockToCheck.AttachedBlock.Equals(to))
				{
					//path found, work backwards from blockToCheck's parent.
					//
					PathBlock backtracker = blockToCheck;
					while (backtracker.Parent != null)
					{
						pathToTake.Insert(0, backtracker);
						backtracker = backtracker.Parent;
					}
					return pathToTake;
				}
				openList.Remove(blockToCheck);
				closedList.Add(blockToCheck);

				foreach (MapBlock mb in blockToCheck.AttachedBlock.SurroundingBlocks)
				{
					PathBlock p = new PathBlock(mb, blockToCheck);
					p.G = blockToCheck.G + 1;
					p.CalculateH(to);
				
					//ignore blocks in the closed list and those that aren't passable unless smashy smashy
					//
					if (!closedList.Contains(p)) 
					{
						int gScore = blockToCheck.G + 1;
						if (!mb.Passable) //!passable means a prop is there. That means more time smashing.
						{
							gScore += 3;
						}

						
						
						if (!openList.Contains(p)) 
						{
							bool inserted = false;
							for (int i = 0; i < openList.Count; i++)
							{
								if (openList[i].F > p.F)
								{
									openList.Insert(i, p);
									inserted = true;
									break;
								}
							}
							if (!inserted)
							{
								openList.Add(p);
							}
						}
						else 
						{
							//if the block is already in the openList, take the one with the lowest G value. If needed, switch the parent and recalculate G.
							PathBlock blockInList = openList.Find((i) => i.Equals(p));
							if (p.G < blockInList.G)
							{
								openList.Remove(blockInList);
								bool inserted = false;
								for (int i = 0; i < openList.Count; i++)
								{
									if (openList[i].F > p.F)
									{
										openList.Insert(i, p);
										inserted = true;
										break;
									}
								}
								if (!inserted)
								{
									openList.Add(p);
								}
								//blockInList.G = p.G;
								//blockInList.Parent = blockToCheck;
							}
						}
					}
				}
			}


			return pathToTake;
		}


		public abstract void DoZombieStuff();
	}
}
