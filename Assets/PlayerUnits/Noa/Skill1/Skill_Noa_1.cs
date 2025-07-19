using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Noa_1", menuName = "Scriptable Object/SkillData/Noa_1", order = int.MaxValue)]
public class Skill_Noa_1 : Skill_Base
{
    public Buff_Base buff;
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(1.0f, 0, AType.Front, ATarget.Enemy, DType.Normal, 0);
        Effect_Base effect2 = new Effect_Buff(0.15f, 0, AType.Self, ATarget.Player, buff, 2);
        EffectList.Add(effect1);
        EffectList.Add(effect2);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster); // 1.0AT Àü¹æ°ø°Ý
        EffectList[1].Execute(caster); // Èû ¹öÇÁ È¹µæ
    }

}