using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Noa_3", menuName = "Scriptable Object/SkillData/Noa_3")]
public class Skill_Noa_3: Skill_Base
{
    public override bool IsAvailable(Unit caster)
    {
        if (caster.Skill.Passive.CurrentStack < 5) return false;
        else return true;
    }   
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(0.6f, 0, AType.Front, ATarget.Enemy, DType.Normal, 0);
        Effect_Base effect2 = new Effect_Damage(0.6f, 0, AType.Front, ATarget.Enemy, DType.Normal, 30);
        EffectList.Add(effect1);
        EffectList.Add(effect2);
    }
    public override void Execute(Unit caster)
    {       
        if (caster.Skill.Passive.CurrentStack < 10) // 10스택 미만, 강화 전
        {
            for (int i = 0; i < caster.Skill.Passive.CurrentStack; i++)
            {
                EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost); // 0.6AT * 스택 횟수
            }                      
        }
        else // 10스택 이상, 강화 후
        {
            for (int i = 0; i < caster.Skill.Passive.CurrentStack; i++)
            {
                EffectList[1].Execute(caster, caster.Ability.AT, Skill_Cost); // 0.6AT * 스택 횟수, 방무30%
            }
        }
        caster.Skill.Passive.CurrentStack = 0;
    }
}