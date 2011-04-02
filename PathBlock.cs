using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame
{
	/// <summary>
	/// This is a block used for pathfinding. Contains parameters for A*-related paths. Tree-like, but not really.
	/// </summary>
	public class PathBlock
	{
		int g, h;
		PathBlock parent; 
		MapBlock attachedBlock; //the MapBlock that this PathBlock represents

		public PathBlock(MapBlock m, PathBlock p)
		{
			attachedBlock = m;
			parent = p;
			G = 0;
		}

		public PathBlock Parent
		{
			get { return parent; }
		}

		public MapBlock AttachedBlock
		{
			get { return attachedBlock; }
		}

		/// <summary>
		/// Distance from this block to the start.
		/// </summary>
		public int G
		{
			get { return g; }
			set { g = value; }
		}

		/// <summary>
		/// Heuristic approximation of distance from this block to the target.
		/// </summary>
		public int H
		{
			get { return h; }
			set { h = value; }
		}

		/// <summary>
		/// Summation of G and H.
		/// </summary>
		public int F
		{
			get { return G + H; }
		}

		/// <summary>
		/// //H is calculated using the Manhattan method - just check the difference in X and Y, then add them together
		/// </summary>
		/// <param name="to">The block to check to.</param>
		public void CalculateH(MapBlock to)
		{
			H = Math.Abs(this.AttachedBlock.Coordinates.X - to.Coordinates.X) +
							 Math.Abs(this.AttachedBlock.Coordinates.Y - to.Coordinates.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is PathBlock)
			{
				if (((PathBlock)obj).AttachedBlock.Coordinates.Equals(this.AttachedBlock.Coordinates))
				{
					return true;
				}
			}
			return false;
		}
	}
}
