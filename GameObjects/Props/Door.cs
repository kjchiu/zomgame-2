using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Props
{
    class Door : Prop
    {
        protected bool opened;
        private Sprite openedGraphic;

        public Door(string _imgLoc)
            : base (_imgLoc)
        {
            openedGraphic = new Sprite("door_opened_bmp");
        }

        public override bool Passable
        {
            get { return opened; }
        }

        public override bool SeeThrough
        {
            get
            {
                return opened;
            }
        }

        public void ToggleOpen()
        {
            opened = !opened;
        }

        public override Sprite Graphic
        {
            get
            {
                if (opened)
                {
                    return openedGraphic;
                }
                return base.Graphic;
            }
        }
    }
}
