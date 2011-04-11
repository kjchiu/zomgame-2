using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zomgame.Factories;
using Zomgame.Props;

namespace Zomgame.MapObjects.MapGen
{
    class Room : MapGenObject
    {
        private Coord iTopLeft;
        private int iHeight;
        private int iWidth;
        private Direction iDoorLocation;

        public Room(Coord aTopLeft, int aHeight, int aWidth, Direction aDirection)
        {
            iTopLeft = aTopLeft;
            iHeight = aHeight;
            iWidth = aWidth;
            iDoorLocation = aDirection;
        }

        public override void Construct(Map aMap)
        {
            for (int x = 0; x < iWidth; x++)
            {
                for (int y = 0; y < iHeight; y++)
                {
                    if (x == 0 || x == iWidth - 1 || y == 0 || y == iHeight - 1)
                    {
                        aMap.GetBlockAt(iTopLeft[x, y]).AddObject(PropFactory.GetWoodWall());
                    }
                    
                }
            }
            Coord lDoorCoord;
            switch (DoorLocation)
            {
                case Direction.NORTH:
                    lDoorCoord = iTopLeft[Width / 2, 0];
                    break;

                case Direction.EAST:
                    lDoorCoord = iTopLeft[iWidth - 1, iHeight / 2];
                    break;

                case Direction.SOUTH:
                    lDoorCoord = iTopLeft[iWidth / 2, iHeight - 1];
                    break;

                case Direction.WEST:
                    lDoorCoord = iTopLeft[0, iHeight / 2];
                    break;

                default:
                    lDoorCoord = iTopLeft[iWidth / 2, 0];
                    break;
            }
            aMap.GetBlockAt(lDoorCoord).AddObject(PropFactory.GetWoodDoor());
        }

        public int Height
        {
            get { return iHeight; }
            set { iHeight = value; }
        }

        public int Width
        {
            get { return iWidth; }
            set { iWidth = value; }
        }

        public Direction DoorLocation
        {
            get { return iDoorLocation; }
            set { iDoorLocation = value; }

        }
    }
}
