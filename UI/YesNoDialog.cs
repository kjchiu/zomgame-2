using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Zomgame.UI
{
    public class YesNoDialog : Dialog<YesNoDialog.Response, YesNoDialog.Actions>
    {
        public enum Response {Yes,No, NUM_RESPONSES}
        public enum Actions {Left, Right, Click, None}

        Response response;

        public YesNoDialog(Screen screen, string message)
            : base(screen, message)
        {
            response = Response.Yes;
        }

        public override void Update(GameTime gameTime, YesNoDialog.Actions arg)
        {
            response++;
        }

        public override YesNoDialog.Actions ProcessKey(InputHandler input)
        {
            if (input.Consume(KeyBindings.LEFT))
            {
                return Actions.Left;
            }
            else if (input.Consume(KeyBindings.RIGHT))
            {
                return Actions.Right;
            }
            else if (input.Consume(KeyBindings.CONFIRM))
            {
                return Actions.Click;
            }
            else
            {
                return Actions.None;
            }
                
        }
    }
}
