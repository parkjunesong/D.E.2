using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Skill_WolfCub_Passive", menuName = "Scriptable Object/SkillData/WolfCub_Passive", order = int.MaxValue)]

public class Skill_WolfCub_Passive : Skill_Base
{
    public UnitData SummonUnit;
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
        new Effect_Summon(0, 0, 0, ATarget.Enemy, SummonUnit, Position.Back).Execute(caster);
    }
}