using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Zomgame.Factories;
using System;
using System.Collections;
using System.Diagnostics;
using Zomgame.Events;
using Zomgame.States;
using System.IO;
using Zomgame.UI;
using Zomgame.Constants;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;
using Zomgame.Props;
using Zomgame.Abilities;
using Zomgame.Items;
using Graphics;
using Zomgame.Utility;

namespace Zomgame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region " Class Members "
        public static int TurnsPassed = 0;
        #endregion

        #region " Constants "
        //public const int VISIBLE_MAP_SIZE = 2 * VISIBLE_MAP_OFFSET;
        public const int VISBLE_MAP_WIDTH = 2 * VISIBLE_MAP_WIDTH_OFFSET;
        public const int VISBLE_MAP_HEIGHT = 2 * VISIBLE_MAP_HEIGHT_OFFSET;
        public const int VISIBLE_MAP_WIDTH_OFFSET = 25;
        public const int VISIBLE_MAP_HEIGHT_OFFSET = 20;
        public const int MAP_BLOCK_SIZE = 15;
        public const int MAP_BLOCK_CENTER = MAP_BLOCK_SIZE / 2;
        public const int MAX_INPUT_FREQUENCY = 125; // milliseconds
        #endregion

        #region " Private Members "
        GraphicsDeviceManager graphics;
        ZSpriteBatch spriteBatch;
        protected Player player;
        protected Camera camera;
        protected Map map;
        protected SpriteFont font;
        protected double inputTimePassed = 0;
        protected bool canInput = false;
        protected List<Creature> entities;
        protected InputHandler inputHandler;
        protected LinkedList<GameState> states; // new states add to end
                                                // update using only last
                                                // draw first to last
        
        #endregion 

        

        #region " Properties "
        public  Player Player 
        { 
            get { return player; }
        }

        public Camera Camera
        {
            get { return camera; }
        }
        public Map Map 
        { 
            get { return map; }
        }

        public List<Creature> Entities
        {
            get { return entities; }
        }

        public ZSpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        public InputHandler InputHandler
        {
            get { return inputHandler; }
        }

        protected GameState CurrentState
        {
            get { return states.Last.Value; }
        }
        #endregion


        public Game()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            

			GraphicsDispenser.initialize();
            
           
            //inputHandler = InputHandler.Instance;
            Content.RootDirectory = "Content";

            entities = new List<Creature>();          
            states = new LinkedList<GameState>();
            this.IsMouseVisible = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			
			for (int i = 0; i < 5; i++)
            {
                //entities.Add(new Zombie(i * 20, i * 20));
            }
			
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            Logger.Log("Loading Content");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new ZSpriteBatch(GraphicsDevice);

            GraphicsDispenser.LoadTextureData(Content);
            GraphicsDispenser.LoadFontData(Content);

            MessageLog.Font = GraphicsDispenser.getFont("MessageBarFont") ;
			LoadData();
            StateFactory.Init(this);
            this.AddState(StateFactory.CreatePlayState(camera));

        }

		protected void LoadData()
		{
			player = new Player("player_bmp");
			player.AddSkill(new Skill(SkillNames.MEDICAL_SKILL));

            player.Inventory.Add(ItemFactory.CreateDefaultItem());
            player.Inventory.Add(ItemFactory.CreateDefaultItem());
            player.Inventory.Add(ItemFactory.CreateDefaultItem());

            player.Inventory.Add(ItemFactory.CreateBandage(player));
            player.Inventory.Add(ItemFactory.CreateBandage(player));

            Weapon sword2 = WeaponFactory.CreateSword();
            sword2.Name = "OTHER Sword";
            player.Inventory.Add(sword2);
            player.Inventory.Add(WeaponFactory.CreateSword());

            map = new Map(80, 80);
			map.AddObjectAt(player, 5, 5);

            Door d = new Door("door_closed_bmp");
            d.Interaction = new UseDoorPAbility(d);
            map.GetBlockAt(5, 10).AddObject(d);

			camera = new Camera(player, Game.VISBLE_MAP_WIDTH, Game.VISBLE_MAP_HEIGHT, map);

			Item item = new Item("item_bmp");
			map.GetBlockAt(3, 3).AddObject(item);

       }

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {			
            inputTimePassed += gameTime.ElapsedGameTime.TotalMilliseconds;
            
            states.Last.Value.Update(gameTime);

			InputHandler.Instance.UpdateStates();
            
            EventHandler.Instance.FireEvents(TurnsPassed);
			
            base.Update(gameTime);
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();
            foreach (GameState state in states)
                state.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void EndState()
        {
            if (states.Count > 1)
                CurrentState.StateEnded -= this.EndState;
            states.RemoveLast();

        }

        protected void AddState(GameState state)
        {
            state.StateEnded += this.EndState;
            state.StateCreated += this.AddState;
            states.AddLast(state);
        }

    }
}
