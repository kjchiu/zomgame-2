namespace Zomgame.Factories
{
	static class EntityFactory
	{
		public static Zombie CreateZombie(Player p){
			Zombie zom = new Zombie(p);
			zom.Graphic = new Sprite("zombie_wander_bmp");
			//fiddle with stats or something
			return zom;
		}

	}
}
