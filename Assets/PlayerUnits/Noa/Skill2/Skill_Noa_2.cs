using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Noa_2", menuName = "Scriptable Object/SkillData/Noa_2", order = int.MaxValue)]
public class Skill_Noa_2 : Skill_Base
{  
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(1.0f, 0, AType.Front, ATarget.Enemy, DType.Normal, 0);
        Effect_Base effect2 = new Effect_Damage(0.3f, 10, AType.Select, ATarget.Enemy, DType.Penetrate, 0);
        EffectList.Add(effect1);
        EffectList.Add(effect2);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster); // 1.0AT 전방공격
        EffectList[1].Execute(caster); // 0.3AT 관통공격 3회
        EffectList[1].Execute(caster); // 0.3AT 관통공격 3회
        EffectList[1].Execute(caster); // 0.3AT 관통공격 3회
    }
}