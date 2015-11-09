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

	//---------------//
	//Assassin Skills//
	//---------------//

	private static Skill shiv = null;
	public static Skill GetShiv()
	{
		if (shiv == null)
		{
			shiv = new Skill();
			shiv.name = "Shiv";
			shiv.icon = Resources.Load<Sprite>("SpellIcons/owl");
			
			shiv.basePower = 1;
			shiv.aoe = 0;
			shiv.range = 1;
			shiv.manaCost = user => 1;
			shiv.cooldown = 0;
			shiv.damageType = Skill.DamageType.DAMAGE;
			shiv.targetType = Skill.TargetType.ENEMY;
			shiv.OnTarget = (user, target, args) =>
			{
				float amt = user.DamageMultiplier * shiv.basePower * (1f - target.Armor);
				target.TakeDamage(amt,user);

			};
			shiv.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = shiv.gatherTargets(user,tile);
				foreach(Unit target in list){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user, target));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,target.tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(target,"Hit"));
				}
				shiv.EnqueueExecuteTask(user,tile,args);
			};
		}
		return shiv;
	}


	private static Skill fade = null;
	public static Skill GetFade()
	{
		if(fade == null)
		{
			fade = new Skill();
			fade.name = "Fade";
			fade.icon = Resources.Load<Sprite>("SpellIcons/llama");
			
			fade.basePower = 0;
			fade.manaCost = user => 1;
			fade.aoe = 0;
			fade.range = 0;
			fade.cooldown = 6;
			fade.damageType = Skill.DamageType.HEAL;
			fade.targetType = Skill.TargetType.SELF;
			fade.OnTarget = (user, target, args) => {
				//bool flag = user.talentTags.Contains("Fade Upgrade");
				target.AddEffect(EffectFactory.getInvincibleEffect(),3);

			};
			fade.GenerateTasks = (user, tile, args) =>
			{
				GameManager.instance.tasks.Add(new Task_Trigger_Animation(user, "Punch"));
				GameManager.instance.tasks.Add(new Task_Wait(0.3f));
			
				fade.EnqueueExecuteTask(user, tile, args);
			};
		}
		return fade;
	}


	private static Skill cripple = null;
	public static Skill GetCripple()
	{
		if (cripple == null)
		{
			cripple = new Skill();
			cripple.name = "Cripple";
			cripple.icon = Resources.Load<Sprite>("SpellIcons/owl");
			
			cripple.basePower = 1;
			cripple.aoe = 0;
			cripple.range = 4;
			cripple.manaCost = user => 3;
			cripple.cooldown = 0;
			cripple.damageType = Skill.DamageType.DAMAGE;
			cripple.targetType = Skill.TargetType.ENEMY;
			cripple.OnTarget = (user, target, args) =>
			{
				float amt = user.DamageMultiplier * cripple.basePower * (1f - target.Armor);
				target.TakeDamage(amt,user);
				target.AddEffect(EffectFactory.getCrippleEffect(), 3);
			};
			cripple.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = cripple.gatherTargets(user,tile);
				foreach(Unit target in list){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user, target));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,target.tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(target,"Hit"));
				}
				cripple.EnqueueExecuteTask(user,tile,args);
			};
		}
		return cripple;
	}
	//------------//
	// Tech Skills//
	//------------//
	private static Skill snipe = null;
	public static Skill GetSnipe()
	{
		if (snipe == null)
		{
			snipe = new Skill();
			snipe.name = "Snipe";
			snipe.icon = Resources.Load<Sprite>("SpellIcons/owl");
			
			snipe.basePower = 6;
			snipe.aoe = 0;
			snipe.range = 4;
			snipe.manaCost = user => 6;
			snipe.cooldown = 0;
			snipe.damageType = Skill.DamageType.DAMAGE;
			snipe.targetType = Skill.TargetType.ENEMY;
			snipe.OnTarget = (user, target, args) =>
			{
				float amt = user.DamageMultiplier * snipe.basePower * (1f - target.Armor);
				target.TakeDamage(amt,user);
				
			};
			snipe.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = snipe.gatherTargets(user,tile);
				foreach(Unit target in list){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user, target));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,target.tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(target,"Hit"));
				}
				snipe.EnqueueExecuteTask(user,tile,args);
			};
		}
		return snipe;
	}

	private static Skill repair = null;
	public static Skill GetRepair()
	{
		if (repair == null)
		{
			repair = new Skill();
			repair.name = "Repair";
			repair.icon = Resources.Load<Sprite>("SpellIcons/owl");
			
			repair.basePower = 2;
			repair.aoe = 0;
			repair.range = 4;
			repair.manaCost = user => 2;
			repair.cooldown = 0;
			repair.damageType = Skill.DamageType.HEAL;
			repair.targetType = Skill.TargetType.ALLY;
			repair.OnTarget = (user, target, args) =>
			{
				float amt = -user.DamageMultiplier * repair.basePower ;
				target.TakeDamage(amt,user);
				target.AddEffect(EffectFactory.getDamageMultiplierEffect(),3);
			};
			repair.GenerateTasks = (user,tile,args)=>
			{
				List<Unit> list = repair.gatherTargets(user,tile);
				foreach(Unit target in list){
					GameManager.instance.tasks.Add(new Task_Face_Eachother(user, target));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(user,"Punch"));
					GameManager.instance.tasks.Add(new Task_Wait(0.3f));
					GameManager.instance.tasks.Add(new Task_Fire_Projectile(user.transform.position+Vector3.up,target.tile.transform.position+Vector3.up,(GameObject)Resources.Load("SpellVisuals/BulletCube")));
					GameManager.instance.tasks.Add(new Task_Trigger_Animation(target,"Hit"));
				}
				repair.EnqueueExecuteTask(user,tile,args);
			};
		}
		return repair;
	}


	
	
	
}