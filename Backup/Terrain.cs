using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.States;

namespace Zomgame {
    public class Terrain
    {
		Sprite graphic;

        public Terrain()
        {
			graphic = new Sprite("null_bmp");
        }

        public Terrain(string imgLoc)
        {
            graphic = new Sprite(imgLoc);
        }

        public Sprite Graphic {
            get { return graphic; }
			set { graphic = value; }
        }
    }
}
