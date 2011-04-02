/* 
 * Wait Event
 * 
 * Does nothing. The Entity is set to BUSY for the duration of the event.
 * 
 */

namespace Zomgame.Events {
	class WaitEvent : BaseEvent {
		Entity waiter;
        
        public WaitEvent(Entity nWaiter) {
            waiter = nWaiter;
        }

        public override void fireEvent() {
            waiter.State = Entity.EntityState.IDLE;
        }
	}
}
