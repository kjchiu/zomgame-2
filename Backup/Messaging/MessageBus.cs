using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Messaging
{
    class MessageBus
    {
        protected List<IMessageListener> listeners;

        // we'll keep it as a singleton for now i guess
        protected static MessageBus _instance;

        public static MessageBus Instance
        { 
            get 
            {
                if (_instance == null) {
                    _instance = new MessageBus();
                }
                return _instance;
            }
        }

        

        protected MessageBus()
        {
            listeners = new List<IMessageListener>();
        }

        public void AddListener(IMessageListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(IMessageListener listener)
        {
            listeners.Remove(listener);
        }

        public void AddMessage(Message msg)
        {
            foreach (IMessageListener handler in listeners)
            {
                handler.HandleMessage(msg);
            }
        }
    }
}
