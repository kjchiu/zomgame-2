using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zomgame.Events;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zomgame.UI;

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
        protected Screen Screen
        {
            get;
            private set;
        }


        protected GameState(Game game, Screen screen)
        {
            this.spriteBatch = game.SpriteBatch;
            this.player = game.Player;
            this.map = game.Map;
            this.entities = game.Entities;
            this.Exit = game.Exit;
            this.font = game.Font;
            Screen = screen;            
        }

        protected GameState(Game game)
            : this(game, new Screen())
        {
        }

        

        public void Update(GameTime gameTime)
        {
            Screen.Update(gameTime, InputHandler.Instance);
            UpdateState(gameTime, InputHandler.Instance);            
        }
        public void Draw(GameTime gameTime)
        {
            DrawState(gameTime);
            Screen.Draw(gameTime, spriteBatch);
        }

        public abstract void UpdateState(GameTime gameTime, InputHandler input);
        public abstract void DrawState(GameTime gameTime);

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
