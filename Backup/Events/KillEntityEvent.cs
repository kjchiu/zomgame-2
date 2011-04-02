using System.Diagnostics;
namespace Zomgame.Events
{
	class KillEntityEvent : BaseEvent
	{
		protected Entity dyingEntity;

		public KillEntityEvent(Entity nDEntity)
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
