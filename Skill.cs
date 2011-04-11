/*
 * A skill, in code terms, in simply a number that is used to judge the competence in something. They are attached to abilities and checked at points like combat.
 * 
 */

using System;
namespace Zomgame
{
    public class Skill
    {
        protected string name;
        protected int growth; //0 to 99
        protected int value; //1 to 99

        public Skill(string nName)
        {
            name = nName;
            growth = 0;
            value = 1;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Growth
        {
            get { return growth; }
            set { growth = value; }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Raises the growth of the skill.
        /// Note: Need to change it so that the amount is lessened somewhat or grown depending on the level.
        /// </summary>
        /// <param name="amount">Amount to grow by</param>
        public void GrowBy(int amount)
        {
            if (value < 100)
            {
                growth += amount;
                if (growth >= 100)
                {
                    value += 1;
                    growth -= 100;
                }
            }
            else //when attribute value = 100, keep growth at 0
            {
                growth = 0;
            }
        }

    }
}
