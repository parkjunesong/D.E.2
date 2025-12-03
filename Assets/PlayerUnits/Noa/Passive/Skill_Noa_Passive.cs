using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Noa_Passive", menuName = "Scriptable Object/SkillData/Noa_Passive")]
public class Skill_Noa_Passive : Skill_Passive
{
    public override PassiveInstance CreateInstance(Unit caster)
    {
        return new PassiveInstance_Noa(this, caster);
    }
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
        new Effect_Damage(0.5f, 10, AType.Select, ATarget.Enemy, DType.Normal, 0).Execute(caster, caster.Ability.AT, new int[] {0,0,0}); // 0.5AT АјАн
    }
}