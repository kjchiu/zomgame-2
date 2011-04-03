using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Zomgame.States
{
    class ItemDetailsState : GameState
    {
        Item examinedItem;
        List<String> abilityNames;

        Vector2 topLeft;
        int width;
        int height;
        int hMargin;
        int vMargin;
        int selectedIndex;

        public ItemDetailsState(Game game, Item nExItem)
            : base (game)
        {
            width = 150;
            height = 300;
            hMargin = 10;
            vMargin = 5;
            selectedIndex = 0;
            topLeft = new Vector2(300 - 1 - width / 2,
                                    400 - 1 - height / 2);
            
            examinedItem = nExItem;
            abilityNames = new List<String>(examinedItem.Abilities.Keys);
        }

        public override void UpdateState(GameTime time, InputHandler input)
        {
            if (KeyIsPushed(Keys.Escape))
            {
                this.EndState();
            }
            if (KeyIsPushed(Keys.Enter))
            {
                //activate selected ability
                examinedItem.Abilities[abilityNames[selectedIndex]].ItemAction(player);
                this.EndState();
            }
            if (KeyIsPushed(KeyBindings.UP))
            {
                selectedIndex = (selectedIndex > 0 ? selectedIndex - 1 : abilityNames.Count - 1);
            }
            if (KeyIsPushed(KeyBindings.DOWN))
            {
                if (selectedIndex + 1 < abilityNames.Count)
                {
                    selectedIndex++;
                }
                else 
                { 
                    selectedIndex = 0; 
                }
            }
        }

        public override void DrawState(GameTime time)
        {
            spriteBatch.DrawRectangle((int)topLeft.X, (int)topLeft.Y, width, height, Color.Black, 1, Color.DarkGray);
            for (int i = 0; i < abilityNames.Count; i++)
            {
                spriteBatch.DrawString(font, abilityNames[i], new Vector2(topLeft.X + hMargin, topLeft.Y + i * font.LineSpacing + vMargin), (i==selectedIndex ? Color.Yellow : Color.White));
            }
        }
    }
}
