using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Skill_Noa_Passive", menuName = "Scriptable Object/SkillData/Noa_Passive", order = int.MaxValue)]
public class Skill_Noa_Passive : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(0.5f, 10, AType.Select, ATarget.Enemy, DType.Normal);
        EffectList.Add(effect1);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster); // 0.5AT АјАн
    }
}