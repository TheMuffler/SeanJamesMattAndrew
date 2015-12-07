using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectFactory {

    private static Effect rootEffect;
    public static Effect getRootEffect()
    {
        if (rootEffect == null)
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
    public static Effect getStunEffect() {

        if (stunEffect == null)
        {
            stunEffect = new Effect();
            stunEffect.onTurnBegin = user =>
            {
                GameManager.instance.ProcessCommand(() => { });
            };
            //stunEffect.animBool = "Dizzy"; character will look whoozy while stunned, if we put in an animation like this.
            stunEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/TANK/SLAM/stun effect prefab");
        }
        return stunEffect;
    }

    private static Effect pierceEffect;
    public static Effect getPierceEffect()
    {
        if (pierceEffect == null)
        {
            pierceEffect = new Effect();
            pierceEffect.onTurnBegin = user =>
            {
                GameManager.instance.ProcessCommand(() => { }); //skips turn
            };
            pierceEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/ASSASSIN/PIERCE/pierce effect prefab");
        }
        return pierceEffect;
    }

    private static Effect efficiencyEffect;
    public static Effect getEfficiencyEffect(bool isAlly)
    {
        if (efficiencyEffect == null)
        {
            efficiencyEffect = new Effect();
            string prefab = (isAlly ? "SpellVisuals/ASSASSIN/EFFICIENCY/efficiency ally effect prefab" : "SpellVisuals/ASSASSIN/EFFICIENCY/efficiency enemy effect prefab");
            efficiencyEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/ASSASSIN/EFFICIENCY/efficiency effect prefab");
        }
        return efficiencyEffect;
    }

    private static Effect vampiricEffect;
    public static Effect getVampiricEffect()
    {
        if (vampiricEffect == null)
        {
            vampiricEffect = new Effect();
            vampiricEffect.OnHitAttacking = (attacker, defender, amt) =>
            {
                if (attacker != defender && amt > 0)
                    attacker.TakeDamage(-amt, attacker);
            };
        }
        return vampiricEffect;
    }

    private static Effect invincibleEffect;
    public static Effect getInvincibleEffect() {

        if (invincibleEffect == null)
        {
            invincibleEffect = new Effect();

            invincibleEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/ASSASSIN/FADE/Fade Prefab");
            invincibleEffect.armorBonus = user => 999;
        }
        return invincibleEffect;
    }

    private static Effect crippleEffect;
    public static Effect getCrippleEffect() {

        if (crippleEffect == null)
        {
            crippleEffect = new Effect();
            crippleEffect.moveRangeBonus = -2;

            //stunEffect.animBool = "Dizzy"; character will look whoozy while stunned, if we put in an animation like this.
            crippleEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/ASSASSIN/CRIPPLE/cripple effect prefab");
        }
        return crippleEffect;
    }



    private static Effect damageMultiplierEffect;
    public static Effect getDamageMultiplierEffect() {

        if (damageMultiplierEffect == null)
        {
            damageMultiplierEffect = new Effect();
            damageMultiplierEffect.damageBonus = user => 2;

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
            weakenOffenseEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/MEDIC/WEAKEN OFFENSE/weaken offense effect prefab");
        }
        return weakenOffenseEffect;
    }

    private static Effect weakenDefenseEffect;
    public static Effect GetWeakenDefenseEffect() {
        if (weakenDefenseEffect == null) {
            weakenDefenseEffect = new Effect();
            weakenDefenseEffect.armorBonus = user => -1;
            weakenDefenseEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/MEDIC/WEAKEN DEFENSE/weaken defense effect prefab");
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
            smokeEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/ASSASSIN/FADE/Fade Prefab");

        }
        return smokeEffect;
    }

    private static Effect enrageEffect;
    public static Effect getEnrageEffect()
    {
        if (enrageEffect == null)
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
        if (surroundedEffect == null)
        {
            surroundedEffectHelper = new Skill();
            surroundedEffectHelper.aoe = 2;
            surroundedEffectHelper.targetType = Skill.TargetType.ENEMY;

            surroundedEffect = new Effect();
            surroundedEffect.OnHitDefending = (attacker, defender, amt) =>
            {
                List<Unit> others = surroundedEffectHelper.gatherTargets(defender, defender.tile);
                if (others.Count >= 2)
                {
                    foreach (Unit other in others)
                    {
                        if (other.curHP > amt / 2f)
                            other.TakeDamage(amt / 2f, defender);
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
        if (determinationEffect == null)
        {
            determinationEffect = new Effect();
            determinationEffect.OnHitDefending = (attacker, defender, amt) =>
            {
                if (!defender.talentTags.Contains("DTCoolDown") && (defender.curHP - amt) / defender.maxHP <= 0.3)
                {
                    defender.curHP = defender.maxHP;
                    determinationEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/TANK/DETERMINATION/determination prefab");
                    defender.AddEffect(getDeterminationCooldownEffect(), 6);
                }
            };
        }
        return determinationEffect;
    }

    private static Effect determinationCooldownEffect;
    public static Effect getDeterminationCooldownEffect()
    {
        if (determinationCooldownEffect == null)
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

    private static Skill medStationHelper;
    private static Effect medStationEffect;
    public static Effect getMedStationEffect()
    {
        if(medStationEffect == null)
        {
            medStationHelper = new Skill();
            medStationHelper.targetType = Skill.TargetType.ALLY;
            medStationHelper.range = 0;
            medStationHelper.aoe = 4;
            medStationEffect = new Effect();
            medStationEffect.onTurnBegin = user =>
            {
                foreach(Unit ally in medStationHelper.gatherTargets(user, user.tile))
                {
                    ally.TakeDamage(-2, user);
                    medStationEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/MEDIC/AOE HEAL/aoe receiver prefab");

                }
            };
        }
        return medStationEffect;
    }

    private static Effect doomEffect;
    public static Effect getDoomEffect()
    {
        if(doomEffect == null)
        {
            doomEffect = new Effect();
            doomEffect.OnExit = user =>
            {
                user.TakeDamage(99999, user);
            };
        }
        return doomEffect;
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
            anatomyEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/MEDIC/ANATOMY/anatomy projectile prefab");
            anatomyEffect.damageBonus = user => 0.4f;
        }
        return anatomyEffect;
    }

    private static Effect balanceBuffEffect;
    public static Effect getBalanceBuffEffect()
    {
        if(balanceBuffEffect == null)
        {
            balanceBuffEffect = new Effect();
            balanceBuffEffect.armorBonus = user => 0.1f;
        }
        return balanceBuffEffect;
    }

    private static Effect balanceDebuffEffect;
    public static Effect getBalanceDebuffEffect()
    {
        if (balanceBuffEffect == null)
        {
            balanceBuffEffect = new Effect();
            balanceBuffEffect.armorBonus = user => -0.1f;
        }
        return balanceBuffEffect;
    }

    private static Effect lethalInjectionEffect;
    public static Effect getLethalInjectionEffect()
    {
        if (lethalInjectionEffect == null)
        {
            lethalInjectionEffect = new Effect();
            lethalInjectionEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/MEDIC/LETHAL INJECTION/lethal injection effect prefab");
            lethalInjectionEffect.armorBonus = user => -0.1f;
            lethalInjectionEffect.damageBonus = user => -0.1f;
        }
        return lethalInjectionEffect;
    }

    private static Skill epidemicEffectHelper;
    private static Effect epidemicEffect;
    public static Effect getEpidemicEffect()
    {
        if (epidemicEffect == null)
        {
            epidemicEffectHelper = new Skill();
            epidemicEffectHelper.targetType = Skill.TargetType.ALLY;
            epidemicEffectHelper.range = 0;
            epidemicEffectHelper.aoe = 3;
            epidemicEffectHelper.basePower = 8;
            epidemicEffectHelper.OnTarget = (user, target, args) =>
            {
                float amt = epidemicEffectHelper.basePower * (1f - target.Armor);
                target.TakeDamage(amt, user);
            };
            epidemicEffectHelper.GenerateTasks = (user, tile, args) =>
            {
                GameManager.instance.tasks.Add(new Task_ShowParticleAnimation((GameObject)Resources.Load("SpellVisuals/ASSASSIN/EPIDEMIC/epidemic prefab"), user.transform.position, 2));
                epidemicEffectHelper.EnqueueExecuteTask(user, tile, args);
            };
            epidemicEffect = new Effect();
            epidemicEffect.OnExit = user =>
            {
                epidemicEffectHelper.Perform(user, user.tile);
            };
        }
        return epidemicEffect;
    }

    private static Skill stickyGrenadeEffectHelper;
    private static Effect stickyGrenadeEffect;
    public static Effect getStickyGrenadeEffect()
    {
        if(stickyGrenadeEffect == null)
        {
            stickyGrenadeEffectHelper = new Skill();
            stickyGrenadeEffectHelper.targetType = Skill.TargetType.ALLY;
            stickyGrenadeEffectHelper.range = 0;
            stickyGrenadeEffectHelper.aoe = 2;
            stickyGrenadeEffectHelper.basePower = 2;
            stickyGrenadeEffectHelper.OnTarget = (user, target, args) =>
            {
                float amt = stickyGrenadeEffectHelper.basePower * (1f - target.Armor);
                target.TakeDamage(amt, user);
            };
            stickyGrenadeEffectHelper.GenerateTasks = (user, tile, args) =>
            {
                GameManager.instance.tasks.Add(new Task_ShowParticleAnimation((GameObject)Resources.Load("SpellVisuals/TECH/GRENADE/sticky grenade explosion prefab"), user.transform.position, 1));
                stickyGrenadeEffectHelper.EnqueueExecuteTask(user, tile, args);
            };

            stickyGrenadeEffect = new Effect();
            stickyGrenadeEffect.particlePrefab = Resources.Load<GameObject>("SpellVisuals/TECH/GRENADE/stuck sticky prefab");
            stickyGrenadeEffect.OnExit = user =>
            {
                stickyGrenadeEffectHelper.Perform(user, user.tile);
            };
        }
        return stickyGrenadeEffect;
    }

    private static Skill singledOutEffectHelper;
    private static Effect singledOutEffect;
    public static Effect getSingledOutEffect()
    {
        if(singledOutEffect == null)
        {
            singledOutEffectHelper = new Skill();
            singledOutEffectHelper.targetType = Skill.TargetType.ALLY;
            singledOutEffectHelper.range = 0;
            singledOutEffectHelper.aoe = 3;

            singledOutEffect = new Effect();
            singledOutEffect.OnHitAttacking = (attacker, defender, amt) =>
            {
                if(singledOutEffectHelper.gatherTargets(defender,defender.tile).Count==0)
                    defender.TakeDamage(amt, attacker, true);
            };
        }
        return singledOutEffect;
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