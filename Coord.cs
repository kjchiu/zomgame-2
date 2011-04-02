using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zomgame 
{
    public class Coord {
        private int x;
        private int y;

        public Coord():this(0,0) {}

        public Coord(int nx, int ny) {
            X = nx;
            Y = ny;
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

		public static Coord operator +(Coord x, Coord y)
		{
			return new Coord(x.X + y.X, x.Y + y.Y);
		}

		public static Coord operator -(Coord x, Coord y)
		{
			return new Coord(x.X - y.X, x.Y - y.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Coord)
			{
				Coord otherCoord = (Coord)obj;
				return (this.X == otherCoord.X) && (this.Y == otherCoord.Y);
			}
			return false;
		}

		public int DistanceTo(Coord otherCoord)
		{
			int distX = this.X - otherCoord.X;
			int distY = this.Y - otherCoord.Y;
			double dist = Math.Sqrt( (distX * distX) + (distY * distY) );

			return (int)dist;
		}

		public Vector2 ToVector2
		{
			get { return new Vector2(X, Y); }
		}

        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }
    }
}
