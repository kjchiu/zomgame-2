﻿/*
 *  Event Factory
 *  
 * Creates all kinds of events, inherited from the BaseEvent class.
 * 
 */

using Zomgame.Events;

namespace Zomgame.Factories {
    static class EventFactory {

        public static BaseEvent CreateMoveEvent(Creature nMov, MapBlock nDest) {
		    if (nMov.State != Creature.EntityState.BUSY)
			{
				MoveEvent mEvent = new MoveEvent(nMov, nDest);
				mEvent.CreateTime = Game.TurnsPassed;
				mEvent.ActivateTime = Game.TurnsPassed + 1;

				nMov.State = Creature.EntityState.BUSY;
				return mEvent;
			}
			else
			{
				return EventFactory.CreateEmptyEvent(nMov);
			}
			
          
        }

		public static AttackSpaceEvent CreateAttackSpaceEvent(Creature nAtt, MapBlock nSpa){
			AttackSpaceEvent aEvent = new AttackSpaceEvent(nAtt, nSpa);

			aEvent.CreateTime = Game.TurnsPassed;
			aEvent.ActivateTime = Game.TurnsPassed + 0;

			return aEvent;
		}

		public static AttackEvent CreateAttackEvent(Creature nAtt, Creature nDef)
		{
			AttackEvent aEvent = new AttackEvent(nAtt, nDef);

			aEvent.CreateTime = Game.TurnsPassed;
			aEvent.ActivateTime = Game.TurnsPassed + 1;

			nAtt.State = Creature.EntityState.BUSY;

			return aEvent;
		}

		public static AttackPropEvent CreateAttackPropEvent(Creature nAtt, Prop nDef)
		{
			AttackPropEvent aEvent = new AttackPropEvent(nAtt, nDef);

			aEvent.CreateTime = Game.TurnsPassed;
			aEvent.ActivateTime = Game.TurnsPassed + 1;

			nAtt.State = Creature.EntityState.BUSY;

			return aEvent;
		}
		
		public static DestroyPropEvent CreateDestroyPropEvent(Prop nProp)
		{
			DestroyPropEvent dEvent = new DestroyPropEvent(nProp);

			dEvent.CreateTime = Game.TurnsPassed;
			dEvent.ActivateTime = Game.TurnsPassed + 0;

			return dEvent;
		}

        public static DropItemEvent CreateDropItemEvent(Player picker, Item item)
        {
            DropItemEvent ev = new DropItemEvent(picker, item);
            ev.CreateTime = Game.TurnsPassed;
            ev.ActivateTime = Game.TurnsPassed;

            picker.State = Creature.EntityState.BUSY;

            return ev;
        }

		public static EquipItemEvent CreateEquipItemEvent(Player equipper, Item item, string location)
		{
			EquipItemEvent eiv = new EquipItemEvent(equipper, item, location);
			eiv.CreateTime = Game.TurnsPassed;
			eiv.ActivateTime = Game.TurnsPassed + 2;

			equipper.State = Creature.EntityState.BUSY;

			return eiv;
		}

		public static KillEntityEvent CreateKillEntityEvent(Creature nDie)
		{
			KillEntityEvent dEvent = new KillEntityEvent(nDie);

			dEvent.CreateTime = Game.TurnsPassed;
			dEvent.ActivateTime = Game.TurnsPassed + 0;
		
			nDie.State = Creature.EntityState.BUSY;

			return dEvent;
		}

		public static WaitEvent CreateEmptyEvent(Creature nEnt)
		{
			WaitEvent wEvent = new WaitEvent(nEnt);

			wEvent.CreateTime = Game.TurnsPassed;
			wEvent.ActivateTime = Game.TurnsPassed + 0;

			return wEvent;
		}

        public static MakeNoiseEvent CreateMakeNoiseEvent(MapBlock mb, int strength)
        {
            MakeNoiseEvent nEvent = new MakeNoiseEvent(mb, strength);
            nEvent.CreateTime = Game.TurnsPassed;
            nEvent.ActivateTime = Game.TurnsPassed + 0;

            return nEvent;
        }
		
		public static PickupItemEvent CreatePickupItemEvent(Player picker, Item item)
		{
			PickupItemEvent pEvent = new PickupItemEvent(picker, item);
			pEvent.CreateTime = Game.TurnsPassed;
			pEvent.ActivateTime = Game.TurnsPassed + 0;

			picker.State = Creature.EntityState.BUSY;

			return pEvent;
		}

        public static PropInteractionEvent CreatePropInteractionEvent(Prop prop, Player player)
        {
            PropInteractionEvent pEvent = new PropInteractionEvent(prop, player);
            pEvent.CreateTime = Game.TurnsPassed;
            pEvent.ActivateTime = Game.TurnsPassed + 1;

            player.State = Creature.EntityState.BUSY;

            return pEvent;
        }

		public static ReceiveItemEvent CreateReceiveItemEvent(Player picker, Item item)
		{
			ReceiveItemEvent rEvent = new ReceiveItemEvent(picker, item);
			rEvent.CreateTime = Game.TurnsPassed;
			rEvent.ActivateTime = Game.TurnsPassed + 0;

			picker.State = Creature.EntityState.BUSY;

			return rEvent;
		}

		public static UnequipItemEvent CreateUnequipItemEvent(Player p, Item i)
		{
			UnequipItemEvent uiEvent = new UnequipItemEvent(p, i);
			uiEvent.CreateTime = Game.TurnsPassed;
			uiEvent.ActivateTime = Game.TurnsPassed + 0;

			p.State = Creature.EntityState.BUSY;

			return uiEvent;
		}

        public static WaitEvent CreateWaitEvent(Creature nWait)
        {
            WaitEvent wEvent = new WaitEvent(nWait);
            wEvent.CreateTime = Game.TurnsPassed;
            wEvent.ActivateTime = Game.TurnsPassed + 5;

            nWait.State = Creature.EntityState.BUSY;

            return wEvent;
        }
    }
}
