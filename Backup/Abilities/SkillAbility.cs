/* 
 * The Skill Ability is simply an Ability that uses a skill when its action is invoked.
 * 
 */

namespace Zomgame.Abilities
{
    abstract class SkillAbility : Ability
    {
        Skill attSkill; //attached skill

        public SkillAbility(Skill nSkill) 
        {
            attSkill = nSkill;
        }

        public Skill AttSkill
        {
            get { return attSkill; }
        }

    }
}
