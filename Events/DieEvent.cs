using System.Diagnostics;
namespace Zomgame.Events
{
	class DieEvent : BaseEvent
	{
		protected Creature dyingEntity;

		public DieEvent(Creature nDEntity)
		{
			dyingEntity = nDEntity;
		}

		public override void fireEvent()
		{
			Trace.WriteLine(dyingEntity.Name + " has died.");
			dyingEntity.Location.RemoveObject(dyingEntity);
		}
	}
}
