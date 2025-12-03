using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Iris_3_1", menuName = "Scriptable Object/SkillData/Iris_3_1")]
public class Skill_Iris_3_1 : Skill_Base
{
    public override void SetEffect()
    {
    }
    public override void Execute(Unit caster)
    {
        Effect_Base effect = new Effect_Damage(0.6f, 10, AType.Front, ATarget.Enemy, DType.Penetrate, 20 + ((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack);
        effect.Execute(caster, caster.Ability.AT, Skill_Cost); // 0.6AT 관통공격
    }

}