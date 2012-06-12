using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Zomgame.Events;
using Zomgame.Factories;
using Zomgame.Graphics;
using Zomgame.Messaging;
using Zomgame.Messaging.Messages;

namespace Zomgame.UI
{
    class InventoryPanel : InteractivePanel<InventoryPanel.Actions>
    {
        public enum Actions
        {
            Up,
            Down,
            Left,
            Right,
            Close,
            Pickup,
            Drop,
            Ability,
            None
        }
        private List<Item>[] items;

        private uint selectedSource;
        private uint selectedIndex;

        #region
        //TODO Purge this garbage once inventory panel renders
        protected ItemSource SelectedSource
        {
            get { return (ItemSource)(int)(selectedSource); }
        }
        protected enum ItemSource { Player, Ground, NUM_SOURCES };
        protected enum ItemEvent { GrabItem, UseItem };
        protected delegate BaseEvent ItemDelegate(Player player, Item item, EventHandler handler);
        protected readonly IDictionary<ItemSource, IDictionary<ItemEvent, Action>> itemEventDelegates;
        private Player player;
        #endregion

        public InventoryPanel(Screen screen, Player player)
            : base(0, Game.VISBLE_MAP_HEIGHT * Game.MAP_BLOCK_SIZE, Game.VISBLE_MAP_WIDTH * Game.MAP_BLOCK_SIZE, Game.WINDOW_HEIGHT - (Game.VISBLE_MAP_HEIGHT * Game.MAP_BLOCK_SIZE), screen)
        {
            itemEventDelegates = new Dictionary<ItemSource, IDictionary<ItemEvent, Action>>();
            this.player = player;
            items = new List<Item>[] { player.Inventory, player.Location.Items };
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

        public override Actions ProcessKey(InputHandler input)
        {
            if (input.Consume(KeyBindings.UP))
            {
                return Actions.Up;
            }
            else if (input.Consume(KeyBindings.DOWN))
            {
                return Actions.Down;
            }
            else if (input.Consume(KeyBindings.LEFT))
            {
                return Actions.Left;
            }
            else if (input.Consume(KeyBindings.RIGHT))
            {
                return Actions.Right;
            }
            else if (input.Consume(KeyBindings.CLOSE_INV))
            {
                return Actions.Close;
            }
            else if (input.Consume(KeyBindings.DROP_ITEM))
            {
                return Actions.Drop;
            }
            else if (input.Consume(Keys.A))
            {
                MessageBus.Instance.AddMessage(new FillerMessage());
                return Actions.Ability;
            }
            else
            {
                return Actions.None;
            }
        }

        public override void Update(GameTime gameTime, Actions action)
        {
            switch (action)
            {
                case Actions.None:
                    return;
                case Actions.Right:
                    selectedSource = (++selectedSource) % 2;
                    break;
                case Actions.Left:
                    selectedSource = (--selectedSource) % 2;
                    break;
                case Actions.Up:
                    if (items[selectedSource].Count > 0)
                    {
                        selectedIndex = (--selectedIndex) % (uint)items[selectedSource].Count;
                    }
                    break;
                case Actions.Down:
                    if (items[selectedSource].Count > 0)
                    {
                        selectedIndex = (++selectedIndex) % (uint)items[selectedSource].Count;
                    }
                    break;
                case Actions.Drop:
                    var item = items[(int)selectedSource][(int)selectedIndex];
                    if (selectedSource == 0)
                    {
                        item.Abilities["Drop"].ItemAction(player);
                    }
                    else
                    {
                        MessageBus.Instance.AddMessage(new Message("What, " + item.Name + " is already on the ground."));
                    }
                    break;
                case Actions.Pickup:
                    if (items[selectedSource].Count > 0)
                    {
                        itemEventDelegates[SelectedSource][ItemEvent.GrabItem]();
                        selectedIndex = 0;
                        selectedSource = (uint)ItemSource.Player;
                    }
                    break;
                case Actions.Close:
                    this.Close();
                    break;
            }

            if (items[selectedSource].Count > 0)
            {
                selectedIndex = (uint)Math.Min(items[selectedSource].Count - 1, (int)selectedIndex);
            }
            else
            {
                selectedSource = (--selectedSource) % 2;
            }
        }

        /// <summary>
        /// Draw inventory panel
        /// </summary>
        /// <param name="brush"></param>
        public override void DrawContent(Brush brush)
        {

            int columnOffset = 0;
            int y = 0;
            var font = GraphicsDispenser.GetFont("Calibri");

            int columnWidth = Width / items.Length;
            int source = 0;
            int index = 0;
            foreach (var container in items)
            {
                index = 0;
                y = 0;
                foreach (var item in container)
                {

                    brush.DrawString(font, item.Name, new Vector2(columnOffset + 5, y), selectedSource == source && selectedIndex == index ? Color.Yellow : Color.White);
                    y += font.LineSpacing;
                    ++index;
                }
                ++source;
                columnOffset = columnWidth * source;
                brush.DrawLine(new Vector2(columnOffset, 0), new Vector2(columnOffset, Height), Color.White);
            }

            
        }
    }
}
