/*
 * An item used for healing wounds and treating injury. Requires some skill to use, or there's a good chance there'll be problems.
 * 
 */

namespace Zomgame.Items
{
	class MedicalItem : Item
	{
		int difficulty, healingStrength;
	
		public MedicalItem(int nDif, int nStrength, string imgLoc) 
            : base(imgLoc)
		{
			Name = "MedicalItem" + ThisID;
			difficulty = nDif;
			healingStrength = nStrength;
		}

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        public int HealingStrength
        {
            get { return healingStrength; }
            set { healingStrength = value; }
        }
	}
}
