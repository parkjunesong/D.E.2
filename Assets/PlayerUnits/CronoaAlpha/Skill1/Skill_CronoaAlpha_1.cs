using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Cronoa_Alpha_1", menuName = "Scriptable Object/SkillData/CronoaAlpha_1", order = int.MaxValue)]
public class Skill_CronoaAlpha_1 : Skill_Base
{
    public Buff_Base buff;
    public override void SetEffect()
    {
        Effect_Base effect = new Effect_Buff(0.15f, 0, AType.Self, ATarget.Player, buff, 2);
        EffectList.Add(effect);  
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster); // Èû ¹öÇÁ È¹µæ
        caster.Passive.PassiveStack += 2;
    }
}