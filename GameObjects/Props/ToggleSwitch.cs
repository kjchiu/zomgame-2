using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.GameObjects.Props
{
    class ToggleSwitch : Prop, ISwitch, IInteractable
    {
        IInteractable iConnectedProp;
        private bool iIsSwitched;
        Sprite iOnGraphic;
        Sprite iOffGraphic;

        public IInteractable ConnectedProp
        {
            get { return iConnectedProp; }
            set { iConnectedProp = value; }
        }
        
        public bool IsSwitched
        {
            get { return iIsSwitched; }
        }

        public override Sprite Graphic
        {
            get
            {
                if (IsSwitched)
                {
                    return iOnGraphic;
                }
                return iOffGraphic;
            }

        }

        public Sprite OnGraphic
        {
            get { return iOnGraphic; }
            set { iOnGraphic = value;  }
        }

        public Sprite OffGraphic
        {
            get { return iOffGraphic; }
            set { iOffGraphic = value; }
        }

        public override bool SeeThrough
        {
            get
            {
                return true;
            }
        }

        #region ISwitch Members

        public void FlickSwitch()
        {
            iIsSwitched = !iIsSwitched;
            iConnectedProp.Interact();
        }

        #endregion

        #region IInteractable Members

        public void Interact()
        {
            FlickSwitch();
        }

        #endregion
    }
}
