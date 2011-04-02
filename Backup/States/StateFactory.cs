using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.States
{
    public class StateFactory
    {
        protected static Game game;

        public static void Init(Game game)
        {
            StateFactory.game = game;
        }

        public static GameState CreateItemDetailsState(Item exItem)
        {
            return new ItemDetailsState(game, exItem);
        }

        public static GameState CreateInventoryState()
        {
            return new InventoryState(game);
        }

        public static PlayState CreatePlayState(Camera c)
        {
			PlayState ps = new PlayState(game);
			return ps;
        }



    }
}
