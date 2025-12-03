using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfCub_1", menuName = "Scriptable Object/SkillData/Skill_WolfCub")]

public class Skill_WolfCub_1 : Skill_Base
{
    public UnitData SummonUnit;
    public override void SetEffect() 
    {
        Effect_Base effect = new Effect_Summon(0, 0, 0, ATarget.Enemy, SummonUnit, Position.Back);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
    }
}