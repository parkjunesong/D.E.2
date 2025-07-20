using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_WolfCub", menuName = "Scriptable Object/SkillData/Skill_WolfCub", order = int.MaxValue)]

public class Skill_WolfCub : Skill_Base
{
    public UnitData SummonUnit;
    public override void SetEffect() 
    {
        Effect_Base effect = new Effect_Summon(1, 0, 0, ATarget.Enemy, SummonUnit, Position.Back);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
    }
}
