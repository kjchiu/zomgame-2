namespace Zomgame.Factories
{
	static class EntityFactory
	{
		public static Zombie CreateZombie(Player p, Map aMap){
			Zombie zom = new Zombie(p, aMap);
			zom.Graphic = new Sprite("zombie_wander_bmp");
			//fiddle with stats or something
			return zom;
		}

	}
}
