﻿using System;
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
using Zomgame.GameObjects.Props;

namespace Zomgame.States
{
    public class PlayState : GameState
    {
        protected Camera camera;
        protected int inputTimePassed;
        protected MessageLog msgLog;

        public PlayState(Game game)
            : base(game)
        {
            camera = game.Camera;
            int y = camera.Height * Game.MAP_BLOCK_SIZE;
            msgLog = new MessageLog(0, y, camera.Width * Game.MAP_BLOCK_SIZE, game.Window.ClientBounds.Height - y, Screen);
        }

        #region IState Members

        public override void UpdateState(GameTime time, InputHandler input)
        {

            //first handle CTRL/ALT/SHIFT keys
            if (KeyIsHeld(KeyBindings.L_CTRL) || KeyIsHeld(KeyBindings.R_CTRL))
            {
                if (input.Consume(KeyBindings.UP))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1);
                    EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
                }
                else if (input.Consume(KeyBindings.DOWN))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1);
                    EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
                }
                else if (input.Consume(KeyBindings.LEFT))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y);
                    EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
                }
                else if (input.Consume(KeyBindings.RIGHT))
                {
                    MapBlock location = map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y);
                    EventHandler.Instance.AddEvent(EventFactory.CreateAttackSpaceEvent(player, location));
                }

            }
            else if (KeyIsHeld(KeyBindings.L_SHIFT) || KeyIsHeld(KeyBindings.R_SHIFT))
            {
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
                if (input.Consume(KeyBindings.UP))
                {
                    MapBlock lInteractBlock = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y - 1);
                    InteractWithBlock(lInteractBlock);
                }
                else if (input.Consume(KeyBindings.DOWN))
                {
                    MapBlock lInteractBlock = map.GetBlockAt(player.Location.Coordinates.X, player.Location.Coordinates.Y + 1);
                    InteractWithBlock(lInteractBlock);
                }
                else if (input.Consume(KeyBindings.LEFT))
                {
                    MapBlock lInteractBlock = map.GetBlockAt(player.Location.Coordinates.X - 1, player.Location.Coordinates.Y);
                    InteractWithBlock(lInteractBlock);
                }
                else if (input.Consume(KeyBindings.RIGHT))
                {
                    MapBlock lInteractBlock = map.GetBlockAt(player.Location.Coordinates.X + 1, player.Location.Coordinates.Y);
                    InteractWithBlock(lInteractBlock);
                }
            }
            else if (input.Consume(Keys.Escape))
            {
                this.Exit();
            }
            else if (input.Consume(KeyBindings.LEFT))
            {
                player.Move(map.GetBlockAt(player.Location.Coordinates[-1, 0]));
            }
            else if (input.Consume(KeyBindings.RIGHT))
            {
                player.Move(map.GetBlockAt(player.Location.Coordinates[+1, 0]));
            }
            else if (input.Consume(KeyBindings.DOWN))
            {
                player.Move(map.GetBlockAt(player.Location.Coordinates[0, +1]));
            }
            else if (input.Consume(KeyBindings.UP))
            {
                player.Move(map.GetBlockAt(player.Location.Coordinates[0, -1 ]));
                player.Move(map.GetBlockAt(player.Location.Coordinates[0, 0]));
            }
            else if (input.Consume(KeyBindings.PICK_UP))
            {
                if (player.Location.HasItems)
                {
                    EventHandler.Instance.AddEvent(EventFactory.CreatePickupItemEvent(player, player.Location.ItemAt(0)));
                }
            }
            else if (input.Consume(KeyBindings.PICK_UP))
            {
                if (player.Location.HasItems)
                {
                    player.Location.RemoveObject(player.Location.ItemAt(0));
                    player.PickUpItem(player.Location.ItemAt(0));
                    EventHandler.Instance.AddEvent(EventFactory.CreatePickupItemEvent(player, player.Location.ItemAt(0)));
                }
            }
            else if (input.Consume(KeyBindings.DROP_ITEM))
            {
                if (player.Inventory.Count > 0)
                {
                    player.DropItem(player.Inventory[0]);
                }
            }
            else if (input.Consume(Keys.W))
            {
                EventHandler.Instance.AddEvent(EventFactory.CreateWaitEvent(player));
            }
            else if (input.Consume(KeyBindings.OPEN_INV))
            {
                //AddState(StateFactory.CreateInventoryState());
                Screen.AddPanel(new InventoryPanel(Screen, player));
            }
            else if (input.Consume(Keys.N))
            {
                EventHandler.Instance.AddEvent(EventFactory.CreateMakeNoiseEvent(map.GetBlockAt(player.Location.Coordinates), 50));
            }
            else if (input.Consume(Keys.Back))
            {
                MessageBus.Instance.AddMessage(new FillerMessage());
            }
            else if (input.Consume(Keys.E))
            {
                Trace.WriteLine("Player is equipped with " + player.EquipmentIn(EquipmentTypes.MELEE_WEAPON).Name);
            }


        }

        private void InteractWithBlock(MapBlock lInteractBlock)
        {
            if (lInteractBlock.PropInBlock is IInteractable)
            {
                IInteractable lInteractable = (IInteractable)lInteractBlock.PropInBlock;
                lInteractable.Interact();
            }
        }

        public override void DrawState(GameTime time)
        {
            camera.Draw(spriteBatch);
            msgLog.Draw(spriteBatch);
        }

        #endregion
    }
}
