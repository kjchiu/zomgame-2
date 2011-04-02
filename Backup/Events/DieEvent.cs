using System.Diagnostics;
namespace Zomgame.Events
{
	class DieEvent : BaseEvent
	{
		protected Entity dyingEntity;

		public DieEvent(Entity nDEntity)
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
