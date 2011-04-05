using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Zomgame.UI
{
    public abstract class Dialog<TResult, TAction> : InteractivePanel<TAction>
    {
        protected Semaphore semaphore;
        TResult result;

        public string Message
        {
            get;
            private set;
        }

        public TResult Result
        {
            get
            {
                semaphore.WaitOne();
                return result;    
            }
            set
            {
                Result = value;
            }
        }

        public Dialog(Screen screen,string message)
            : base (50,50,200,200, screen)
        {
            this.semaphore = new Semaphore(0,1);
            screen.AddDialog(this);
            screen.GameState.Semaphore.Set();
        }

        public override void DrawContent(Brush brush)
        {
            brush.DrawString(GraphicsDispenser.GetFont("default"), Message, new Vector2(5, 5), Color.Pink); 
        }



        public override abstract void Update(GameTime gameTime, TAction arg);
        public override abstract TAction ProcessKey(InputHandler input);

        public override void Close()
        {
            semaphore.Release();
            base.Close();
        }
    }
}
