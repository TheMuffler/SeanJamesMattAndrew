using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillFactory
{
    private static Skill bloodDonor = null;
    public static Skill GetBloodDonor()
    {
        if (bloodDonor == null)
        {
            bloodDonor = new Skill();
            bloodDonor.basePower = 3;
            bloodDonor.aoe = 0;
            bloodDonor.range = 2;
			bloodDonor.damageType = Skill.DamageType.DAMAGE;
			bloodDonor.targetType = Skill.TargetType.ENEMY;
            bloodDonor.OnTarget = (user, target, args) =>
            {
                float amt = user.DamageMultiplier * bloodDonor.basePower * (1f - target.Armor);
                target.TakeDamage(amt);
                user.TakeDamage(-amt);
            };
			bloodDonor.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = bloodDonor.gatherTargets(user,tile);
				if(list.Count > 0){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user,list[0]));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(list[0],"Hit"));
				}
				bloodDonor.EnqueueExecuteTask(user,tile,args);
			};
        }
        return bloodDonor;
    }
}