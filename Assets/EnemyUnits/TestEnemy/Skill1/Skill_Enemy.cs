using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Enemy", menuName = "Scriptable Object/SkillData/Skill_Enemy", order = int.MaxValue)]

public class Skill_Enemy : Skill_Base
{
    public override void SetEffect() 
    {
        Effect_Base effect = new Effect_Heal(1f, 0, AType.Self, ATarget.Enemy);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
    }
}
