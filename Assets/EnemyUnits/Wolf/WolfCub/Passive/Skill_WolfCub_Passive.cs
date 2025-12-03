using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfCub_Passive", menuName = "Scriptable Object/SkillData/WolfCub_Passive")]

public class Skill_WolfCub_Passive : Skill_Passive
{
    public UnitData SummonUnit;

    public override PassiveInstance CreateInstance(Unit caster)
    {
        return new PassiveInstance_WolfCub(this, caster);
    }
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
        new Effect_Summon(0, 0, 0, ATarget.Enemy, SummonUnit, Position.Back).Execute(caster);
        new Effect_Summon(0, 0, 0, ATarget.Enemy, SummonUnit, Position.Back).Execute(caster);

    }
}