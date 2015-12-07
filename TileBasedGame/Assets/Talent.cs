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
        icon = Resources.Load<Sprite>("SpellIcons/llama");
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
        baseModel = Resources.Load <GameObject> ("basemodel");
    }

    public Unit Generate()
    {
        GameObject model = (GameObject)GameObject.Instantiate(baseModel);
        Unit u = model.AddComponent<Unit>();
        u.curHP = u.maxHP = maxHP;
        u.curMP = u.maxMP = maxMP;
        u.baseDamageMultiplier = attackPower;
        u.baseArmor = armor;
        u.icon = image;

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
        baseModel = Resources.Load<GameObject>("TankModel");

        attackPower = 0.9f;
        armor = 0.4f;
        maxHP = 7;
        maxMP = 6;


        baseSkills.Add(SkillFactory.GetSlam());
        baseSkills.Add(SkillFactory.GetWeakenDefense());
        baseSkills.Add(SkillFactory.GetTaunt());

        Talent t = new Talent();
        t.name = "Enrage";
        t.description = "While health is below 50%, damage goes up by 20%";
        t.IfChosen = unit => {
            unit.AddEffect(EffectFactory.getEnrageEffect(), -1);
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Surrounded";
        t.description = "Whenever the tank is hit, if there are more than two enemies nearby, their attacks will recoil half the damage to eachother. This damage is never fatal.";
        t.IfChosen = unit =>
        {
            unit.AddEffect(EffectFactory.getSurroundedEffect(),-1);
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Grand Slam";
        t.description = "Double's Slam's Damage, but halfs the root effect";
        t.IfChosen = unit =>
        {
            unit.talentTags.Add("HardSlam");
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Determination";
        t.description = "If the tank's health would go below 30% from an attack, the tank is first fully healed. Has a 6 turn cooldown";
        t.IfChosen = unit =>
        {
            unit.AddEffect(EffectFactory.getDeterminationEffect(),-1); //-1 is permanent
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Front Lines";
        t.description = "For each enemy within 3 tiles, gain 3% hp per turn";
        t.IfChosen = unit =>
        {
            unit.AddEffect(EffectFactory.getFrontLineEffect(),-1);
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Useless 2";
        t.IfChosen = Unit =>
        {

        };
		talentOptions.Add(t);

        name = "Tank";
        description = "A tank. Deals and takes little damage. Hinders enemies to protect his team.";
        image = Resources.Load<Sprite>("CharacterIcons/TankIcon");
    }

}

public class AssassinFactory : ClassFactory
{

    public AssassinFactory()
    {
        baseModel = Resources.Load<GameObject>("AssassinModel");

        attackPower = 1.2f;
        armor = 0.1f;
        maxHP = 6;
        maxMP = 6;


        baseSkills.Add(SkillFactory.GetShiv());
        baseSkills.Add(SkillFactory.GetFade());
        baseSkills.Add(SkillFactory.GetCripple());

        Talent t = new Talent();
        t.name = "Shadow Drift";
        t.description = "The assassin teleports to any tile within a huge distance.";
        t.IfChosen = unit => {
            unit.AddSkill(SkillFactory.getShadowDrift());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Add Repair";
        t.description = "Adds Repair to Class";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.GetRepair());
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Single Out";
        t.description = "Deal double damage to units with no allies within 3 tiles.";
        t.IfChosen = unit =>
        {
            unit.AddEffect(EffectFactory.getSingledOutEffect(), -1);
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Pierce";
        t.description = "Shiv is replace by pierce, a ranged attack with similar damage.";
        t.IfChosen = unit =>
        {
            for (int i = 0; i < unit.skillContainers.Count; ++i)
            {
                if (unit.skillContainers[i].skill == SkillFactory.GetShiv())
                {
                    unit.skillContainers.RemoveAt(i);
                    return;
                }
            }
            unit.AddSkill(SkillFactory.getPierce());
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Efficiency";
        t.description = "Move which slightly heals an ally or slightly damages an enemy. Has no energy cost, and restores energy to the caster.";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getEfficiency());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Acid Blade";
        t.description = "Assassin stabs an enemy. Weaker than shiv, but pierces and corrodes an enemy's armor.";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getAcidBlade());
        };
		talentOptions.Add(t);

        name = "Assassin";
        description = "A spooky dangerous assassin, who deals a lot of damage";
        image = Resources.Load<Sprite>("CharacterIcons/AssassinIcon");
    }

}

public class MedicFactory : ClassFactory
{

    public MedicFactory()
    {
        baseModel = Resources.Load<GameObject>("MedicModel");

        attackPower = 0.9f;
        armor = 0.4f;
        maxHP = 4;
        maxMP = 6;


        baseSkills.Add(SkillFactory.GetHeal());
        baseSkills.Add(SkillFactory.GetAoEHeal());
        baseSkills.Add(SkillFactory.GetWeakenOffense());

        Talent t = new Talent();
        t.name = "Anatomy";
        t.description = "Medic learns the Anatomy Skill. Provides a huge damage buff to a single target.";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getAnatomy());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Med Station";
        t.description = "Can deploy a med station, which passively heals allies around it";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getMakeMedStation());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Blood Donor";
        t.description = "Medic learns the Blood Donor Skill. The medic siphons the hitpoints of his target.";
        t.IfChosen = unit => {
            unit.AddSkill(SkillFactory.GetBloodDonor());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Balance";
        t.description = "Heal now damages a nearby enemy in addition to healing the target. It also siphons the enemy's armor.";
        t.IfChosen = unit =>
        {
            unit.talentTags.Add("Balance");
        };
		talentOptions.Add(t);

       

        t = new Talent();
        t.name = "Lethal Injection";
        t.description = "Medic gains Lethal Injection, a single target attack that slightly lowers the target's armor and damage";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getLethalInjection());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Oath Breaker";
        t.description = "AOE heal now deals damage instead";
        t.IfChosen = unit =>
        {
            unit.talentTags.Add("OathBreaker");
        };
		talentOptions.Add(t);

        name = "Medic";
        description = "A doctor who supports his team with his healing powers.";
        image = Resources.Load<Sprite>("CharacterIcons/MedicIcon");
    }

}

public class TechFactory : ClassFactory
{

    public TechFactory()
    {
        baseModel = Resources.Load<GameObject>("TechModel");

        attackPower = 0.9f;
        armor = 0.4f;
        maxHP = 4;
        maxMP = 6;


        baseSkills.Add(SkillFactory.GetSnipe());
        baseSkills.Add(SkillFactory.GetRepair());
        baseSkills.Add(SkillFactory.GetMakeSentry());

        Talent t = new Talent();
        t.name = "Med Station";
        t.description = "Able to create med stations. Static structures that restore health to nearby allies every turn.";
        t.IfChosen = unit => {
            unit.AddSkill(SkillFactory.getMakeMedStation());
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Add Repair";
        t.description = "Adds Repair to Class";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.GetRepair());
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Battle Bot";
        t.description = "Creates a mobile robot buddy. Will move to and attack enemies for you.";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getMakeBattlePet());
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Sticky Grenade";
        t.description = "Attaches a sticky grenade to an enemy, which explodes after 2 turns.";
        t.IfChosen = unit =>
        {
            unit.AddSkill(SkillFactory.getStickyGrenade());
        };
		talentOptions.Add(t);

        t = new Talent();
        t.name = "Master Sniper";
        t.description = "Snipe is free.";
        t.IfChosen = unit =>
        {
            unit.talentTags.Add("FreeSnipe");
        };
        talentOptions.Add(t);

        t = new Talent();
        t.name = "Useless 2";
        t.IfChosen = Unit =>
        {

        };
		talentOptions.Add(t);

        name = "Technician";
        description = "A repairman who can build robots";
        image = Resources.Load<Sprite>("CharacterIcons/TechIcon");
    }

}