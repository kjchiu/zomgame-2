using System;
using System.Collections.Generic;
using Zomgame.States;

namespace Zomgame {
    public class MapBlock {
        //public static readonly MapBlock NullBlock = new MapBlock(new Coord(-1, -1));
		Map gameMap;

        Coord coordinates;
        List<Entity> entityList;
        List<Prop> propList;
        List<Item> itemList;
        
        List<Terrain> terrainList;

        
        public MapBlock(Map map) : this(new Coord(0,0), map) { }

        public MapBlock(Coord nCoordinates, Map map) {
            Coordinates = nCoordinates;
			gameMap = map;
            entityList = new List<Entity>();
            propList = new List<Prop>();
            itemList = new List<Item>();
            terrainList = new List<Terrain>();
            terrainList.Add(new Terrain());
        }        

        public Coord Coordinates {
            get { return coordinates; }
            set { coordinates = value; }
        }

        public void AddObject(GameObject gObject){
            if (gObject is Entity) {
                entityList.Add((Entity)gObject);
            } else if (gObject is Prop) {
                propList.Add((Prop)gObject);
            } else if (gObject is Item) {
                itemList.Add((Item)gObject);
            }
            gObject.Location = this;
        }

        public void RemoveObject(GameObject gObject) {
            if (gObject is Entity) {
                entityList.Remove((Entity)gObject);
            } else if (gObject is Prop) {
                propList.Remove((Prop)gObject);
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
                if (entityList.Count > 0)
                    return entityList[entityList.Count - 1].Graphic;
                else if (itemList.Count > 0)
					return itemList[itemList.Count - 1].Graphic;
				else if (propList.Count > 0)
					return propList[propList.Count - 1].Graphic;
				else
					return terrainList[terrainList.Count - 1].Graphic;
            }
        }

		public Boolean Passable
		{
			get
			{
				foreach (Prop p in Props)
				{
					if (!p.Passable)
					{
						return false;
					}
				}

				return true;

			}
		}
		public List<Prop> Props
		{
			get { return propList; }
		}

		public List<Entity> Entities
		{
			get { return entityList; }
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
				if (Props.Count > 0)
				{
					return Props[0].SeeThrough;
				}
				return true;
			}
		}

        public List<Terrain> TerrainList
        {
            get { return terrainList; }
            set { terrainList = value; }
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
	}
}
