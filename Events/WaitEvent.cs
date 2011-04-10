/* 
 * Wait Event
 * 
 * Does nothing. The Entity is set to BUSY for the duration of the event.
 * 
 */

namespace Zomgame.Events {
	class WaitEvent : BaseEvent {
		Creature waiter;
        
        public WaitEvent(Creature nWaiter) {
            waiter = nWaiter;
        }

        public override void fireEvent() {
            waiter.State = Creature.EntityState.IDLE;
        }
	}
}
