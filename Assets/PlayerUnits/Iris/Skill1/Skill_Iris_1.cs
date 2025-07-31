using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Iris_1", menuName = "Scriptable Object/SkillData/Iris_1", order = int.MaxValue)]
public class Skill_Iris_1 : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(0.4f, 0, AType.Front, ATarget.Enemy, DType.Normal, 0);
        EffectList.Add(effect1);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost); // 0.4AT 전방공격
    }

}