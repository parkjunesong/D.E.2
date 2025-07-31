using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Iris_2", menuName = "Scriptable Object/SkillData/Iris_2", order = int.MaxValue)]
public class Skill_Iris_2 : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(1.2f, 0, AType.Select, ATarget.Enemy, DType.Normal, 0);
        EffectList.Add(effect1);
    }
    public override void Execute(Unit caster)
    {
        SkillManager.Instance.CastingList.Add(new CastingSkill(caster, this, 2));
    }
    public override void CastingExecute(Unit caster)
    {
        EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost); // 1.2AT 전방공격
        CostManager.Instance.Cost_Vanish(CostManager.Instance.RandCostFindNum());
    }
}