using System;
using System.Collections.Generic;
using Zomgame.States;

namespace Zomgame {
    public class MapBlock {
        //public static readonly MapBlock NullBlock = new MapBlock(new Coord(-1, -1));
		Map gameMap;

        Coord coordinates;
       Creature iCreature;
        Prop iProp;
        Terrain iTerrain;

        List<Item> itemList;
        
        public MapBlock(Map map) : this(new Coord(0,0), map) { }

        public MapBlock(Coord nCoordinates, Map map) {
            Coordinates = nCoordinates;
			gameMap = map;
            iCreature = null;
            iProp = null;
            itemList = new List<Item>();
            iTerrain = new Terrain();
        }        

        public Coord Coordinates {
            get { return coordinates; }
            set { coordinates = value; }
        }

        public void AddObject(GameObject gObject){
            if (gObject is Creature) {
                iCreature = (Creature)gObject;
            } else if (gObject is Prop) {
                iProp = (Prop)gObject;
            } else if (gObject is Item) {
                itemList.Add((Item)gObject);
            }
            gObject.Location = this;
        }

        public void RemoveObject(GameObject gObject) {
            if (gObject is Creature && CreatureInBlock.Equals(gObject)) {
                iCreature = null;
            }
            else if (gObject is Prop && PropInBlock.Equals(gObject))
            {
                iProp = null;
            } else if (gObject is Item) {
                itemList.Remove((Item)gObject);
            }
        }

		public List<MapBlock> SurroundingBlocks
		{
			get
			{
				List<MapBlock> otherBlocks = new List<MapBlock>();
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X + 1, coordinates.Y + 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X + 1, coordinates.Y - 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X + 1, coordinates.Y + 0));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X + 0, coordinates.Y + 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X + 0, coordinates.Y - 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X - 1, coordinates.Y + 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X - 1, coordinates.Y - 1));
				otherBlocks.Add(gameMap.GetBlockAt(coordinates.X - 1, coordinates.Y + 0));
				return otherBlocks;
			}
		}
        public Sprite Graphic
        {
            get
            {
                if (CreatureInBlock != null)
                    return CreatureInBlock.Graphic;
                else if (itemList.Count > 0)
					return itemList[itemList.Count - 1].Graphic;
				else if (PropInBlock != null)
					return PropInBlock.Graphic;
				else
					return TerrainInBlock.Graphic;
            }
        }

        public Creature CreatureInBlock
        {
            get { return iCreature; }
        }

        public Prop PropInBlock
        {
            get { return iProp; }
        }

        public Terrain TerrainInBlock
        {
            get { return iTerrain; }
            set { iTerrain = value; }
        }

		public Boolean Passable
		{
			get
			{
                if (CreatureInBlock != null)
                {
                    return false;
                }
				if (PropInBlock != null && !PropInBlock.Passable)
			    {
						return false;
				}
				
                return true;

				
			}
		}
		
		public List<Item> Items
		{
			get { return itemList; }
		}

		public Map GameMap
		{
			get { return gameMap; }
		}

		public Boolean HasItems
		{
			get { return (itemList.Count != 0); }
		}

		public Item ItemAt(int index)
		{
			if (index < itemList.Count && index >= 0)
			{
				return itemList[index];
			}
			return null;
		}

		public bool SeeThrough
		{
			get
			{
				return iProp.SeeThrough;
			}
		}

        public override bool Equals(object obj)
		{
			if (obj is MapBlock)
			{
				if (((MapBlock)obj).Coordinates.Equals(this.Coordinates))
				{
					return true;
				}
			}
			return false;
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
	}
}
