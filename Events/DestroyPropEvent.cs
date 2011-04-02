
namespace Zomgame.Events
{
	class DestroyPropEvent : BaseEvent
	{
		Prop destroyedProp;

		public DestroyPropEvent(Prop nDProp)
		{
			destroyedProp = nDProp;
		}

		public override void fireEvent()
		{
            destroyedProp.Location.RemoveObject(destroyedProp);
            destroyedProp.Location.GameMap.UpdateLightMap();

			//update the LightMap
			destroyedProp.Location.GameMap.UpdateLightMap();
		}
	}
}
