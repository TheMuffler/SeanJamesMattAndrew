using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
class Effect
{
    public float damageBonus = 0;
    public float armorBonus = 0;
    public bool root = false;
    public bool stun = false;
    public float dot = 0;

    public bool HasDot
    {
        get
        {
            return dot != 0;
        }
    }
}
