using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Zomgame.Abilities
{
	class FirstAidAbility : SkillAbility
	{
		public FirstAidAbility(Skill medicSkill) 
            :base(medicSkill)
		{
			ItemAction = new PerformAction(Invoke);
		}

		public override string Name
		{
			get
			{
				return "First Aid";
			}
		}

		public void Invoke(Player player)
		{
			//check the skill first
			//MessageHandler.Instance.AddMessage("Youtried to use", AttachedItem);
            Trace.WriteLine("Medical Item used. First Aid skill = " + AttSkill.Value);
            AttSkill.GrowBy(20);
        }
	}
}
