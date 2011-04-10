using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.GameObjects.Props
{
    class StepSwitch : Prop, ISwitch
    {
        private IInteractable iConnectedProp;

        public override bool Passable
        {
            get
            {
                return true;
            }
        }

        #region ISwitch Members

        public IInteractable ConnectedProp
        {
            get
            {
                return iConnectedProp ;
            }
            set { iConnectedProp = value; }
        }

        public void FlickSwitch()
        {
            iConnectedProp.Interact();
        }

        #endregion
    }
}
