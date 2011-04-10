/* 
 * Game Object
 * 
 * This is an object in the world: an entity, prop, or item.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.States;

namespace Zomgame {
    public abstract class GameObject {
		static int id = 0;

		private int thisID;
        private String name;
        protected string description;
        private MapBlock location;
		Sprite graphic;

        // newObject.RemoveObject = new RemoveFromWorld(RemoveObject);
        //    newObject.AddObject = new AddToWorld(AddObject);
        //
       public Map.AddObjectAtDel addObjectAt;
       public Map.RemoveObjectDel removeObject;

        protected GameObject():this("null_bmp"){ }

        protected GameObject(string nImgLoc) {
           	thisID = id++;
			name = "GameObject-" + thisID;
            description = "A thing.";
			graphic = new Sprite(nImgLoc);
        }

        public Map.AddObjectAtDel AddObjectAt
        {
            get { return addObjectAt; }
            set { addObjectAt = value; }
        }

        public Map.RemoveObjectDel RemoveObject
        {
            get { return removeObject; }
            set { removeObject = value; }
        }

        public MapBlock Location {
            get { return location; }
            set { location = value; }
        }

		public virtual Sprite Graphic
		{
			get { return graphic; }
			set { graphic = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int ThisID
		{
			get { return thisID; }
		}

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
