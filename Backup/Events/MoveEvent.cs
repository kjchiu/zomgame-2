/*
 *  Move Event 
 *  
 * Simulates an object moving from one mapblock to another.
 * 
 */

using System.Diagnostics;
using Zomgame.Factories;
using Zomgame.Props;
namespace Zomgame.Events
{
    class MoveEvent : BaseEvent
    {
        Entity mover;
        MapBlock destination;

        public MoveEvent(Entity nMover, MapBlock nDestination) {
            mover = nMover;
            destination = nDestination;
        }

        public override void fireEvent() {
			mover.State = Entity.EntityState.IDLE;
			if (destination != null && destination.GameMap.IsInMap(destination.Coordinates)) {
				//check for props and entities first
				if (destination.Props.Count > 0)
				{
					foreach (Prop prop in destination.Props)
					{
						if (!prop.Passable)
						{
							//If it's a door and the mover is a player, then just open the door
                            if (prop is Door && mover is Player)
                            {
                                prop.Interact((Player)mover);
							}
							else if (mover is Zombie)
							{
								//zombies just destroy shit because they feel unloved.
								EventHandler.Instance.AddEvent(EventFactory.CreateAttackPropEvent(mover, prop));
							}
                            mover.State = Entity.EntityState.IDLE;
							
							return;
						}
					}
				}
				if (destination.Entities.Count > 0)
				{
					Entity defender = destination.Entities[0];
					if (!(mover is Zombie && defender is Zombie))
					{ 
						EventHandler.Instance.AddEvent(EventFactory.CreateAttackEvent(mover, destination.Entities[0]));
					}
					return;
				}
				mover.Location.RemoveObject(mover);
				destination.AddObject(mover);
				mover.Location = destination;
			}
			
        }
    }
}
