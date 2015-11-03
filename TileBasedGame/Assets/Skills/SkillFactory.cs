using UnityEditor;
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
            bloodDonor.OnTarget = (user, target, args) =>
            {
                float amt = user.DamageMultiplier * bloodDonor.basePower * (1f - target.Armor);
                target.TakeDamage(amt);
                user.TakeDamage(-amt);
            };
        }
        return bloodDonor;
    }
}