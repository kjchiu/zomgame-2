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
        Creature mover;
        MapBlock destination;

        public MoveEvent(Creature nMover, MapBlock nDestination) {
            mover = nMover;
            destination = nDestination;
        }

        public override void fireEvent() {
			mover.State = Creature.EntityState.IDLE;
			if (destination != null && destination.GameMap.IsInMap(destination.Coordinates)) {
				//check for props and entities first
				if (destination.PropInBlock != null)
				{
					
						if (!destination.PropInBlock.Passable)
						{
							//If it's a door and the mover is a player, then just open the door
                            if (destination.PropInBlock is Door && mover is Player)
                            {
                                destination.PropInBlock.Interact((Player)mover);
							}
							else if (mover is Zombie)
							{
								//zombies just destroy shit because they feel unloved.
                                EventHandler.Instance.AddEvent(EventFactory.CreateAttackPropEvent(mover, destination.PropInBlock));
							}
                            mover.State = Creature.EntityState.IDLE;
							
							return;
						}
					
				}
				if (destination.CreatureInBlock != null)
				{
                    Creature defender = destination.CreatureInBlock;
					if (!(mover is Zombie && defender is Zombie))
					{
                        EventHandler.Instance.AddEvent(EventFactory.CreateAttackEvent(mover, destination.CreatureInBlock));
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
