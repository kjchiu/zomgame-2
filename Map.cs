/*
 * Map
 * 
 * The map. An array of mapblocks.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zomgame.Props;

namespace Zomgame {
    public class Map
    {
        MapBlock[,] mapBlocks;
        Color[,] lightMap;
        List<Light> lights; //required in order to check all lights
        List<Creature> mapEntities;

        int width, height;

        public delegate void RemoveObjectDel(GameObject aGObject);
        public delegate void AddObjectAtDel(GameObject aGObject, int aX, int aY);
        public delegate void MoveObjectToDel(GameObject aGameObject, MapBlock aMapBlock);

        #region " Properties "
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Color[,] LightMap
        {
            get { return lightMap; }
        }

        public List<Creature> MapEntities
        {
            get { return mapEntities; }
        }
        #endregion


        public Map(int _width, int _height)
        {
            width = _width;
            height = _height;
            lights = new List<Light>();
            mapBlocks = new MapBlock[_width, _height];
            lightMap = new Color[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    mapBlocks[i, j] = new MapBlock(this);
                    mapBlocks[i, j].Coordinates = new Coord(i, j);
                    lightMap[i, j] = Color.Gray;
                }
            }
            mapEntities = new List<Creature>();
        }

        public void SetBlockAt(MapBlock nBlock, int x, int y)
        {
            if (IsInMap(x, y))
            {
                nBlock.Coordinates = new Coord(x, y);
                mapBlocks[x, y] = nBlock;
            }
            else
            {
                throw new BlockOutOfBoundsException(x, y);
            }
        }

        public MapBlock GetBlockAt(Coord coord)
        {
            return GetBlockAt(coord.X, coord.Y);
        }

        public MapBlock GetBlockAt(int x, int y)
        {
            if (IsInMap(x, y))
            {
                return mapBlocks[x, y];
            }
            return new MapBlock(this);
        }

        public void MoveObject(GameObject aGameObject, MapBlock aMapBlock)
        {
            if (this.IsInMap(aMapBlock.Coordinates))
            {
                aGameObject.Location.RemoveObject(aGameObject);
                AddObjectAt(aGameObject, aMapBlock.Coordinates.X, aMapBlock.Coordinates.Y);
            }
        }

        public void AddObjectAt(GameObject gObject, int x, int y)
        {
            gObject.AddObjectAt = new AddObjectAtDel(AddObjectAt);
            gObject.RemoveObject = new RemoveObjectDel(RemoveObject);

            if (gObject is Creature)
            {
                Creature lCreature = (Creature)gObject;
                mapBlocks[x, y].AddObject(lCreature);
                mapEntities.Add(lCreature);
                lCreature.MoveTo = new MoveObjectToDel(MoveObject);
            }
            else if (gObject is Prop)
            {
                mapBlocks[x, y].AddObject((Prop)gObject);
            }
            else if (gObject is Item)
            {
                mapBlocks[x, y].AddObject((Item)gObject);
            }

            if (gObject is Light)
            {
                lights.Add((Light)gObject);
            }
            gObject.Location = mapBlocks[x, y];
        }

        public void RemoveObject(Creature aCreature)
        {
            MapEntities.Remove(aCreature);
            RemoveObject((GameObject)aCreature);
        }

        public void RemoveObject(GameObject aGameObject)
        {
            MapBlock lObjectBlock = aGameObject.Location;
            lObjectBlock.RemoveObject(aGameObject);
        }

        public bool IsInMap(Coord coord)
        {
            return IsInMap(coord.X, coord.Y);
        }

        public bool IsInMap(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }

        private class BlockOutOfBoundsException : Exception
        {
            public BlockOutOfBoundsException(int x, int y)
                : base("Game attempted access to MapBlock with invalid coordinates [" + x + "," + y + "]")
            {
            }
        }

        /// <summary>
        /// Updates the light map by moving through the list of light sources and lighting the areas around them.
        /// Should only be called when a prop is moved/spawned/destroyed. Overhead might be steep.
        /// </summary>
        public void UpdateLightMap()
        {
            if (false)
            {
                foreach (Light light in lights)
                {
                    Coord coord;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            coord = new Coord(x, y);
                            if (IsInMap(coord))
                            {
                                CreateLightRay(light.Location.Coordinates, coord, light);
                            }
                        }
                    }

                    lightMap[light.Location.Coordinates.X, light.Location.Coordinates.Y] =

                        //(float)light.LightColor.G * ((float)light.Strength / 100)
                        //red strength = (float)light.LightColor.R * (light.Strength / 5 / 255) * light.Strength / 5

                        new Color((byte)(light.LightColor.R * light.Strength / 100),
                                  (byte)(light.LightColor.G * light.Strength / 100),
                                  (byte)(light.LightColor.B * light.Strength / 100));
                }
            }
        }
        
        public List<Coord> GetRay(Coord start, Coord target)
        {
            int dir;
            List<Coord> ray = new List<Coord>();
            // check for redundant ray
            //if (target == player.Location) {
            //	return NULL;
            //}
            float slope;
            if ((target.X - start.X) != 0)
            {
                // a = slope = (y2 - y1) / (x2 - x1)
                // y2 - y1 = a(x2 - x1)
                float dx, dy;
                dy = target.Y - start.Y;
                dx = target.X - start.X;
                slope = dy / dx;
                // check which is the dominant axis, (i.e. dx > dy?)
                // iterate over the longer one to ensure no gaps in ray
                //
                if (slope > -1 && slope < 1)
                {
                    float y;
                    dir = (start.X > target.X) ? -1 : 1;
                    for (int x = start.X; x != target.X; x += dir)
                    {
                        y = slope * (x - start.X) + start.Y;
                        float flor = (float)Math.Floor(y);
                        if (y - flor > 0.5f)
                        {
                            ray.Add(new Coord((int)x, (int)Math.Ceiling(y)));
                        }
                        else
                        {
                            ray.Add(new Coord((int)x, (int)flor));
                        }
                    }
                }
                else
                {
                    float x;
                    dir = (start.Y > target.Y) ? -1 : 1;

                    for (int y = start.Y; y != target.Y; y += dir)
                    {
                        x = ((y - start.Y) / slope) + start.X;
                        float flor = (float)Math.Floor(x);
                        if (x - flor > 0.5)
                        {
                            ray.Add(new Coord((int)Math.Ceiling(x), (int)y));
                        }
                        else
                        {
                            ray.Add(new Coord((int)flor, (int)y));
                        }
                    }
                }
            }
            else
            {
                // vertical line, simple case
                //
                dir = (start.Y > target.Y) ? -1 : 1;
                for (int y = start.Y; y != target.Y; y += dir)
                {
                    ray.Add(new Coord(start.X, y));
                }
            }


            ray.Add(new Coord(target.X, target.Y));
            return ray;
        }

        public List<Coord> GetLoSRay(Coord start, Coord target)
        {
            int dir;
            List<Coord> ray = new List<Coord>();
            // check for redundant ray
            //if (target == player.Location) {
            //	return NULL;
            //}
            float slope;
            if ((target.X - start.X) != 0)
            {
                // a = slope = (y2 - y1) / (x2 - x1)
                // y2 - y1 = a(x2 - x1)
                float dx, dy;
                dy = target.Y - start.Y;
                dx = target.X - start.X;
                slope = dy / dx;
                // check which is the dominant axis, (i.e. dx > dy?)
                // iterate over the longer one to ensure no gaps in ray
                //
                if (slope > -1 && slope < 1)
                {
                    float y;
                    dir = (start.X > target.X) ? -1 : 1;
                    for (int x = start.X; x != target.X; x += dir)
                    {
                        y = slope * (x - start.X) + start.Y;
                        float flor = (float)Math.Floor(y);
                        if (y - flor > 0.5f)
                        {
                            ray.Add(new Coord((int)x, (int)Math.Ceiling(y)));
                            if (!GetBlockAt((int)x, (int)Math.Ceiling(y)).SeeThrough)
                            {
                                return ray;
                            }
                        }
                        else
                        {
                            ray.Add(new Coord((int)x, (int)flor));
                            if (!GetBlockAt((int)x, (int)flor).SeeThrough)
                            {
                                return ray;
                            }
                        }
                    }
                }
                else
                {
                    float x;
                    dir = (start.Y > target.Y) ? -1 : 1;

                    for (int y = start.Y; y != target.Y; y += dir)
                    {
                        x = ((y - start.Y) / slope) + start.X;
                        float flor = (float)Math.Floor(x);
                        if (x - flor > 0.5)
                        {
                            ray.Add(new Coord((int)Math.Ceiling(x), (int)y));
                            if (!GetBlockAt((int)Math.Ceiling(x), (int)y).SeeThrough)
                            {
                                return ray;
                            }
                        }
                        else
                        {
                            ray.Add(new Coord((int)flor, (int)y));
                            if (!GetBlockAt((int)flor, (int)y).SeeThrough)
                            {
                                return ray;
                            }
                        }
                    }
                }
            }
            else
            {
                // vertical line, simple case
                //
                dir = (start.Y > target.Y) ? -1 : 1;
                for (int y = start.Y; y != target.Y; y += dir)
                {
                    ray.Add(new Coord(start.X, y));
                    if (!GetBlockAt(start.X, y).SeeThrough)
                    {
                        return ray;
                    }
                }
            }


            ray.Add(new Coord(target.X, target.Y));
            return ray;
        }

        public void CreateLightRay(Coord start, Coord target, Light light)
        {
            int dir;

            //curStr -> the current strenght of the light. Represents how many blocks the light can travel before disappearing completely.
            float curStr = light.Strength / 5, lightR = 0f, lightG = 0f, lightB = 0f;
            // check for redundant ray
            //if (target == player.Location) {
            //	return NULL;
            //}
            curStr--;
            float slope;
            if ((target.X - start.X) != 0)
            {
                // a = slope = (y2 - y1) / (x2 - x1)
                // y2 - y1 = a(x2 - x1)
                float dx, dy;
                dy = target.Y - start.Y;
                dx = target.X - start.X;
                slope = dy / dx;
                // check which is the dominant axis, (i.e. dx > dy?)
                // iterate over the longer one to ensure no gaps in ray
                //
                if (slope > -1 && slope < 1)
                {
                    float y;
                    dir = (start.X > target.X) ? -1 : 1;
                    for (int x = start.X; x != target.X && curStr > 0; x += dir)
                    {
                        lightR = (float)light.LightColor.R * ((float)curStr / 255);
                        lightG = (float)light.LightColor.G * ((float)curStr / 255);
                        lightB = (float)light.LightColor.B * ((float)curStr / 255);

                        y = slope * (x - start.X) + start.Y;
                        float flor = (float)Math.Floor(y);
                        if (y - flor > 0.5f)
                        {
                            //ray.Add(new Coord((int)x, (int)Math.Ceiling(y)));
                            lightMap[(int)x, (int)Math.Ceiling(y)] = new Color(lightR * curStr, lightB * curStr, lightG * curStr);
                            if (!GetBlockAt((int)x, (int)Math.Ceiling(y)).SeeThrough)
                            {
                                return;
                            }
                        }
                        else
                        {
                            //ray.Add(new Coord((int)x, (int)flor));
                            lightMap[(int)x, (int)flor] = new Color((byte)(lightR * curStr), (byte)(lightB * curStr), (byte)(lightG * curStr));

                            if (!GetBlockAt((int)x, (int)flor).SeeThrough)
                            {
                                return;
                            }
                        }
                        curStr--;
                    }
                }
                else
                {
                    float x;
                    dir = (start.Y > target.Y) ? -1 : 1;

                    for (int y = start.Y; y != target.Y && curStr > 0; y += dir)
                    {
                        lightR = (float)light.LightColor.R * ((float)curStr / 255);
                        lightG = (float)light.LightColor.G * ((float)curStr / 255);
                        lightB = (float)light.LightColor.B * ((float)curStr / 255);

                        x = ((y - start.Y) / slope) + start.X;
                        float flor = (float)Math.Floor(x);
                        if (x - flor > 0.5)
                        {
                            lightMap[(int)Math.Ceiling(x), (int)y] = new Color((byte)(lightR * curStr), (byte)(lightB * curStr), (byte)(lightG * curStr));

                            //ray.Add(new Coord((int)Math.Ceiling(x), (int)y));
                            if (!GetBlockAt((int)Math.Ceiling(x), (int)y).SeeThrough)
                            {
                                return;
                            }
                        }
                        else
                        {
                            lightMap[(int)flor, (int)y] = new Color((byte)(lightR * curStr), (byte)(lightB * curStr), (byte)(lightG * curStr));

                            //ray.Add(new Coord((int)flor, (int)y));
                            if (!GetBlockAt((int)flor, (int)y).SeeThrough)
                            {
                                return;
                            }
                        }
                        curStr--;
                    }
                }
            }
            else
            {
                // vertical line, simple case
                //
                dir = (start.Y > target.Y) ? -1 : 1;
                for (int y = start.Y; y != target.Y && curStr > 0; y += dir)
                {
                    lightR = (float)light.LightColor.R * ((float)curStr / 255);
                    lightG = (float)light.LightColor.G * ((float)curStr / 255);
                    lightB = (float)light.LightColor.B * ((float)curStr / 255);

                    lightMap[start.X, y] = new Color((byte)(lightR * curStr), (byte)(lightB * curStr), (byte)(lightG * curStr));
                    // ray.Add(new Coord(start.X, y));
                    if (!GetBlockAt(start.X, y).SeeThrough)
                    {
                        return;
                    }
                    curStr--;
                }
            }

            //lightMap[target.X, target.Y] = new Color((byte)(lightR * curStr), (byte)(lightB * curStr), (byte)(lightG * curStr));
            //ray.Add(new Coord(target.X, target.Y));
            return;
        }

    }
}
