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
}