/*
 *  BaseEvent
 *  
 *  Acts as an event in the game.
 *  
 */

namespace Zomgame.Events
{
    public abstract class BaseEvent
    {
        int createTime; //When, in gametime, the event was created
        int activateTime; //When, in gametime, the event should be activated

        public int ActivateTime
        {
            get { return activateTime; }
            set { activateTime = value; }
        }

        public int CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        public abstract void fireEvent();
    }
}
