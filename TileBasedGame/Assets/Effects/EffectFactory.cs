using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectFactory{

    private static Effect rootEffect;
    public static Effect getRootEffect()
    {
        if(rootEffect == null)
        {
            rootEffect = new Effect();
            rootEffect.onTurnBegin = user =>
            {
                user.hasMoved = true;
            };
            //rootEffect.animBool = "Dizzy"; If we had a dizzy animaiton and wanted rooted characters to look like that.
        }
        return rootEffect;
    }

    private static Effect stunEffect;
    public static Effect getStunEffect(){

        if (stunEffect == null)
        {
            stunEffect = new Effect();
            stunEffect.onTurnBegin = user =>
            {
                GameManager.instance.ProcessCommand(() => { });
            };
            //stunEffect.animBool = "Dizzy"; character will look whoozy while stunned, if we put in an animation like this.
            //stunEffect.particlePrefab = Resources.Load<GameObject>("EffectParticles/StunParticle");
        }
        return stunEffect;
    }

    private static Effect vampiricEffect;
    public static Effect getVampiricEffect()
    {
        if (vampiricEffect == null)
        {
            vampiricEffect = new Effect();
            vampiricEffect.OnHitAttacking = (attacker, defender, amt) =>
            {
                if(attacker != defender && amt>0)
                    attacker.TakeDamage(-amt,attacker);
            };
        }
        return vampiricEffect;
    }

	private static Effect invincibleEffect;
	public static Effect getInvincibleEffect(){
		
		if (invincibleEffect == null)
		{
			invincibleEffect = new Effect();

		
			invincibleEffect.armorBonus=user => 999;
		}
		return invincibleEffect;
	}

	private static Effect crippleEffect;
	public static Effect getCrippleEffect(){
		
		if (crippleEffect == null)
		{
			crippleEffect = new Effect();
			crippleEffect.moveRangeBonus= -2;
			
			//stunEffect.animBool = "Dizzy"; character will look whoozy while stunned, if we put in an animation like this.
			//stunEffect.particlePrefab = Resources.Load<GameObject>("EffectParticles/StunParticle");
		}
		return crippleEffect;
	}



	private static Effect damageMultiplierEffect;
	public static Effect getDamageMultiplierEffect(){
		
		if (damageMultiplierEffect == null)
		{
			damageMultiplierEffect = new Effect();
			damageMultiplierEffect.damageBonus= user => 2;
			
			//stunEffect.animBool = "Dizzy"; character will look whoozy while stunned, if we put in an animation like this.
			//stunEffect.particlePrefab = Resources.Load<GameObject>("EffectParticles/StunParticle");
		}
		return damageMultiplierEffect;
	}

	private static Effect weakenOffenseEffect;
	public static Effect GetWeakenOffenseEffect() {
		if (weakenOffenseEffect == null) {
			weakenOffenseEffect = new Effect();
			weakenOffenseEffect.damageBonus = user => -2;
		}
		return weakenOffenseEffect;
	}

	private static Effect weakenDefenseEffect;
	public static Effect GetWeakenDefenseEffect() {
		if (weakenDefenseEffect == null) {
			weakenDefenseEffect = new Effect();
			weakenDefenseEffect.armorBonus = user => -1;
		}
		return weakenDefenseEffect;
	}

	private static Effect tauntEffect;
	public static Effect getTauntEffect() {
		if (tauntEffect == null) {
			tauntEffect = new Effect();
            tauntEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/TANK/TAUNT/taunt prefab");
			//TODO: finish dis shiiiit
		}
		return tauntEffect;
	}

	// Particles
	private static Effect wrenchExplosionEffect;
	public static Effect getWrenchExplosionEffect() {
		if (wrenchExplosionEffect == null) {
			wrenchExplosionEffect = new Effect();
			wrenchExplosionEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/WrenchExplosion");
			//wrenchExplosionEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/WrenchOngoing");
			
		}
		return wrenchExplosionEffect;
	}
	private static Effect wrenchBuffEffect;
	public static Effect getWrenchBuffEffect() {
		if (wrenchBuffEffect == null) {
			wrenchBuffEffect = new Effect();
			
			wrenchBuffEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/WrenchOngoing");
			
		}
		return wrenchBuffEffect;
	}
	private static Effect smokeEffect;
	public static Effect getSmokeEffect() {
		if (smokeEffect == null) {
			smokeEffect = new Effect();
			smokeEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/Fade");
			
		}
		return smokeEffect;
	}

    private static Effect enrageEffect;
    public static Effect getEnrageEffect()
    {
        if(enrageEffect == null)
        {
            enrageEffect = new Effect();
            enrageEffect.damageBonus = user => user.curHP / user.maxHP <= 0.5f ? 0.2f : 0f;
        }
        return enrageEffect;
    }

    private static Skill surroundedEffectHelper;
    private static Effect surroundedEffect;
    public static Effect getSurroundedEffect()
    {
        if(surroundedEffect == null)
        {
            surroundedEffectHelper = new Skill();
            surroundedEffectHelper.aoe = 2;
            surroundedEffectHelper.targetType = Skill.TargetType.ENEMY;

            surroundedEffect = new Effect();
            surroundedEffect.OnHitDefending = (attacker, defender, amt) =>
            {
                List<Unit> others = surroundedEffectHelper.gatherTargets(defender, defender.tile);
                if(others.Count >= 2)
                {
                    foreach(Unit other in others)
                    {
                        if (other.curHP > amt/2f)
                            other.TakeDamage(amt /2f, defender);
                        else if (other.curHP > 0.1f)
                            other.TakeDamage(other.curHP - 0.1f, defender);
                        else
                            other.TakeDamage(0, defender);
                        GameManager.instance.tasks.Add(new Task_Trigger_Animation(other, "Hit"));
                    }
                };
            };
        }
        return surroundedEffect;
    }

    private static Effect determinationEffect;
    public static Effect getDeterminationEffect()
    {
        if(determinationEffect == null)
        {
            determinationEffect = new Effect();
            determinationEffect.OnHitDefending = (attacker, defender, amt) =>
            {
                if(!defender.talentTags.Contains("DTCoolDown") && (defender.curHP - amt)/defender.maxHP <= 0.3)
                {
                    defender.curHP = defender.maxHP;
                    defender.AddEffect(getDeterminationCooldownEffect(), 6);
                }
            };
        }
        return determinationEffect;
    }

    private static Effect determinationCooldownEffect;
    public static Effect getDeterminationCooldownEffect()
    {
        if(determinationCooldownEffect == null)
        {
            determinationCooldownEffect = new Effect();
            determinationCooldownEffect.OnEnter = user =>
            {
                user.talentTags.Add("DTCoolDown");
            };
            determinationCooldownEffect.OnExit = user =>
            {
                user.talentTags.Remove("DTCoolDown");
            };
        }
        return determinationCooldownEffect;
    }


    private static Skill frontLineEffectHelper;
    private static Effect frontLineEffect;
    public static Effect getFrontLineEffect()
    {
        if (frontLineEffect == null)
        {
            frontLineEffectHelper = new Skill();
            frontLineEffectHelper.aoe = 3;
            frontLineEffectHelper.targetType = Skill.TargetType.ENEMY;

            frontLineEffect = new Effect();
            frontLineEffect.onTurnBegin = user =>
            {
                List<Unit> others = frontLineEffectHelper.gatherTargets(user, user.tile);

                float perOther = user.maxMP * 0.03f;
                user.TakeDamage(-perOther*others.Count, user);
            };
        }
        return frontLineEffect;
    }

    private static Effect anatomyEffect;
    public static Effect getAnatomyEffect()
    {
        if(anatomyEffect == null)
        {
            anatomyEffect = new Effect();
            anatomyEffect.damageBonus = user => 0.4f;
        }
        return anatomyEffect;
    }

    private static Effect epidemicEffect;
    public static Effect getEpidemicEffect()
    {
        if (epidemicEffect == null)
        {
            epidemicEffect = new Effect();

        }
        return epidemicEffect;
    }

    private static Effect persistenceEffect;
    public static Effect getPersistenceEffect()
    {
        if (persistenceEffect == null)
        {
            persistenceEffect = new Effect();

        }
        return persistenceEffect;
    }
}