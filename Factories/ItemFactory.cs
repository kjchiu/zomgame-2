using Zomgame.Abilities;
using Zomgame.Items;
using Zomgame.Constants;
namespace Zomgame.Factories
{
	static class ItemFactory
	{
		public static Item CreateDefaultItem()
		{
			return new Item();
		}

		public static Item CreateDefaultFood()
		{
			Item foodItem = new Item("choc_bar_bmp");
			foodItem.Name = "Default Food";

			return foodItem;
		}

        public static Item CreateBandage(Player p)
        {
            MedicalItem mItem = new MedicalItem(1, 5, "bandage_bmp");
            mItem.Name = "Bandage";
            mItem.Description = "A simple bandage. Won't help much, but isn't hard to use.";
            mItem.AttachAbility(new FirstAidAbility(p.Skills[SkillNames.MEDICAL_SKILL]));

            return mItem;
        }
	}
}
