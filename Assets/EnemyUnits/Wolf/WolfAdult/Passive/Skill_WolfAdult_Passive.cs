using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfAdult_Passive", menuName = "Scriptable Object/SkillData/WolfAdult_Passive")]

public class Skill_WolfAdult_Passive : Skill_Passive
{
    public override PassiveInstance CreateInstance(Unit caster)
    {
        return new PassiveInstance_WolfAdult(this, caster);
    }
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
    }
}