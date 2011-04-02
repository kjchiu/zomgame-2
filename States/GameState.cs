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
        protected event Action EndState;
        protected Player player;
        protected Camera camera;
        protected Map map;
        protected EventHandler eventHandler;
        protected List<Entity> entities;
        protected SpriteBatch spriteBatch;
        protected SpriteFont font;


        public GameState(Game game)
        {
            this.eventHandler = game.EventHandler;
            this.spriteBatch = game.SpriteBatch;
            this.player = game.Player;
            this.map = game.Map;
            this.entities = game.Entities;
            this.Exit = game.Exit;
            
            this.font = game.Font;
            
        }



        abstract public void Update(GameTime time, HashSet<Keys> keys);
        abstract public void Draw(GameTime time);
        

    }
}
