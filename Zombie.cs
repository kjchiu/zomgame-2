using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Zomgame.Constants;
using Zomgame.ZombieStates;

namespace Zomgame
{
    public class Zombie : Entity
    {
        public Dictionary<string, ZombieState> stateList;
		ZombieState currentState;

        
        public Zombie(Player p) : base("null_bmp")
        {
			Name = "Zombie-" + ThisID;
            stateList = new Dictionary<string, ZombieState>();
            stateList.Add(ZombieStateNames.HUNT_STATE, new HuntState(this, p));
            stateList.Add(ZombieStateNames.SEARCH_STATE, new SearchState(this, p));
            stateList.Add(ZombieStateNames.WANDER_STATE, new WanderState(this, p));
            ChangeStateTo(ZombieStateNames.WANDER_STATE);
        }

		public void Update()
		{
			currentState.DoZombieStuff();
		}

		public ZombieState CurrentState
		{
			get { return currentState; }
			set { currentState = value; }
		}

        public void ChangeStateTo(ZombieState state)
        {
            state.player = currentState.player;
            state.zombie = this;
            currentState = state;
        }

        /// <summary>
        /// State-changing function for any state except SEARCH. Uses state names from ZombieStateNames.cs.
        /// </summary>
        /// <param name="state">Name of state to switch to</param>
        public void ChangeStateTo(string state)
        {
            currentState = stateList[state];
        }

        /// <summary>
        /// This state changing function is used for the SEARCH state. The location is where the zombie will be heading.
        /// </summary>
        /// <param name="state">This will be "Search", otherwise the destination is ignored.</param>
        /// <param name="destination">The destination for the search.</param>
        public void ChangeStateTo(string state, MapBlock destination)
        {
            currentState = stateList[state];
            if (state.Equals(ZombieStateNames.SEARCH_STATE)){
                 ((SearchState)currentState).Destination = destination;
            }
        }

		public override Sprite Graphic
		{
			get
			{
				return currentState.graphic;
			}
		}
    }
}