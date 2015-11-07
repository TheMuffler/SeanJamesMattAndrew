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
            rootEffect.moveRangeBonus = -999;
            //rootEffect.animBool = "Dizzy"; If we had a dizzy animaiton and wanted rooted characters to look like that.
        }
        return rootEffect;
    }

}