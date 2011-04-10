using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Zomgame.Graphics;

namespace Zomgame
{
	public class Camera
	{
	    protected Creature focus;
	    protected int width;
        protected int height;
        protected int offset;
	    protected Coord origin;
		protected bool[,] visiblePositions;
		protected Map map;

        public Camera(int width, int height, Map nMap)
            : this(null, width, height, nMap)
	    {
	    }


        public Camera(Creature focus, int width, int height, Map nMap)
	    {
	        this.focus = focus;
	        this.width = width;
	        this.height = height;
			this.map = nMap;
            origin = new Coord(width / 2, height / 2);
			visiblePositions = new bool[width, height];
	    }

        /**
         * <summary>Convert from world coords to screen coords.</summary>
         * <paramter name="position">World coord of entity</parameter>
         */
	    public Coord ToWorld(int x, int y)
	    {
            return new Coord(x + focus.Location.Coordinates.X - origin.X, y + focus.Location.Coordinates.Y - origin.Y);
	    }

        public Coord ToLocal(Coord world)
        {
            return ToLocal(world.X, world.Y);
        }

        public Coord ToLocal(int x, int y)
        {
            return new Coord(x - focus.Location.Coordinates.X + origin.X, y - focus.Location.Coordinates.Y + origin.Y);
        }

		public void Update()
		{

		}


        /// <summary>
        /// Update Line of site table (visiblePositions)
        /// </summary>
		private void UpdateLoS()
		{
            List<Coord> ray = new List<Coord>();
            Coord coord;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    coord = this[x, y];
                    if (map.IsInMap(coord))
                    {
                        ray = map.GetLoSRay(focus.Location.Coordinates, coord);
                        SetVisibleBlocks(ray);
                    }
                }
            }
		}

        /// <summary>
        /// Update visibility for a given ray
        /// </summary>
        /// <param name="visibleLine"></param>
		public void SetVisibleBlocks(List<Coord> visibleLine)
		{
			foreach (Coord c in visibleLine.Select((world) => ToLocal(world.X, world.Y)))
			{
				visiblePositions[c.X, c.Y] = true;
			}
		}

        /// <summary>
        /// Render map centered on focus with line of sight
        /// </summary>
        public void Draw(ZSpriteBatch spriteBatch)
        {
            UpdateLoS();
            
			// draw map
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					//spriteBatch.Draw(map.GetBlockAt(x, y).Graphic.Texture, new Vector2(x * Game.MAP_BLOCK_SIZE, y * Game.MAP_BLOCK_SIZE), Color.White);
					//player.Location.Coordinates.X + x, y);
					//Coord coord = new Coord(focus.Location.Coordinates.X + x - Game.VISIBLE_MAP_OFFSET, focus.Location.Coordinates.Y + y - Game.VISIBLE_MAP_OFFSET);
                    Coord coord = this[x,y];
                    //int r = GameMap.LightMap[x, y].R;
					if (map.IsInMap(coord) && visiblePositions[x, y])
					{
						Texture2D texture = map.GetBlockAt(coord).Graphic.Texture;
						spriteBatch.Draw(texture,
										 new Rectangle(x * Game.MAP_BLOCK_SIZE, y * Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE),
										 GameMap.LightMap[coord.X,coord.Y]);
					}
                    else if (map.IsInMap(coord))
                    {
						Texture2D texture = map.GetBlockAt(coord).Graphic.Texture;
                        spriteBatch.DrawRectangle(x * Game.MAP_BLOCK_SIZE, y * Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE, Color.Gray);
                        //spriteBatch.Draw(texture,
                        //                 new Rectangle(x * Game.MAP_BLOCK_SIZE, y * Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE),
                        //                 Color.White);
					}
					else
					{
						spriteBatch.Draw(GraphicsDispenser.GetTexture("void_bmp"),
										 new Rectangle(x * Game.MAP_BLOCK_SIZE, y * Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE, Game.MAP_BLOCK_SIZE),
										 Color.White);
					}
				}
			}
            this.ResetVision();
			
        }

        /// <summary>
        /// Reset visibility
        /// </summary>
        protected void ResetVision()
        {
            for(int y=0; y < height; ++y)
            {
                for(int x=0; x < width; ++x)
                {
                    visiblePositions[x, y] = false;
                }
                
            }
        }


		public Map GameMap
		{
			get { return map; }
			set { map = value; }
		}

        public Coord this[int x, int y]
        {
            get { return this.ToWorld(x, y); }
        }


        /// <summary>
        /// Height in map blocks
        /// </summary>
        public int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Width in map blocks
        /// </summary>
        public int Width
        {
            get { return this.width; }
        }
        
	}
}
