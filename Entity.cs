using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zomgame
{
    public class Entity : GameObject
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT }
        public enum EntityState { IDLE, BUSY };

		protected int health;

        protected Vector2 position;
        protected Vector2 direction;
        protected float velocity = 0.2f;
        protected float radius = 5.0f;

        protected EntityState state;

        public Entity(string imgLoc)
			: this(new Vector2(0, 0), imgLoc)
        {
        }

		/// <summary>
		/// Actual initialization constructor.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="imgLoc"></param>
		public Entity(Vector2 direction, string imgLoc) : base(imgLoc)
        {
            this.direction = direction;
            state = EntityState.IDLE;
			Name = "Entity-" + ThisID;
			Health = 10;
        }

        public EntityState State {
            get { return state; }
            set { state = value; }
        }
		
        public virtual void Move(Direction dir, GameTime delta)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    position.X -= Distance(delta);
                    break;
                case Direction.RIGHT:
                    position.X += Distance(delta);
                    break;
                case Direction.UP:
                    position.Y -= Distance(delta);
                    break;
                case Direction.DOWN:
                    position.Y += Distance(delta);
                    break;
            }
        }

        public virtual void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    position.X -= 1;
                    break;
                case Direction.RIGHT:
                    position.X += 1;
                    break;
                case Direction.UP:
                    position.Y -= 1;
                    break;
                case Direction.DOWN:
                    position.Y += 1;
                    break;
            }
        }

        protected float Distance(GameTime delta)
        {
            return velocity * delta.ElapsedGameTime.Milliseconds;          
        }

		public int Health
		{
			get { return health; }
			set { health = value; }
		}

		public void TakeDamage(int amount)
		{
			health -= amount;
		}
    }
}
