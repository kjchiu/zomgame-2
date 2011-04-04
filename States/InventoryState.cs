using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Zomgame.Factories;
using Zomgame.Events;
using Zomgame.UI;

namespace Zomgame.States
{
    public class InventoryState : GameState
    {
        protected enum ItemSource { Player, Ground, NUM_SOURCES};
        protected enum ItemEvent { GrabItem, UseItem };
        protected delegate BaseEvent ItemDelegate(Player player, Item item, EventHandler handler);
        protected readonly IDictionary<ItemSource, IDictionary<ItemEvent, Action>> itemEventDelegates;
        protected Vector2 anchor;
        protected int width;
        protected int height;
        protected int margin = 5;
        protected int border = 1; //thickness of border?

        protected List<Item>[] items;
        protected uint selectedIndex = 0;
        protected uint selectedSource = 0;
        protected Panel panel;

        protected ItemSource SelectedSource
        {
            get { return (ItemSource)(int)(selectedSource); }
        }

        protected uint NumSources
        {
            get { return (uint)ItemSource.NUM_SOURCES; }
        }
            
        public InventoryState(Game game)
            : base(game)
        {
            anchor = new Vector2(border, spriteBatch.GraphicsDevice.Viewport.Height / 2 - border);
            width = (int)(spriteBatch.GraphicsDevice.Viewport.Height) - border - border;
            height = spriteBatch.GraphicsDevice.Viewport.Height / 2 + border;
            items = new List<Item>[] { player.Inventory, player.Location.Items };            
            
            itemEventDelegates = new Dictionary<ItemSource, IDictionary<ItemEvent, Action>>();

            IDictionary<ItemSource, Action> playerEventDelegates = new Dictionary<ItemSource, Action>(); 
            
			IDictionary<ItemEvent, Action> itemEventHandler = new Dictionary<ItemEvent, Action>();
			itemEventHandler.Add(ItemEvent.GrabItem, () => EventHandler.Instance.AddEvent(EventFactory.CreateDropItemEvent(player, items[(int)selectedSource][(int)selectedIndex])));
            itemEventHandler.Add(ItemEvent.UseItem, () => { });
            itemEventDelegates.Add(ItemSource.Player, itemEventHandler);

            itemEventHandler = new Dictionary<ItemEvent, Action>();
			itemEventHandler.Add(ItemEvent.GrabItem, () => EventHandler.Instance.AddEvent(EventFactory.CreatePickupItemEvent(player, items[(int)selectedSource][(int)selectedIndex])));
            itemEventHandler.Add(ItemEvent.UseItem, () => { });
            itemEventDelegates.Add(ItemSource.Ground, itemEventHandler);
        }
        
        public override void UpdateState(GameTime time, InputHandler input)
        {
            if (selectedIndex >= items[selectedSource].Count)
            {
                selectedIndex = (uint)items[selectedSource].Count - 1;
            }
            
            if (InputHandler.Instance.IsKeyPushed(KeyBindings.CLOSE_INV))
            {
                this.EndState();
            }

            
            if (InputHandler.Instance.IsKeyPushed(KeyBindings.UP))
            {
                if (items[selectedSource].Count > 0)
                {
                    selectedIndex = (--selectedIndex); //% (uint)items[selectedSource].Count;
                }
            }

            if (InputHandler.Instance.IsKeyPushed(KeyBindings.DOWN))
            {
                if (items[selectedSource].Count > 0)
                {
                    selectedIndex = (++selectedIndex) % (uint)items[selectedSource].Count;
                }
            }

			if (InputHandler.Instance.IsKeyPushed(KeyBindings.LEFT))
            {

                selectedSource = (--selectedSource) % NumSources;
                if (items[selectedSource].Count > 0)
                {
                    selectedIndex = (uint)Math.Min(items[selectedSource].Count, (int)selectedIndex);
                }
                else
                {
                    selectedSource = (++selectedSource) % 2;
                }

            }

            if (InputHandler.Instance.IsKeyPushed(KeyBindings.RIGHT))
            {
                selectedSource = (++selectedSource) % 2;
                if (items[selectedSource].Count > 0)
                {
                    selectedIndex = (uint)Math.Min(items[selectedSource].Count, (int)selectedIndex);
                }
                else
                {
                    selectedSource = (--selectedSource) % 2;
                }
            }

            if (InputHandler.Instance.IsKeyPushed(KeyBindings.PICK_UP))
            {
                if (items[selectedSource].Count > 0)
                {
                    itemEventDelegates[SelectedSource][ItemEvent.GrabItem]();
					selectedIndex = 0;
					selectedSource = (uint)ItemSource.Player;
                }
            }

			if (InputHandler.Instance.IsKeyPushed(Keys.A))
			{
				foreach (Ability a in items[(int)selectedSource][(int)selectedIndex].Abilities.Values)
				{
					Trace.WriteLine(a.Name + " ");
				}
			}

			if (InputHandler.Instance.IsKeyPushed(KeyBindings.DROP_ITEM))
			{
				items[(int)selectedSource][(int)selectedIndex].Abilities["Drop"].ItemAction(player);
			}

            if (InputHandler.Instance.IsKeyPushed(Keys.M))
            {
                items[(int)selectedSource][(int)selectedIndex].Abilities["First Aid"].ItemAction(player);
            }

            if (KeyIsPushed(Keys.Enter))
            {
                if (items[selectedSource].Count > 0)
                    if (selectedSource == 0)
                    {
                        AddState(StateFactory.CreateItemDetailsState(items[(int)selectedSource][(int)selectedIndex]));
                    }
                    else if (selectedSource == 1)
                    {
                        EventHandler.Instance.AddEvent(EventFactory.CreatePickupItemEvent(player, items[(int)selectedSource][(int)selectedIndex]));  
                    }
            }
        }

        public override void DrawState(GameTime time)
        {
            panel.Draw(spriteBatch);
            int columnWidth = width / 2;
            for (int column = 0; column < 2; ++column)
            {
                for (int i = 0; i < items[column].Count; ++i)
                {
                    Vector2 offset = anchor + new Vector2(margin + column*columnWidth, font.LineSpacing * (i + 1));
                    spriteBatch.Draw(items[column][i].Graphic.Texture, offset, Color.White);
                    offset += new Vector2(items[column][i].Graphic.Texture.Width + margin, 0);
                    spriteBatch.DrawString(font, items[column][i].Name, offset, i == selectedIndex && column == selectedSource ? Color.Yellow : Color.White);
                }
                if (items[column].Count == 0)
                {
                    Vector2 offset = anchor + new Vector2(margin + column * columnWidth, font.LineSpacing);
                    spriteBatch.DrawString(font,"-----", offset, column == selectedSource ? Color.Yellow : Color.White);
                }
            }
            


            spriteBatch.DrawLine(anchor + new Vector2(width / 2, 0), anchor + new Vector2(width / 2, height), border, Color.DarkGray);
        }

    }
}
