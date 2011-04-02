using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Zomgame.UI
{
    class InventoryWindow : InteractiveWindow<InventoryWindow.Actions>
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

        public override Actions ProcessKey(InputHandler input)
        {
            if (input.IsKeyPushed(KeyBindings.UP))
            {
                return Actions.Up;
            }
            else if (input.IsKeyPushed(KeyBindings.DOWN))
            {
                return Actions.Down;
            }
            else if (input.IsKeyPushed(KeyBindings.LEFT))
            {
                return Actions.Left;
            }
            else if (input.IsKeyPushed(KeyBindings.RIGHT))
            {
                return Actions.Right;
            }
            else if (input.IsKeyPushed(KeyBindings.CLOSE_INV))
            {
                return Actions.Close;
            }
            else if (input.IsKeyPushed(KeyBindings.DROP_ITEM))
            {
                return Actions.Drop;
            }
            else if (input.IsKeyPushed(Keys.A))
            {
                return Actions.Ability;
            }
            else
            { 
                return Actions.None;
            }
        }

        public void Update(GameTime gameTime, Actions action)
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
                    items[(int)selectedSource][(int)selectedIndex].Abilities["Drop"].ItemAction(player);
                    break;
                case Actions.Pickup:
                    if (items[selectedSource].Count > 0)
                    {
                        itemEventDelegates[SelectedSource][ItemEvent.GrabItem]();
                        selectedIndex = 0;
                        selectedSource = (uint)ItemSource.Player;
                    }

            }

            if (items[selectedSource].Count > 0)
            {
                selectedIndex = (uint)Math.Min(Items[selectedSource].Count, (int)selectedIndex);
            }
            else
            {
                selectedSource = (--selectedSource) % 2;
            }
        }
    }
}
