using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Talent
{
    public bool toggle = false;
    public string name;
    public string description;
    public Sprite icon;

    public Talent()
    {

    }

    public delegate void AlterCharacterDelegate(Unit unit);
    public AlterCharacterDelegate IfChosen = unit => { };

}

public abstract class ClassFactory
{

    public string name;
    public string description;
    public List<Talent> talentOptions = new List<Talent>();

    public List<Skill> baseSkills = new List<Skill>();

    public GameObject baseModel;

    public float attackPower, armor, maxHP, maxMP;
    public Sprite image;

    public ClassFactory()
    {

    }

    public Unit Generate()
    {
        GameObject model = (GameObject)GameObject.Instantiate(baseModel);
        Unit u = model.AddComponent<Unit>();
        u.curHP = u.maxHP = maxHP;
        u.curMP = u.maxMP = maxMP;
        u.baseDamageMultiplier = attackPower;
        u.baseArmor = armor;

        foreach (Skill s in baseSkills)
            u.AddSkill(s);

        foreach (Talent t in talentOptions)
            if (t.toggle)
                t.IfChosen(u);

        return u;
    }


}

public class TankFactory : ClassFactory
{

    public TankFactory()
    {
        attackPower = 0.9f;
        armor = 0.4f;
        maxHP = 4;
        maxMP = 6;

        Talent t = new Talent();
        t.name = "Add Blood Donor";
        t.description = "Adds Blood Donor to Class";
        t.IfChosen = unit => {
            unit.AddSkill(SkillFactory.GetBloodDonor());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Improve Skill X";
        t.description = "Makes Skill X better. Skill X is scripted to check if the performing agent has the string 'SkillXImprove' in its talentTags";
        t.IfChosen = unit =>
        {
            unit.talentTags.Add("SkillXImprove");
        };
        talentOptions.Add(t);
    }

}