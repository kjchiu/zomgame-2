using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zomgame.Events;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zomgame.States
{
    public abstract class GameState
    {
        protected Action Exit; // this isn't a giant hack.. really
        public event Action StateEnded;
        public event Action<GameState> StateCreated;
        protected Player player;
        protected Map map;
        protected List<Entity> entities;
        protected ZSpriteBatch spriteBatch;
        protected SpriteFont font;


        public GameState(Game game)
        {
            this.spriteBatch = game.SpriteBatch;
            this.player = game.Player;
            this.map = game.Map;
            this.entities = game.Entities;
            this.Exit = game.Exit;
            this.font = game.Font;
            
        }

        abstract public void Update(GameTime time);
        abstract public void Draw(GameTime time);

        protected void EndState()
        {
            this.StateEnded();
        }

        protected void AddState(GameState state)
        {
            StateCreated(state);
        }

		protected bool KeyIsPushed(Keys key)
		{
			return InputHandler.Instance.IsKeyPushed(key);
		}

		protected bool KeyIsHeld(Keys key)
		{
			return InputHandler.Instance.IsKeyHeld(key);
		}





    }
}
