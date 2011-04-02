/*
 * Event Handler
 * 
 * This class will be taking care of all the events, firing them when it needs to, and perhaps discarding them after a time.
 * 
 */

using Zomgame.Events;
using System.Collections.Generic;
using System.Diagnostics;

namespace Zomgame {
    public class EventHandler {

		private static EventHandler instance;

        private List<BaseEvent> eventList;
        private List<BaseEvent> firedList;
        int necroTime; //in game turns

        public EventHandler() {
            InitLists();
            necroTime = 10;
        }

        private void InitLists(){
            eventList = new List<BaseEvent>();
            firedList = new List<BaseEvent>();
        }

        public void AddEvent(BaseEvent newEvent) {
            eventList.Add(newEvent);
        }

        public void RemoveEvent(BaseEvent remEvent) {
            eventList.Remove(remEvent);
        }

        public bool HasEvents()
        {
            return eventList.Count > 0;
        }

        
        /// <summary>
        /// Fires all events up to the currentTime in the game, then puts them in the firedEvents list
        /// </summary>
        /// <param name="currentTime">The current gametime, in turns passed</param>
        public void FireEvents(int currentTime) {
            for(int i=0; i<eventList.Count; i++){
                BaseEvent bEvent = eventList[i];
                //Trace.WriteLine("Current time = " + currentTime);
                   
                if (bEvent.ActivateTime <= currentTime) {
                    //Trace.WriteLine("Event activate: " + bEvent.ActivateTime);
                    bEvent.fireEvent();
                    firedList.Add(bEvent);
                    eventList.Remove(bEvent);
                    i--;
                }
            }
         
            CleanFiredEvents(currentTime);
        }

        /// <summary>
        /// Removes the fired events that are older than necroTime.
        /// </summary>
		/// <param name="currentTime">The current gametime, in turns passed</param>
        private void CleanFiredEvents(int currentTime) {
           for (int i=0; i<firedList.Count; i++){
               BaseEvent bEvent = firedList[i];
                if (currentTime - bEvent.ActivateTime > necroTime) {
                    firedList.Remove(bEvent);
                    i--;
                }
            }
		}

            
			public static EventHandler Instance{
				get {
					if (instance == null)
					{
						instance = new EventHandler();
					}
					return instance;
				}
			}
    }
}
