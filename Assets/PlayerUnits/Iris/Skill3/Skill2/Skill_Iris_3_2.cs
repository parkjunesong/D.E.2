using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Iris_3_2", menuName = "Scriptable Object/SkillData/Iris_3_2")]
public class Skill_Iris_3_2 : Skill_Base
{
    public override void SetEffect()
    {
    }
    public override void Execute(Unit caster)
    {
        Effect_Base effect = new Effect_Damage(1.6f, 0, AType.Select, ATarget.Enemy, DType.Penetrate, 20 + ((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack);
        effect.Execute(caster, caster.Ability.AT, Skill_Cost); // 1.6AT 관통공격
        CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna));
    } 
}