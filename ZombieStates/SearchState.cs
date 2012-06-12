using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;
using Zomgame.Constants;
using System.Diagnostics;

namespace Zomgame.ZombieStates
{
    class SearchState : ZombieState
    {
        MapBlock destination;
        List<PathBlock> path;

        public SearchState(Zombie aZombie, Player nPlayer, Map aMap)
            : base(aZombie, nPlayer, aMap)
        {
            graphic = new Sprite("zombie_search_bmp");
            path = new List<PathBlock>();
        }

        public MapBlock Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public override void DoZombieStuff()
        {
            //first, if the zombie can see the player, put it in HUNT mode
            //destination = player.Location;
            //if the zombie can see the location and it's between 0 - 6 blocks away, put it in WANDER mode
            if (zombie.Location.Coordinates.DistanceTo(destination.Coordinates) < 6)
            {
                if (new Random(zombie.ThisID).Next(0, 3) > 0)
                {
                    //Zombie is in killin' mode.
                    //zombie.ChangeStateTo(ZombieStateNames.WANDER_STATE);
                }
            }


            //otherwise, move towards the destination using a pathfinding algorithm - no smashing stuff
            //okay maybe some smashing, if needed
            if (path.Count == 0)
            {
                path = AStarPath(zombie.Location, destination, true);
            }
            if (new Random().Next(0, 3) > 0)
            {
                if (path.Count > 0) { path.Insert(0, new PathBlock(zombie.Location, path[0])); }
                MoveInRandomDirection();
            }
            if (path != null && path.Count > 0)
            {
                EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(zombie, path[0].AttachedBlock));
                path.Remove(path[0]);
            }



        }
    }
}
