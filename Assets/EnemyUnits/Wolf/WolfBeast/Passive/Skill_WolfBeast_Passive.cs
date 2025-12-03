using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfBeast_Passive", menuName = "Scriptable Object/SkillData/WolfBeast_Passive")]

public class Skill_WolfBeast_Passive : Skill_Passive
{
    public override PassiveInstance CreateInstance(Unit caster)
    {
        return new PassiveInstance_WolfBeast(this, caster);
    }
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
    }
}