using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfBeast_1", menuName = "Scriptable Object/SkillData/WolfBeast_1")]

public class Skill_WolfBeast_1 : Skill_Base
{
    public Buff_Base buff;
    public override void SetEffect() 
    {
        Effect_Base effect1 = new Effect_Damage(0.5f, 0, AType.Front, ATarget.Player, DType.Normal, 0);
        Effect_Base effect2 = new Effect_Buff(0.15f, 10, 0, ATarget.Enemy, buff, 2);

        EffectList.Add(effect1);
        EffectList.Add(effect2);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost);
        EffectList[1].Execute(caster);
    }
}