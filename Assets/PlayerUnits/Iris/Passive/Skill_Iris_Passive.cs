using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Iris_Passive", menuName = "Scriptable Object/SkillData/Iris_Passive")]
public class Skill_Iris_Passive : Skill_Passive
{
    public override PassiveInstance CreateInstance(Unit caster)
    {
        return new PassiveInstance_Iris(this, caster);
    }
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
        caster.Skill.UseSkill(caster.Skill.prevSkillNo, caster, true);    
    }
}