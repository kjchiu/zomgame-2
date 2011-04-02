using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zomgame.Factories;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Zomgame.UI;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;
using Zomgame.Constants;

namespace Zomgame.States
{
    public class PlayState : GameState
    {
        protected Camera camera;   
        protected int inputTimePassed;
        protected MessageLog msgLog;

        public PlayState(Game game) : base(game)
        {
            camera = game.Camera;
            int y = camera.Height * Game.MAP_BLOCK_SIZE;
            msgLog = new MessageLog(0, y, camera.Width * Game.MAP_BLOCK_SIZE, game.Window.ClientBounds.Height - y);
        }

        #region IState Members

        public override void Update(GameTime time)
        {
            
			//first handle CTRL/ALT/SHIFT keys
			if (KeyIsHeld(KeyBindings.L_CTRL) || KeyIsHeld(KeyBindings.R_CTRL))
			{
				if (KeyIsPushed(KeyBindings.UP)){
					MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1);
					EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
				}
				else if (KeyIsPushed(KeyBindings.DOWN))
				{
					MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1);
					EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
				}
				else if (KeyIsPushed(KeyBindings.LEFT))
				{
					MapBlock location = map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y);
					EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
				}
				else if (KeyIsPushed(KeyBindings.RIGHT))
				{
					MapBlock location = map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y);
					EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
				}

			} else if (KeyIsHeld(KeyBindings.L_SHIFT) || KeyIsHeld(KeyBindings.R_SHIFT)){
                if (KeyIsHeld(KeyBindings.LEFT))
                {
                    EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y)));
                }
                else if (KeyIsHeld(KeyBindings.RIGHT))
                {
                    EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y)));
                }
                else if (KeyIsHeld(KeyBindings.DOWN))
                {
                    EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1)));
                }
                else if (KeyIsHeld(KeyBindings.UP))
                {
                    EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1)));
                }
            }
            else if (KeyIsHeld(KeyBindings.L_ALT) || KeyIsHeld(KeyBindings.R_ALT))
            {
                if (KeyIsPushed(KeyBindings.UP))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1);
                    EventHandler.Instance.AddEvent(EventFactory.CreatePropInteractionEvent(location.Props[0], player));
                }
                else if (KeyIsPushed(KeyBindings.DOWN))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1);
                    EventHandler.Instance.AddEvent(EventFactory.CreatePropInteractionEvent(location.Props[0], player));
                }
                else if (KeyIsPushed(KeyBindings.LEFT))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y);
                    EventHandler.Instance.AddEvent(EventFactory.CreatePropInteractionEvent(location.Props[0], player));
                }
                else if (KeyIsPushed(KeyBindings.RIGHT))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y);
                    EventHandler.Instance.AddEvent(EventFactory.CreatePropInteractionEvent(location.Props[0], player));
                }
            }
            else if (InputHandler.Instance.IsKeyPushed(Keys.Escape)){
                this.Exit();
			}
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.LEFT))
            {
				EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y)));
            }
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.RIGHT))
            {
				EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y)));
            }
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.DOWN))
            {
				EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1)));
            }
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.UP))
            {
				EventHandler.Instance.AddEvent(EventFactory.CreateMoveEvent(player, map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1)));
            }
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.PICK_UP))
			{
				if (player.Location.HasItems)
				{
					EventHandler.Instance.AddEvent(EventFactory.CreatePickupItemEvent(player, player.Location.ItemAt(0)));
				}
			}
			else if (InputHandler.Instance.IsKeyPushed(Keys.W))
            {
				EventHandler.Instance.AddEvent(EventFactory.CreateWaitEvent(player));
            }
			else if (InputHandler.Instance.IsKeyPushed(KeyBindings.OPEN_INV))
            {
                AddState(StateFactory.CreateInventoryState());
            }
			else if (KeyIsPushed(Keys.N))
			{
				EventHandler.Instance.AddEvent(EventFactory.CreateMakeNoiseEvent(map.GetBlockAt(player.Location.Coordinates), 50));
			}
            else if (KeyIsPushed(Keys.Back))
            {
                MessageBus.Instance.AddMessage(new FillerMessage());
            }
			else if (KeyIsPushed(Keys.E))
			{
				Trace.WriteLine("Player is equipped with " + player.EquipmentIn(EquipmentTypes.MELEE_WEAPON).Name);
			}
        }

        public override void Draw(GameTime time)
        {
			camera.Draw(spriteBatch);
            msgLog.Draw(spriteBatch);
        }

        #endregion
    }
}
