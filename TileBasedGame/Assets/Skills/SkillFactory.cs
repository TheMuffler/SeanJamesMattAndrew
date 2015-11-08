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
            bloodDonor.name = "Blood\nDonor";
            bloodDonor.icon = Resources.Load<Sprite>("SpellIcons/owl");

            bloodDonor.basePower = 3;
            bloodDonor.aoe = 0;
            bloodDonor.range = 4;
			bloodDonor.manaCost = user => 3;
            bloodDonor.cooldown = 3;
			bloodDonor.damageType = Skill.DamageType.DAMAGE;
			bloodDonor.targetType = Skill.TargetType.ENEMY;
            bloodDonor.OnTarget = (user, target, args) =>
            {
                float amt = user.DamageMultiplier * bloodDonor.basePower * (1f - target.Armor);
                target.TakeDamage(amt,user);
                user.TakeDamage(-amt,user);
            };
			bloodDonor.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = bloodDonor.gatherTargets(user,tile);
				foreach(Unit target in list){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user, target));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,target.tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(target,"Hit"));
				}
				bloodDonor.EnqueueExecuteTask(user,tile,args);
			};
        }
        return bloodDonor;
    }

    private static Skill slam = null;
    public static Skill GetSlam()
    {
        if(slam == null)
        {
            slam = new Skill();
            slam.name = "Slam";
            slam.icon = Resources.Load<Sprite>("SpellIcons/llama");

            slam.basePower = 4;
            slam.manaCost = user => 4;
            slam.aoe = 1;
            slam.range = 0;
            slam.cooldown = 4;
            slam.damageType = Skill.DamageType.DAMAGE;
            slam.targetType = Skill.TargetType.ENEMY;
            slam.OnTarget = (user, target, args) => {
                bool flag = user.talentTags.Contains("Slam Upgrade");

                float amt = user.DamageMultiplier * bloodDonor.basePower * (1f - target.Armor);
                target.TakeDamage(amt*(flag?2:1), user);
                target.AddEffect(EffectFactory.getStunEffect(), flag?1:2);
            };
            slam.GenerateTasks = (user, tile, args) =>
             {
                 GameManager.instance.tasks.Add(new Task_Trigger_Animation(user, "Punch"));
                 GameManager.instance.tasks.Add(new Task_Wait(0.3f));
                 foreach(Unit target in slam.gatherTargets(user, tile))
                 {
                     GameManager.instance.tasks.Add(new Task_Trigger_Animation(target, "Hit"));
                 }
                 slam.EnqueueExecuteTask(user, tile, args);
             };
        }
        return slam;
    }

}