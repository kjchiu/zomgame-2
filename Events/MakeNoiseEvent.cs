/*
 * Making a noise will alert both the zombies and the player.
 *
 */

using System.Collections.Generic;
using System.Diagnostics;
using Zomgame.ZombieStates;
using Zomgame.Constants;
namespace Zomgame.Events
{
	class MakeNoiseEvent : BaseEvent
	{
		MapBlock location;
		int strength; //the stronger the sound, the further it goes

		public MakeNoiseEvent(MapBlock nLocation, int nStrength){
			location = nLocation;
			strength = nStrength;
		}

		public override void fireEvent()
		{
			//look at the location and alert anyone in it about the noise
			//then propogate using breadth-first search
			int currentStrength = strength;
			List<MapBlock> placesVisited = new List<MapBlock>();
			List<MapBlock> placesToGo = new List<MapBlock>();
			List<MapBlock> placesToAdd = new List<MapBlock>();
			placesToGo.Add(location);
			while (currentStrength > 0)
			{
				while (placesToGo.Count > 0){
					MapBlock b = placesToGo[0];
					if (!placesVisited.Contains(b))
					{
						if (b.CreatureInBlock != null)
						{
							//alert the entity(ies)
                            if (b.CreatureInBlock is Player)
							{
								//directional-based hearing? ie "You hear a noise to the south!"
								Trace.WriteLine("You hear a loud noise!");
							}
                            else if (b.CreatureInBlock is Zombie)
							{
								//put zombie into SEARCH state
								Trace.WriteLine("The zombie has heard a loud noise.");
                                ((Zombie)b.CreatureInBlock).ChangeStateTo(ZombieStateNames.SEARCH_STATE, location);
							}
						}
						placesToAdd.AddRange(OuterBlocksOf(b));
						placesVisited.Add(b);
						//Trace.WriteLine("Making noise at " + b.Coordinates.X + ", " + b.Coordinates.Y + ", strength of " + strength);
					}
					placesToGo.Remove(b);
				}
				placesToGo.AddRange(placesToAdd);
				placesToAdd.Clear();
				currentStrength--;
			}
			Trace.WriteLine("Noise propogation complete.");
		}

		private List<MapBlock> OuterBlocksOf(MapBlock center)
		{
			List<MapBlock> blocks = new List<MapBlock>();
			Map map = center.GameMap;
			//blocks.Add(map.GetBlockAt(center.Coordinates.X-1, center.Coordinates.Y+1));
			if (map.IsInMap(center.Coordinates.X-1, center.Coordinates.Y)) {
				blocks.Add(map.GetBlockAt(center.Coordinates.X-1, center.Coordinates.Y));
			}
			if (map.IsInMap(center.Coordinates.X+1, center.Coordinates.Y) ){
				blocks.Add(map.GetBlockAt(center.Coordinates.X+1, center.Coordinates.Y));
			}
			if (map.IsInMap(center.Coordinates.X, center.Coordinates.Y+1) ){
				blocks.Add(map.GetBlockAt(center.Coordinates.X, center.Coordinates.Y+1));
			}
			if (map.IsInMap(center.Coordinates.X, center.Coordinates.Y-1) ){
				blocks.Add(map.GetBlockAt(center.Coordinates.X, center.Coordinates.Y-1));
			}
			//blocks.Add(map.GetBlockAt(center.Coordinates.X-1, center.Coordinates.Y-1));
			//blocks.Add(map.GetBlockAt(center.Coordinates.X+1, center.Coordinates.Y+1));
			//blocks.Add(map.GetBlockAt(center.Coordinates.X+1, center.Coordinates.Y-1));
			
			
			return blocks;
		}
	}
}
