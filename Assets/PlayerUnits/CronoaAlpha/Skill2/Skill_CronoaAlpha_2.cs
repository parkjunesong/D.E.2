using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cronoa_Alpha_2", menuName = "Scriptable Object/SkillData/CronoaAlpha_2")]
public class Skill_CronoaAlpha_2 : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(0.4f, 0, AType.Select, ATarget.Enemy, DType.Penetrate, 0);
        Effect_Base effect2 = new Effect_Damage(0.2f, 0, AType.Select, ATarget.Enemy, DType.Penetrate, 0);
        EffectList.Add(effect1);
        EffectList.Add(effect2);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost); // 0.4AT 관통피해
        for(int i = 0; i< caster.Skill.Passive.CurrentStack; i++)
        {
            EffectList[1].Execute(caster, caster.Ability.AT, Skill_Cost); // 0.2AT 관통피해 * '시간의 톱니바퀴' 횟수
        }
    }
}